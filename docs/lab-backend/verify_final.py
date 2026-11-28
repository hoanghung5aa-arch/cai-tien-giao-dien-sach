"""Targeted HTTP regression on the local app; creates and removes only its own QA rows."""
import io
import json
from datetime import datetime, timedelta
from pathlib import Path

import requests
from bs4 import BeautifulSoup
from PIL import Image

BASE = "http://127.0.0.1:5080"
s = requests.Session()
results = []


def check(name, passed):
    results.append({"check": name, "passed": bool(passed)})
    if not passed:
        raise AssertionError(name)


def get(path):
    r = s.get(BASE + path, timeout=40)
    r.raise_for_status()
    return BeautifulSoup(r.text, "html.parser")


def post(path, data, page=None, files=None):
    page = page or path
    token_node = get(page).select_one('input[name="__RequestVerificationToken"]')
    if token_node is None:
        token_node = get('/Movies/Create').select_one('input[name="__RequestVerificationToken"]')
    token = token_node['value']
    return s.post(BASE + path, data={**data, "__RequestVerificationToken": token}, files=files, timeout=40, allow_redirects=False)


stamp = datetime.now().strftime("%Y%m%d%H%M%S")
title = "LAB QA " + stamp
gid = mid = None
try:
    form = get("/Movies/Create")
    gid = form.select_one('select[name="GenreId"] option[value]:not([value=""])')["value"]
    data = dict(Title=title, Summary="Kiểm thử hồi quy tự động của bài LAB", GenreId=gid,
                ReleaseDate=(datetime.now() + timedelta(days=30)).strftime("%Y-%m-%d"), Price="5000", Rated="4.5")
    for field, value in [("Title", " a "), ("Summary", " a "), ("Price", "4999"), ("Rated", "0.9"), ("Rated", "5.1"), ("GenreId", "2147483647"), ("ReleaseDate", "2020-01-01")]:
        r = post("/Movies/Create", {**data, field: value})
        errors = BeautifulSoup(r.text, "html.parser").select(f'[data-valmsg-for="{field}"]')
        check(f"reject invalid {field}={value}", r.status_code == 200 and any(e.get_text(strip=True) for e in errors))
    r = post("/Movies/Create", data, files={"PictureUpload": ("invalid.jpg", b"not an image", "image/jpeg")})
    check("reject forged JPG signature", r.status_code == 200 and bool(BeautifulSoup(r.text, "html.parser").select_one('[data-valmsg-for="PictureUpload"]').get_text(strip=True)))
    png = io.BytesIO()
    Image.new("RGB", (16, 24), "#255042").save(png, "PNG")
    r = post("/Movies/Create", data, files={"PictureUpload": ("qa.png", png.getvalue(), "image/png")})
    check("create at minimum price with valid PNG", r.status_code == 302)
    listing = get("/Movies?search=" + requests.utils.quote(title))
    details = next(a["href"] for a in listing.select('a[href*="/Movies/Details/"]'))
    mid = int(details.rsplit("/", 1)[1])
    edit = get(f"/Movies/Edit/{mid}")
    old_src = edit.select_one('.edit-poster img')["src"]
    for rating in ("1", "4.5", "5"):
        r = post(f"/Movies/Edit/{mid}", {**data, "Id": mid, "Rated": rating})
        check(f"accept rating {rating}", r.status_code == 302)
    edit = get(f"/Movies/Edit/{mid}")
    check("retain uploaded poster after edit", edit.select_one('.edit-poster img')["src"] == old_src)
    check("uploaded PNG served", s.get(BASE + old_src, timeout=30).status_code == 200)
    check("antiforgery missing token rejected", s.post(BASE + "/Cart/Add", data={"id": mid}, timeout=30).status_code == 400)
    r = post("/Cart/Add", {"id": mid}, "/Cart")
    check("cart add", r.status_code == 302 and title in get("/Cart").get_text())
    post("/Cart/Update", {"id": mid, "quantity": 2}, "/Cart")
    cart = get("/Cart")
    check("cart quantity and total", cart.select_one('input[name="quantity"]')["value"] == "2" and "10.000" in cart.get_text())
    post("/Cart/Update", {"id": mid, "quantity": 99}, "/Cart")
    check("cart quantity clamped at 50", get("/Cart").select_one('input[name="quantity"]')["value"] == "50")
    post("/Cart/Remove", {"id": mid}, "/Cart")
    check("cart remove", get("/Cart").select_one('input[name="quantity"]') is None)
finally:
    if mid:
        post(f"/Movies/Delete/{mid}", {"id": mid})
        check("QA movie cleaned up", s.get(BASE + f"/Movies/Details/{mid}", timeout=30).status_code == 404)
    out = Path(__file__).with_name("QA_FINAL_20261128.json")
    out.write_text(json.dumps({"date": "2026-11-28", "target": BASE, "results": results}, ensure_ascii=False, indent=2), encoding="utf-8")
    print(json.dumps(results, ensure_ascii=True))

