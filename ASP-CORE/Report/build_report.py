import argparse
import json
from datetime import date
from pathlib import Path
from PIL import Image

from docx import Document
from docx.enum.section import WD_ORIENT, WD_SECTION
from docx.enum.table import WD_CELL_VERTICAL_ALIGNMENT, WD_TABLE_ALIGNMENT
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.oxml import OxmlElement
from docx.oxml.ns import qn
from docx.shared import Inches, Pt, RGBColor

REPORT = Path(__file__).resolve().parent
IMAGES = REPORT / "images"
COVER_FRAME = IMAGES / "cover_frame.png"
COVER_LOGO = IMAGES / "cover_ueh_logo.png"
OUT = REPORT / "BaiTap_CaNhan_ASP_CORE_LMS.docx"
REPORT_DATE = date(2026, 11, 28)
REPO_URL = "https://github.com/hoanghung5aa-arch/cai-tien-giao-dien-sach"
SOMEE_URL = "https://somee.com/FreeAspNetHosting.aspx"
LIVE_URL = "http://gocphimhung31241027221.somee.com"

INK, BODY, GREEN, NAVY = "151515", "26302C", "236B57", "17372F"
PALE, MINT, GRAY, RED, WHITE, BORDER = "F3F6F3", "EAF3ED", "63716B", "A84438", "FFFFFF", "D9D9D9"

# All 50 labels and captions correspond to the [Hình x.y] prompts in the current .NET 10 PDF.
FIGURES = [
    ("1.1", "Terminal chạy dotnet --version và dotnet --list-sdks, thấy dòng 10.0.xxx."),
    ("2.1", "Terminal sau dotnet new mvc, thấy template MVC tạo thành công và restore hoàn tất."),
    ("2.2", "Hộp thoại Windows HTTPS Security Warning với nút Yes."),
    ("2.3", "Trình duyệt hiện trang Welcome tại localhost:5083, URL nhìn thấy; không có tab Chrome, taskbar hoặc con trỏ. Terminal Now listening on được đính kèm."),
    ("2.4", "Program.cs với dòng options.UseSqlServer(connectionString) được đánh dấu tìm kiếm trong editor."),
    ("3.1", "Trang chủ Somee, khoanh Learn More của gói Free .NET Hosting."),
    ("3.2", "Trang MS SQL Databases có nút CREATE được khoanh."),
    ("3.3", "Form tạo database đã điền, khoanh nút CREATE EMPTY DATABASE."),
    ("3.4", "Connection details của database Somee; dùng Copy to Clipboard để lấy connection string, mật khẩu không hiển thị."),
    ("4.1", "appsettings.json có DefaultConnection; mật khẩu được che."),
    ("4.2", "Explorer hiển thị thư mục Data trước khi xóa Migrations theo hướng dẫn."),
    ("4.3", "Terminal sau migration có Build succeeded và Done; thấy thư mục Migrations."),
    ("4.4", "Trang Register đã điền thông tin."),
    ("4.5", "Trang Register confirmation có liên kết xác nhận."),
    ("4.6", "SSMS Connect to Server đã điền; mật khẩu được che."),
    ("4.7", "SSMS hiển thị bảng dbo.AspNet* và dữ liệu AspNetUsers."),
    ("5.1", "Explorer tạo Movie.cs trong thư mục Models."),
    ("5.2", "ApplicationDbContext.cs sau khi thêm DbSet<Movie> và DbSet<Genre>."),
    ("5.3", "Terminal sau scaffold, hiển thị Added Controller và các View."),
    ("5.4", "Lỗi database ban đầu tại /Movies trước khi thêm migration."),
    ("5.5", "Thư mục Migrations có hai migration và CreateTable cho Genres."),
    ("5.6", "SSMS hiển thị dbo.Genres và dbo.Movies."),
    ("5.7", "Trang /Movies dạng Index chưa có dữ liệu."),
    ("5.8", "Program.cs có route mặc định Movies và trang chủ sau khi chạy."),
    ("6.1", "Terminal sau lệnh scaffold Genre."),
    ("6.2", "Trang Genres/Create đã nhập Viễn Tưởng và trang Index Genres sau khi tạo."),
    ("7.1", "Explorer thấy ảnh dự phòng wwwroot/images/no-image.jpg theo hướng dẫn."),
    ("7.2", "Create.cshtml có enctype multipart/form-data và khối PictureUpload trong source hiện tại."),
    ("7.3", "Form Create đã điền và đã chọn tệp ảnh."),
    ("7.4", "Trang Index sau khi tạo, cột PicturePath hiện đường dẫn /images/….jpg."),
    ("7.5", "Trang Index hiển thị ảnh thay cho đường dẫn."),
    ("7.6", "Trang Index dạng lưới có ảnh, thông tin phim và nút Mua."),
    ("8.1", "Terminal sau migration có Done; SSMS thấy HoaDons và ChiTietHoaDons."),
    ("8.2", "Program.cs, khoanh AddSession và UseSession."),
    ("8.3", "Explorer thấy Extension/SessionExtensions.cs."),
    ("8.4", "Giỏ hàng hiện sản phẩm, số lượng và tổng tiền đúng."),
    ("8.5", "_LoginPartial.cshtml có các dòng kết xuất trạng thái Identity theo hướng dẫn."),
    ("8.6", "Menu hiện số lượng sản phẩm và tổng tiền giỏ hàng."),
    ("9.1", "Google App passwords sau khi tạo; che mật khẩu ứng dụng."),
    ("9.2", "Trang Checkout đã nhập thông tin giao hàng."),
    ("9.3", "Trang ThankYou xác nhận đơn đã lưu."),
    ("9.4", "SSMS có HoaDons, ChiTietHoaDons, ShippingDetails; thêm email nếu gửi thành công."),
    ("9.5", "Footer website hiển thị MSSV và họ tên."),
    ("10.1", "Terminal sau publish và Explorer hiển thị thư mục publish."),
    ("10.2", "Các tệp bên trong publish được chọn và publish.zip không có thư mục bọc."),
    ("10.3", "Trang Websites trên Somee có nút CREATE được khoanh."),
    ("10.4", "Form tạo website đã điền."),
    ("10.5", "File Manager đã chọn gói ZIP delta ngày 28/11/2026; bật ghi đè và sẵn sàng Upload and Extract."),
    ("10.6", "File Manager sau giải nén có web.config, wwwroot và các .dll ở gốc website."),
    ("10.7", "Website chạy trên Somee, thấy thanh địa chỉ và danh sách phim."),
]

CHAPTERS = {
    "1": "Cài SDK và công cụ",
    "2": "Tạo và chạy project",
    "3": "Tạo database trên Somee",
    "4": "Identity và kết nối dữ liệu",
    "5": "Movie model và scaffold",
    "6": "Quản lý thể loại",
    "7": "Poster và danh mục phim",
    "8": "Session và giỏ hàng",
    "9": "Checkout và email",
    "10": "Publish và triển khai",
}

SUPPLEMENTAL = [
    ("Muc14_GitHub.png", "Repository GitHub và cấu trúc thư mục ASP-CORE; ảnh chụp trước commit cập nhật báo cáo này."),
    ("Muc14_Hosting.png", "Somee Summary hiển thị website đang chạy, URL public và môi trường ASP.NET Core."),
    ("Muc14_FileManager.png", "File Manager xác nhận giải nén ba tệp, 523,77 KB; DLL ứng dụng và wwwroot ở gốc website."),
    ("Muc14_Database.png", "Truy vấn chỉ đọc trên SQL Server: mười phim, sáu thể loại, bốn hóa đơn, bốn chi tiết, bốn địa chỉ và một tài khoản đã xác nhận. Đơn #4 có tổng 159.000 VNĐ."),
    ("Hinh_Audit_DanhMuc_20261128.png", "Danh mục website live với poster phim thật; ảnh giữ URL theo đề, không có tab Chrome, taskbar hay con trỏ."),
    ("Hinh_Audit_PosterThat_20261128.png", "Chi tiết phim Avengers: Doomsday với poster thật đang hiển thị trên bản deploy."),
    ("Hinh_Audit_TheLoai_20261128.png", "Danh sách thể loại trên website live sau bản deploy mới."),
    ("Muc14_TaiKhoanXacNhan.png", "Trang xác nhận đăng ký live cho biết email tài khoản đã được xác nhận."),
    ("Muc14_EmailDonHang.png", "Email trong Gmail Inbox xác nhận đơn thử nghiệm #4 cho Avengers: Doomsday với tổng tiền 159.000 VNĐ."),
    ("Muc14_DangNhapThanhCong.png", "Đăng nhập thành công trên website; menu hiện Tài khoản và Đăng xuất."),
    ("Muc14_LocalHome.png", "Trang danh mục local tại 127.0.0.1:5080, hiển thị phần giới thiệu và đầu danh sách poster."),
    ("Muc14_ChinhSuaPhim.png", "Form chỉnh sửa phim local có poster chính thức hiển thị đầy đủ."),
]

# This one existing screenshot was already reviewed as clean and matching. Add later captures
# through the explicit JSON manifest described by --help; a file's presence alone is not proof.
PREVALIDATED_FIGURES = {"7.6": "Hinh_7_6.png"}
DEFAULT_EVIDENCE_MANIFEST = IMAGES / "evidence_manifest.json"
SUPPORTED_IMAGE_SUFFIXES = {".png", ".jpg", ".jpeg", ".gif", ".bmp", ".tif", ".tiff"}



def font(run, size=10.1, bold=False, color=BODY, italic=False):
    run.font.name = "Arial"
    run._element.get_or_add_rPr().rFonts.set(qn("w:ascii"), "Arial")
    run._element.get_or_add_rPr().rFonts.set(qn("w:hAnsi"), "Arial")
    run.font.size = Pt(size)
    run.font.bold = bold
    run.font.italic = italic
    run.font.color.rgb = RGBColor.from_string(color)


def paragraph(doc, text="", size=10.1, bold=False, color=BODY, align=None, after=5, before=0, keep=False):
    p = doc.add_paragraph()
    if text:
        font(p.add_run(text), size, bold, color)
    if align is not None:
        p.alignment = align
    p.paragraph_format.space_before = Pt(before)
    p.paragraph_format.space_after = Pt(after)
    p.paragraph_format.line_spacing = 1.12
    p.paragraph_format.keep_with_next = keep
    return p


def heading(doc, text, level=1):
    p = doc.add_paragraph(style=f"Heading {level}")
    font(p.add_run(text), {1: 16, 2: 12.5, 3: 11}[level], True, INK)
    p.paragraph_format.space_before = Pt(14 if level == 1 else 9)
    p.paragraph_format.space_after = Pt(5)
    p.paragraph_format.keep_with_next = True
    return p


def bullet(doc, lead, text):
    p = doc.add_paragraph(style="List Bullet")
    p.paragraph_format.left_indent = Inches(.23)
    p.paragraph_format.first_line_indent = Inches(-.13)
    p.paragraph_format.space_after = Pt(3)
    font(p.add_run(lead), 10, True, NAVY)
    font(p.add_run(text), 10, False, BODY)
    return p


def hyperlink(p, label, target):
    rid = p.part.relate_to(
        target,
        "http://schemas.openxmlformats.org/officeDocument/2006/relationships/hyperlink",
        is_external=True,
    )
    link = OxmlElement("w:hyperlink")
    link.set(qn("r:id"), rid)
    run = OxmlElement("w:r")
    props = OxmlElement("w:rPr")
    color_node = OxmlElement("w:color")
    color_node.set(qn("w:val"), GREEN)
    props.append(color_node)
    underline = OxmlElement("w:u")
    underline.set(qn("w:val"), "single")
    props.append(underline)
    run.append(props)
    text = OxmlElement("w:t")
    text.text = label
    run.append(text)
    link.append(run)
    p._p.append(link)


def field(p, instruction):
    run = p.add_run()
    begin = OxmlElement("w:fldChar")
    begin.set(qn("w:fldCharType"), "begin")
    instr = OxmlElement("w:instrText")
    instr.set(qn("xml:space"), "preserve")
    instr.text = instruction
    separate = OxmlElement("w:fldChar")
    separate.set(qn("w:fldCharType"), "separate")
    value = OxmlElement("w:t")
    value.text = "1"
    end = OxmlElement("w:fldChar")
    end.set(qn("w:fldCharType"), "end")
    run._r.extend([begin, instr, separate, value, end])


def cell_borders(cell):
    props = cell._tc.get_or_add_tcPr()
    borders = props.first_child_found_in("w:tcBorders")
    if borders is None:
        borders = OxmlElement("w:tcBorders")
        props.append(borders)
    for side in ("top", "left", "bottom", "right", "insideH", "insideV"):
        node = borders.find(qn(f"w:{side}"))
        if node is None:
            node = OxmlElement(f"w:{side}")
            borders.append(node)
        node.set(qn("w:val"), "single")
        node.set(qn("w:sz"), "5")
        node.set(qn("w:space"), "0")
        node.set(qn("w:color"), BORDER)


def cell_padding(cell, top=75, side=90, bottom=75):
    props = cell._tc.get_or_add_tcPr()
    margins = props.first_child_found_in("w:tcMar")
    if margins is None:
        margins = OxmlElement("w:tcMar")
        props.append(margins)
    for key, value in (("top", top), ("start", side), ("bottom", bottom), ("end", side)):
        node = margins.find(qn(f"w:{key}"))
        if node is None:
            node = OxmlElement(f"w:{key}")
            margins.append(node)
        node.set(qn("w:w"), str(value))
        node.set(qn("w:type"), "dxa")


def table(doc, headers, rows, widths, size=8.5, status_col=None):
    t = doc.add_table(rows=1, cols=len(headers))
    t.alignment = WD_TABLE_ALIGNMENT.CENTER
    t.autofit = False
    t.style = "Table Grid"
    header_props = t.rows[0]._tr.get_or_add_trPr()
    header_props.append(OxmlElement("w:tblHeader"))
    for col, label in enumerate(headers):
        cell = t.rows[0].cells[col]
        cell.width = Inches(widths[col])
        cell.vertical_alignment = WD_CELL_VERTICAL_ALIGNMENT.CENTER
        cell_borders(cell)
        cell_padding(cell, 90, 100, 90)
        shade = OxmlElement("w:shd")
        shade.set(qn("w:fill"), NAVY)
        cell._tc.get_or_add_tcPr().append(shade)
        p = cell.paragraphs[0]
        p.paragraph_format.space_after = Pt(0)
        font(p.add_run(label), size, True, WHITE)
    for index, values in enumerate(rows):
        row = t.add_row()
        row._tr.get_or_add_trPr().append(OxmlElement("w:cantSplit"))
        for col, value in enumerate(values):
            cell = row.cells[col]
            cell.width = Inches(widths[col])
            cell.vertical_alignment = WD_CELL_VERTICAL_ALIGNMENT.CENTER
            cell_borders(cell)
            cell_padding(cell)
            if index % 2:
                shade = OxmlElement("w:shd")
                shade.set(qn("w:fill"), PALE)
                cell._tc.get_or_add_tcPr().append(shade)
            p = cell.paragraphs[0]
            p.paragraph_format.space_after = Pt(0)
            p.paragraph_format.line_spacing = 1.04
            text = str(value)
            pending = text.lower().startswith(("chưa", "đang chờ", "cần xác minh"))
            font(p.add_run(text), size, col == 0 or col == status_col,
                 RED if col == status_col and pending else (GREEN if col == status_col else BODY))
    p = doc.add_paragraph()
    p.paragraph_format.space_after = Pt(1)
    return t


def add_embedded_images(doc, image_entries):
    if not image_entries:
        return
    sec = doc.add_section(WD_SECTION.NEW_PAGE)
    sec.orientation = WD_ORIENT.LANDSCAPE
    sec.page_width, sec.page_height = Inches(11.69), Inches(8.27)
    sec.top_margin = sec.bottom_margin = Inches(.42)
    sec.left_margin = sec.right_margin = Inches(.58)
    sec.header_distance = sec.footer_distance = Inches(.18)
    heading(doc, "Ảnh minh chứng", 1)
    paragraph(doc, "Ảnh minh chứng được chèn trực tiếp vào báo cáo; mã hình và mô tả được đặt ngay dưới mỗi ảnh.", 9, color=GRAY, after=7)
    for index, (label, desc, path) in enumerate(image_entries):
        if index and label != "Hình 3.3 tiếp":
            doc.add_page_break()
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.CENTER
        p.paragraph_format.space_after = Pt(2)
        p.paragraph_format.keep_with_next = True
        with Image.open(path) as figure_image:
            aspect = figure_image.width / figure_image.height
            raster_width = figure_image.width / 100
        maximum_height = 5.6 if index == 0 else 6.1
        figure_width = min(9.9, maximum_height * aspect, raster_width)
        shape = p.add_run().add_picture(str(path), width=Inches(figure_width))
        shape._inline.docPr.set("descr", f"[{label}]. {desc}")
        paragraph(doc, f"{label}. {desc}", 9.5, True, INK,
                  WD_ALIGN_PARAGRAPH.CENTER, after=4, keep=False)


def load_figure_evidence(manifest_path):
    """Load only numbered screenshots explicitly marked validated in the manifest.

    Manifest schema: {"version": 1, "figures": [{"number": "7.6",
    "file": "Hinh_7_6.png", "validated": true}]}. Paths are relative to
    Report/images and must stay inside that directory.
    """
    figure_descriptions = dict(FIGURES)
    evidence = {}
    for number, filename in PREVALIDATED_FIGURES.items():
        path = IMAGES / filename
        if path.is_file():
            evidence[number] = path

    if not manifest_path.is_file():
        return evidence

    with manifest_path.open("r", encoding="utf-8") as source:
        manifest = json.load(source)
    if not isinstance(manifest, dict) or manifest.get("version") != 1:
        raise ValueError(f"Unsupported evidence manifest schema: {manifest_path}")
    entries = manifest.get("figures", [])
    if not isinstance(entries, list):
        raise ValueError("Evidence manifest 'figures' must be a list")

    seen = set()
    image_root = IMAGES.resolve()
    for entry in entries:
        if not isinstance(entry, dict):
            raise ValueError("Each manifest figure must be an object")
        number = str(entry.get("number", ""))
        if number not in figure_descriptions:
            raise ValueError(f"Unknown figure number in evidence manifest: {number!r}")
        if number in seen:
            raise ValueError(f"Duplicate figure number in evidence manifest: {number}")
        seen.add(number)
        if entry.get("validated") is not True:
            continue
        filename = entry.get("file")
        if not isinstance(filename, str) or not filename.strip():
            raise ValueError(f"Validated figure {number} needs a non-empty 'file'")
        relative_path = Path(filename)
        if relative_path.is_absolute():
            raise ValueError(f"Figure paths must be relative to Report/images: {filename}")
        path = (IMAGES / relative_path).resolve()
        if not path.is_relative_to(image_root):
            raise ValueError(f"Figure path escapes Report/images: {filename}")
        if path.suffix.lower() not in SUPPORTED_IMAGE_SUFFIXES:
            raise ValueError(f"Unsupported image type for figure {number}: {path.suffix}")
        if not path.is_file():
            raise FileNotFoundError(f"Validated figure image does not exist: {path}")
        evidence[number] = path

    return evidence


def parse_args():
    parser = argparse.ArgumentParser(description="Build the ASP.NET Core report with embedded evidence.")
    parser.add_argument(
        "--evidence-manifest",
        type=Path,
        default=DEFAULT_EVIDENCE_MANIFEST,
        help=(
            "JSON file with version=1 and figures[] entries containing number, file, "
            "and validated=true. Image paths are relative to Report/images. "
            "Default: Report/images/evidence_manifest.json."
        ),
    )
    return parser.parse_args()


def add_cover_frame(doc):
    """Place the retained template's page border behind the first-page text."""
    if not COVER_FRAME.is_file():
        return
    p = doc.add_paragraph()
    p.paragraph_format.space_before = Pt(0)
    p.paragraph_format.space_after = Pt(0)
    p.paragraph_format.line_spacing = Pt(1)
    shape = p.add_run().add_picture(str(COVER_FRAME), width=Inches(7.95), height=Inches(11.35))
    anchor = shape._inline
    anchor.tag = qn("wp:anchor")
    for name, value in {
        "distT": "0", "distB": "0", "distL": "114300", "distR": "114300",
        "simplePos": "0", "relativeHeight": "251659264", "behindDoc": "1",
        "locked": "0", "layoutInCell": "1", "allowOverlap": "1",
    }.items():
        anchor.set(name, value)
    simple = OxmlElement("wp:simplePos")
    simple.set("x", "0")
    simple.set("y", "0")
    horizontal = OxmlElement("wp:positionH")
    horizontal.set("relativeFrom", "margin")
    horizontal_align = OxmlElement("wp:align")
    horizontal_align.text = "center"
    horizontal.append(horizontal_align)
    vertical = OxmlElement("wp:positionV")
    vertical.set("relativeFrom", "page")
    vertical_offset = OxmlElement("wp:posOffset")
    vertical_offset.text = "155448"
    vertical.append(vertical_offset)
    wrap = OxmlElement("wp:wrapNone")
    anchor.insert(0, simple)
    anchor.insert(1, horizontal)
    anchor.insert(2, vertical)
    anchor.insert(list(anchor).index(anchor.find(qn("wp:docPr"))), wrap)
    shape._inline.docPr.set("descr", "Decorative teal frame retained from the BTVN5 cover")


doc = Document()
sec = doc.sections[0]
sec.page_width, sec.page_height = Inches(8.27), Inches(11.69)
sec.top_margin, sec.bottom_margin = Inches(.72), Inches(.7)
sec.left_margin = sec.right_margin = Inches(.82)
sec.header_distance = sec.footer_distance = Inches(.28)
normal = doc.styles["Normal"]
normal.font.name = "Arial"
normal.font.size = Pt(10.1)
normal.font.color.rgb = RGBColor.from_string(BODY)
normal._element.rPr.rFonts.set(qn("w:ascii"), "Arial")
normal._element.rPr.rFonts.set(qn("w:hAnsi"), "Arial")
normal.paragraph_format.space_after = Pt(5)
normal.paragraph_format.line_spacing = 1.12
for name, size in (("Heading 1", 16), ("Heading 2", 12.5), ("Heading 3", 11)):
    style = doc.styles[name]
    style.font.name = "Arial"
    style.font.size = Pt(size)
    style.font.bold = True
    style.font.color.rgb = RGBColor.from_string(INK)
    style._element.rPr.rFonts.set(qn("w:ascii"), "Arial")
    style._element.rPr.rFonts.set(qn("w:hAnsi"), "Arial")
title_style = doc.styles["Title"]
title_style.font.name = "Arial"
title_style.font.color.rgb = RGBColor.from_string(INK)
title_style.font.bold = True
title_style._element.rPr.rFonts.set(qn("w:ascii"), "Arial")
title_style._element.rPr.rFonts.set(qn("w:hAnsi"), "Arial")
title_ppr = title_style._element.pPr
if title_ppr is not None:
    border = title_ppr.find(qn("w:pBdr"))
    if border is not None:
        title_ppr.remove(border)

header = sec.header.paragraphs[0]
header.alignment = WD_ALIGN_PARAGRAPH.RIGHT
font(header.add_run("LAB BACK-END  |  ASP.NET CORE MVC  |  31241027221"), 7.7, True, INK)
footer = sec.footer.paragraphs[0]
footer.alignment = WD_ALIGN_PARAGRAPH.CENTER
font(footer.add_run("Nguyễn Hoàng Hưng  |  Bài tập cá nhân  |  Trang "), 8, False, GRAY)
field(footer, "PAGE")

args = parse_args()
figure_evidence = load_figure_evidence(args.evidence_manifest)
manifest_details = {}
if args.evidence_manifest.is_file():
    manifest_details = {
        str(item["number"]): item
        for item in json.loads(args.evidence_manifest.read_text(encoding="utf-8"))["figures"]
        if item.get("validated") is True
    }
available = []
for number, desc in FIGURES:
    if number not in figure_evidence:
        continue
    details = manifest_details.get(number, {})
    caption = details.get("caption") or details.get("note") or desc
    available.append((f"Hình {number}", caption, figure_evidence[number]))
    for companion in details.get("companions", []):
        companion_path = (IMAGES / companion["file"]).resolve()
        if not companion_path.is_relative_to(IMAGES.resolve()) or not companion_path.is_file():
            raise ValueError(f"Invalid companion figure path for {number}")
        available.append((f"Hình {number} tiếp", companion["caption"], companion_path))
available_supplemental = [(f"Mục 14", desc, IMAGES / name) for name, desc in SUPPLEMENTAL if (IMAGES / name).is_file()]
partial_figures = [number for number, item in manifest_details.items() if item.get("coverage") == "partial"]

# Cover
add_cover_frame(doc)
paragraph(doc, "", after=8)
paragraph(doc, "ĐẠI HỌC KINH TẾ THÀNH PHỐ HỒ CHÍ MINH", 11.5, True, INK, WD_ALIGN_PARAGRAPH.CENTER, after=4)
paragraph(doc, "TRƯỜNG CÔNG NGHỆ VÀ THIẾT KẾ", 10.5, True, INK, WD_ALIGN_PARAGRAPH.CENTER, after=4)
paragraph(doc, "KHOA CÔNG NGHỆ THÔNG TIN KINH DOANH", 9.5, color=GRAY, align=WD_ALIGN_PARAGRAPH.CENTER, after=36)
if COVER_LOGO.is_file():
    logo_paragraph = doc.add_paragraph()
    logo_paragraph.alignment = WD_ALIGN_PARAGRAPH.CENTER
    logo_paragraph.paragraph_format.space_after = Pt(8)
    logo = logo_paragraph.add_run().add_picture(str(COVER_LOGO), width=Inches(2.85))
    logo._inline.docPr.set("descr", "UEH University logo retained from the BTVN5 cover")
p = doc.add_paragraph(style="Title")
p.alignment = WD_ALIGN_PARAGRAPH.CENTER
font(p.add_run("BÀI TẬP CÁ NHÂN\nBACK-END ASP.NET CORE MVC"), 24, True, INK)
p.paragraph_format.space_after = Pt(12)
paragraph(doc, "LAB BACK-END  |  ASP.NET CORE MVC .NET 10", 12, True, GREEN, WD_ALIGN_PARAGRAPH.CENTER, after=24)

meta = [
    ("Sinh viên", "Nguyễn Hoàng Hưng"),
    ("MSSV", "31241027221"),
    ("Lớp", "K50-EE0002"),
    ("Học phần", "Phát triển ứng dụng thương mại điện tử"),
    ("Mã học phần", "26C1INF50902702"),
    ("Giảng viên", "TS. Nguyễn Mạnh Tuấn"),
    ("Ngày báo cáo", REPORT_DATE.strftime("%d/%m/%Y")),
]
t = doc.add_table(rows=0, cols=2)
t.alignment = WD_TABLE_ALIGNMENT.CENTER
t.autofit = False
for label, value in meta:
    cells = t.add_row().cells
    cells[0].width, cells[1].width = Inches(1.55), Inches(4.55)
    for c in cells:
        cell_borders(c)
        cell_padding(c, 105, 130, 105)
        c.vertical_alignment = WD_CELL_VERTICAL_ALIGNMENT.CENTER
    shade = OxmlElement("w:shd")
    shade.set(qn("w:fill"), MINT)
    cells[0]._tc.get_or_add_tcPr().append(shade)
    font(cells[0].paragraphs[0].add_run(label), 9.8, True, NAVY)
    font(cells[1].paragraphs[0].add_run(value), 9.8)
paragraph(doc, "Website cửa hàng phim Góc Phim Hưng", 10, True, GREEN, WD_ALIGN_PARAGRAPH.CENTER, after=5, before=13)
paragraph(doc, "Repository hiện có: hoanghung5aa-arch/cai-tien-giao-dien-sach", 9.2, color=GRAY, align=WD_ALIGN_PARAGRAPH.CENTER, after=3)
paragraph(doc, "Bản mới đã deploy lên Somee và các trang chính đã được mở kiểm tra ngày 28/11/2026.", 9.2, True, GREEN, WD_ALIGN_PARAGRAPH.CENTER, after=0)
doc.add_page_break()

# Table of contents field; Word refreshes it when the report is opened.
paragraph(doc, "Mục lục", 16, True, INK, after=10, keep=True)
toc = doc.add_paragraph()
toc.paragraph_format.space_after = Pt(4)
field(toc, 'TOC \\o "1-2" \\h \\z \\u')
doc.add_page_break()

# Summary
heading(doc, "Tóm tắt kết quả")
paragraph(doc, "Bài tập xây dựng cửa hàng phim Góc Phim Hưng bằng ASP.NET Core MVC .NET 10, Entity Framework Core và SQL Server. Source hiện có Identity, quản lý phim/thể loại, tải poster, giỏ hàng trong session, checkout, lưu đơn hàng và dịch vụ email. Kiểm tra build Release thành công với SDK 10.0.404; Microsoft.NETCore.App và Microsoft.AspNetCore.App runtime là 10.0.15.")
paragraph(doc, "Bản mới đã được triển khai lên Somee ngày 28/11/2026 tại " + LIVE_URL + ". Kiểm tra trực tiếp trên website xác nhận trang danh mục, thể loại, chi tiết phim, đăng nhập thành công và đăng ký đều hiển thị; footer còn đúng MSSV. Giao diện dùng font Noto Sans nhúng cho tiếng Việt và poster quảng bá chính thức được liệt kê nguồn trong báo cáo. Đơn thử nghiệm #4 trị giá 159.000 VNĐ đã được lưu sau triển khai; email xác nhận có trong Inbox và Sent của Gmail ngày 28/11/2026. Trang xác nhận cũng cho biết email tài khoản đã được xác nhận. Build Release của source hiện tại đã thành công với SDK 10.0.404.")
table(doc, ["Hạng mục", "Tình trạng"], [
    ("Project .NET 10", "Target net10.0; build Release đã được kiểm tra xác nhận thành công."),
    ("Phim và thể loại", "Migration khởi tạo năm phim và ba thể loại mẫu. Website live hiện hiển thị mười phim và sáu thể loại; truy vấn SQL hiện thời xác nhận đúng mười Movies và sáu Genres."),
    ("Upload poster", "Source kiểm tra loại tệp, dung lượng và chữ ký; ảnh là file trên đĩa. HTTP QA xác nhận upload PNG hợp lệ và từ chối chữ ký JPG giả. Đã chụp form chọn poster thật và kết quả lưu trên bản kiểm thử cục bộ."),
    ("Cart và checkout", "Ảnh giỏ hàng hiển thị Avengers: Doomsday với tổng 159.000 VNĐ; ảnh checkout và ThankYou ghi nhận đơn #4 trị giá 159.000 VNĐ."),
    ("SQL Server Somee", "Source có schema và migration EF Core. Truy vấn chỉ đọc xác nhận bảng Identity, Movies, Genres, HoaDons, ChiTietHoaDons, ShippingDetails; có ảnh kết quả hiện thời."),
    ("Website public", "Bản mới đã deploy ngày 28/11/2026; danh mục, thể loại, chi tiết phim, đăng nhập và đăng ký đã hiển thị khi kiểm tra trực tiếp."),
    ("Ảnh theo đề", f"{len(figure_evidence)} / {len(FIGURES)} vị trí có ảnh nhúng; {len(available_supplemental)} ảnh bổ sung mục 14 được chèn trực tiếp. Một số ảnh là ảnh lịch sử hoặc tái hiện cục bộ, ghi rõ trong chú thích."),
], [1.55, 4.65], size=8.6, status_col=1)

# Scope and section 14/15.
heading(doc, "Phạm vi và tài liệu đối chiếu")
paragraph(doc, "PDF bài tập ASP.NET Core MVC .NET 10 dài 41 trang là căn cứ chính cho yêu cầu source, các bước, 50 vị trí [Hình x.y], mục 14–15 và rubric 100 điểm. Hướng dẫn cũ dùng .NET 5; transcript video thao tác ASP.NET MVC 5/.NET Framework trong Visual Studio. Hai tài liệu cũ chỉ giúp đối chiếu quy trình, không thay cho phiên bản package, Program.cs, route, session, lưu ảnh và publish trong source .NET 10 này.")
paragraph(doc, "Ma trận dưới đây có ảnh nhúng cho đủ 50 vị trí theo PDF. Chú thích phân biệt ảnh tái hiện, ảnh lịch sử và website live; hiện không có mục nào bị đánh dấu thiếu ảnh. Các ảnh website giữ thanh địa chỉ khi đề yêu cầu URL; không có tab Chrome, taskbar hoặc con trỏ. Hình 2.3 dùng ảnh Welcome local có URL 127.0.0.1:5083 và ảnh terminal Now listening on đi kèm. Hình 6.1 dùng ảnh PowerShell thật sau scaffold Genre, có controller và đủ năm View.")
heading(doc, "Đối chiếu yêu cầu báo cáo và nộp bài")
table(doc, ["Yêu cầu mục 14–15", "Nội dung và trạng thái"], [
    ("Trang đầu", "Có họ tên, MSSV, lớp, học phần, tên bài, ngày báo cáo và URL repository."),
    ("Môi trường", "Windows 11 Home Single Language build 26300; SDK 10.0.404; runtime 10.0.15. VS Code 1.147.0."),
    ("Thiết kế dữ liệu", "Mô tả các model và quan hệ; ảnh truy vấn database được chèn ở mục minh chứng."),
    ("Chức năng", "Cloud QA bản trước xác nhận CRUD, upload/giữ poster, giá 5.000/4.999, cart, antiforgery và checkout. Kiểm thử mới có 20 kiểm tra HTTP đạt: chuẩn hóa Title/Summary, các biên giá/điểm/ngày/thể loại, upload, cart và antiforgery."),
    ("Triển khai", "Bản mới đã deploy ngày 28/11/2026; trang danh mục, thể loại, phim, đăng nhập và đăng ký hiển thị."),
    ("Khó khăn và kết luận", "Nêu khác biệt tài liệu .NET 5/MVC5, giới hạn runtime hosting, quy trình SMTP và các hướng cải tiến tiếp theo."),
    ("Ảnh mục 14", "Bảng ảnh bổ sung riêng cho cấu trúc VS Code, local, CRUD, upload, database, cart, checkout, hosting và GitHub."),
    ("GitHub, README, LMS", "Source, poster/font, tài liệu nguồn và ảnh minh chứng được đồng bộ trong repository; Word nhúng ảnh trực tiếp. URL LMS sinh viên tự nộp."),
], [1.55, 4.65], size=8.3)

# Environment.
heading(doc, "Môi trường thực hiện")
table(doc, ["Thành phần", "Thông tin ghi nhận"], [
    ("Hệ điều hành", "Windows 11 Home Single Language, build 26300."),
    ("IDE", "Visual Studio Code 1.147.0."),
    ("SDK", ".NET SDK 10.0.404; global.json yêu cầu 10.0.100 và rollForward latestFeature."),
    ("Runtime", "Microsoft.NETCore.App 10.0.15 và Microsoft.AspNetCore.App 10.0.15."),
    ("ORM/database", "Entity Framework Core 10.0.15 và SQL Server."),
    ("Somee", "SQL Server 2025 Express database PTUDW31241027221; năm migration đã áp dụng. Trước khi đồng bộ đã sao lưu thành công thành before-film-catalog-20261128.bak. Truy vấn sau xác nhận UseOfficialFilmCatalog (10.0.15). Vì năm phim và thể loại đích đã đúng, giao dịch có điều kiện chỉ thêm migration history, không sửa dữ liệu catalog. Website live: " + LIVE_URL + "; hiển thị mười phim và sáu thể loại."),
    ("Tooling", "dotnet-ef 10.0.15; dotnet-aspnet-codegenerator 10.0.3."),
    ("PATH", "SDK nằm trong %USERPROFILE%\\.dotnet; terminal mặc định không tìm thấy dotnet. Dùng đường dẫn đầy đủ hoặc thêm SDK folder vào PATH của phiên PowerShell."),
], [1.5, 4.7], size=8.6)
paragraph(doc, "Build Release được kiểm tra xác nhận bằng C:\\Users\\Admin\\.dotnet\\dotnet.exe. Ứng dụng đã publish và được kiểm tra trên Somee. Bản MVC mới tạo dùng để tái hiện và chụp Hình 2.3 chạy tại http://127.0.0.1:5083; đây là trang Welcome ban đầu, đúng bước kiểm tra template trước khi bổ sung tính năng phim.", 9.1, color=GRAY)

# Project design and personal work.
heading(doc, "Thiết kế và phần điều chỉnh cá nhân")
paragraph(doc, "Website mang tên Góc Phim Hưng, dùng nội dung tiếng Việt, thương hiệu và danh mục phim riêng. Thay MSSV/họ tên ở footer là một yêu cầu trong đề, nhưng phần cá nhân hóa còn thể hiện ở nội dung, poster, cách trình bày và luồng mua hàng.")
bullet(doc, "Quan hệ thể loại. ", "Mỗi Movie tham chiếu đúng một Genre qua GenreId và khóa ngoại. GenreExists kiểm tra id hợp lệ; unique index ngăn tên trùng. Đây là quan hệ một-nhiều chuẩn hóa, không lưu chuỗi thể loại tự do.")
bullet(doc, "Ràng buộc Movie. ", "Title dài 3–200; Summary 3–1.200; Price từ 5.000 VNĐ; Rated dùng double trong 1–5; ReleaseDate phải ở tương lai; tên thể loại tối đa 200 ký tự.")
bullet(doc, "Chống POST giả mạo. ", "Các POST thay đổi dữ liệu trong phim/thể loại, cart và checkout dùng ValidateAntiForgeryToken.")
bullet(doc, "Giá do server xác nhận. ", "Checkout tải lại tiêu đề, ảnh và giá từ database, không dùng giá session làm nguồn cuối. Số lượng đơn hàng được giới hạn 1–50.")
bullet(doc, "Transaction đơn hàng. ", "HoaDon, ChiTietHoaDon và ShippingDetail được lưu trong một transaction. Một hóa đơn có nhiều dòng chi tiết; ShippingDetail liên hệ một-một với HoaDon.")
bullet(doc, "Ảnh lưu riêng. ", "SQL chỉ lưu PicturePath; poster nằm dưới wwwroot. Database không tự đồng bộ file. Khi backup/chuyển host cần giữ cả các tệp ảnh.")
bullet(doc, "SMTP sau commit. ", "Email đơn thử nghiệm #4 đã có trong Inbox và Sent sau khi triển khai; đơn 159.000 VNĐ được lưu trước khi gửi mail. Trang xác nhận cho biết email tài khoản đã được xác nhận; ảnh menu Tài khoản/Đăng xuất xác nhận đăng nhập thành công. Checkout lưu đơn trước rồi mới thử SMTP; lỗi gửi được ghi log và không rollback đơn.")

heading(doc, "Thiết kế dữ liệu")
table(doc, ["Model/bảng", "Vai trò và quan hệ"], [
    ("IdentityUser / AspNetUsers", "Tài khoản ASP.NET Core Identity; yêu cầu xác nhận email."),
    ("Genre", "Tên tối đa 200, unique; một Genre có nhiều Movie."),
    ("Movie", "Thông tin phim, giá và PicturePath; mỗi phim có một GenreId."),
    ("HoaDon", "Thời gian, người mua và tổng tiền; một hóa đơn có nhiều chi tiết."),
    ("ChiTietHoaDon", "MovieId, số lượng và đơn giá tại thời điểm đặt; tham chiếu hóa đơn và phim."),
    ("ShippingDetail", "Người nhận và ngày giao; HoaDonId duy nhất tạo quan hệ một-một."),
], [1.6, 4.6], size=8.5)
paragraph(doc, "Migration có năm phim và ba thể loại mẫu làm dữ liệu khởi đầu; poster quảng bá thật có nguồn ghi bên dưới. SQL Somee xác nhận mười phim và sáu thể loại, trong đó có năm phim bổ sung The Bluey Movie, Frozen 3, Shrek 5, Incredibles 3 và Violent Night 2. Ảnh thao tác thêm phim và thể loại được chụp riêng trên database cục bộ để minh họa nhập liệu; số lượng live được đối chiếu bằng truy vấn SQL.", 9.2)

heading(doc, "Quá trình thực hiện")
bullet(doc, "Chốt target. ", "Dùng đề .NET 10, global.json và tool manifest ghim dotnet-ef 10.0.15; không áp dụng nguyên lệnh .NET 5/MVC5.")
bullet(doc, "Mô hình hóa dữ liệu. ", "Tạo Identity context, Movie, Genre, HoaDon, ChiTietHoaDon, ShippingDetail, khóa ngoại và migration EF Core.")
bullet(doc, "Xây dựng tính năng. ", "Movies/Genres cung cấp CRUD và lọc/tìm; MovieImageStorage kiểm tra và lưu poster; CartController giữ cart trong session.")
bullet(doc, "Hoàn thiện đặt hàng. ", "Checkout kiểm tra input, tải lại giá sản phẩm, lưu hóa đơn/chi tiết/địa chỉ trong transaction rồi thử gửi email.")
bullet(doc, "Chuẩn bị triển khai. ", "Dùng secrets ngoài source; sau publish kiểm tra web.config/wwwroot ở gốc ZIP và thử URL thật trước khi ghi hoàn tất.")

# Results and current QA.
heading(doc, "Kiểm tra và tình trạng hiện tại")
table(doc, ["Kiểm tra", "Kết quả ghi nhận", "Việc còn lại"], [
    ("Build Release", "Kiểm tra pass với SDK 10.0.404.", "Ảnh SDK 10.0.404 đã chèn ở Hình 1.1."),
    ("Create/Edit phim", "Cloud QA bản trước đã pass CRUD; Price 5.000 được chấp nhận và 4.999 bị từ chối. Kiểm thử HTTP ngày 28/11 xác nhận Title/Summary sau Trim không đủ ba ký tự bị từ chối; Release build pass.", "Đã kiểm thử runtime chuỗi có khoảng trắng."),
    ("Database Somee", "Bản triển khai hiện chạy; truy vấn xác nhận mười phim, sáu thể loại, bốn hóa đơn và một tài khoản đã xác nhận. Bản sao lưu trước migration thành công; migration thứ năm đã được ghi nhận sau khi đối chiếu target.", "Dữ liệu phim/thể loại production không bị sửa trong thao tác migration."),
    ("Dữ liệu phim/thể loại", "Website live có mười phim và sáu thể loại; truy vấn SQL xác nhận năm phim bổ sung: The Bluey Movie, Frozen 3, Shrek 5, Incredibles 3 và Violent Night 2. Ảnh local ghi nhận thao tác nhập phim và thể loại.", "Đã đối chiếu số lượng, tên và ảnh thao tác; nguồn dữ liệu live không lưu lịch sử người nhập."),
    ("Upload poster", "Cloud QA pass upload poster và giữ ảnh sau khi sửa thông tin phim.", "Đã chèn form chọn tệp và kết quả lưu poster cục bộ."),
    ("Cart và checkout", "Cloud QA pass Add/Update/Remove; thiếu ngày giao bị từ chối; đơn hợp lệ lưu và cart được xóa. Ảnh mới ghi nhận cart, checkout và ThankYou cho đơn #4 trị giá 159.000 VNĐ.", "Đã có ảnh form chỉnh sửa và truy vấn SQL; ảnh các bước lịch sử xem ma trận."),
    ("SMTP và Identity", "Email đơn #3 và #4 có trong Inbox và Sent; #4 trị giá 159.000 VNĐ. Trang xác nhận cho biết email tài khoản đã xác nhận; ảnh menu xác nhận đăng nhập thành công.", "Đã xác minh tài khoản và gửi email thực tế."),
    ("Somee .NET 10", "Bản mới đã deploy ngày 28/11/2026; danh mục, thể loại, phim, đăng nhập và đăng ký đã hiển thị.", "Ảnh truy vấn SQL và form chỉnh sửa đã bổ sung; ảnh lịch sử xem ma trận."),
    ("GitHub", "Source, poster/font, nguồn poster và ảnh thể loại đã có trên nhánh main; bản cập nhật này đồng bộ thêm migration an toàn, minh chứng và Word.", "LMS cần sinh viên nộp URL."),
], [1.18, 2.65, 2.37], size=7.8)

# Extra evidence required by section 14.
heading(doc, "Ảnh bổ sung theo mục 14")
paragraph(doc, f"Có {len(available_supplemental)} ảnh bổ sung từ website live và môi trường cục bộ; ảnh không có tab Chrome, taskbar hay con trỏ. Thanh địa chỉ được giữ trong ảnh website theo yêu cầu của đề. Chú thích từng ảnh nêu môi trường tương ứng; các ảnh này không thay thế 50 mã [Hình x.y] của đề.")
supplement_rows = []
for filename, description in SUPPLEMENTAL:
    if (IMAGES / filename).is_file():
        state = filename
    else:
        state = "chưa có minh chứng"
    supplement_rows.append((description, state))
table(doc, ["Ảnh bổ sung", "Tệp"], supplement_rows, [4.8, 1.4], 8.3, status_col=1)
# Keep the assignment image gap separate from supplementary evidence counts.
heading(doc, "Checklist số lượng ảnh minh chứng", 2)
paragraph(doc, "Số lượng đã chèn là số mã hình có ảnh minh họa; mã nào dùng ảnh lịch sử hoặc tái hiện cục bộ được ghi rõ ở chú thích. " + ("Các mã chỉ có ảnh một phần: " + ", ".join(partial_figures) + ". Xem giới hạn tại chú thích." if partial_figures else "Đủ ảnh cho 50/50 mã hình; môi trường và khác biệt giao diện được ghi ở chú thích từng ảnh."))
table(doc, ["Nhóm ảnh", "Đã chèn", "Chưa có"], [
    ("Mã hình theo PDF", f"{len(figure_evidence)} / {len(FIGURES)}", str(len(FIGURES) - len(figure_evidence))),
    ("Ảnh bổ sung mục 14", f"{len(available_supplemental)} / {len(SUPPLEMENTAL)}", str(len(SUPPLEMENTAL) - len(available_supplemental))),
], [3.7, 1.2, 1.3], 8.3)
# Exactly fifty figure evidence rows.
heading(doc, "Ma trận 50 hình theo đề")
paragraph(doc, "Mã và nội dung theo 50 vị trí [Hình x.y] trong PDF .NET 10. Ảnh được nhúng trực tiếp ở phần minh chứng; chú thích phân biệt ảnh live, ảnh local và ảnh lịch sử.")
grouped = {}
for number, desc in FIGURES:
    grouped.setdefault(number.split(".")[0], []).append((number, desc))
for chapter, chapter_title in CHAPTERS.items():
    if chapter == "9":
        doc.add_page_break()
    heading(doc, f"Hình {chapter}  {chapter_title}", 2)
    rows = []
    for number, desc in grouped[chapter]:
        status = ("Ảnh một phần" if manifest_details.get(number, {}).get("coverage") == "partial" else "Đã chèn") if number in figure_evidence else "Chưa có ảnh"
        rows.append((f"[Hình {number}]", desc, status))
    table(doc, ["Hình", "Nội dung cần chụp theo PDF", "Ảnh"], rows, [.78, 4.55, .92], 7.9, 2)

# Hosting and submission status.
heading(doc, "Triển khai lên Somee")
paragraph(doc, "Bản publish mới được đưa lên Somee ngày 28/11/2026 tại " + LIVE_URL + "; File Manager xác nhận đã giải nén ba tệp gồm DLL, CSS và JavaScript, tổng 523,77 KB; `appsettings.Production.json` được giữ nguyên. Website đã khởi động và các trang công khai chính hiển thị khi mở trực tiếp. CSS và JavaScript live đã được tải lại và đối chiếu khớp source. Hình 10.7 giữ URL HTTP thật trên thanh địa chỉ; quảng cáo hosting của Somee che một phần nội dung ở mép dưới ảnh. Cùng ngày, database được sao lưu thành `before-film-catalog-20261128.bak`; sau khi xác nhận năm phim và thể loại đã đúng giá trị đích, giao dịch có điều kiện chỉ ghi migration `20261128010000_UseOfficialFilmCatalog` vào lịch sử EF Core. Truy vấn sau chạy xác nhận ProductVersion 10.0.15. Không có bản ghi phim/thể loại bị cập nhật hoặc xóa.")
paragraph(doc, "Somee hiện quảng bá ASP.NET Core 3–10 ở phần đặc điểm gói, nhưng FAQ trên cùng trang ghi runtime đến .NET 9. Website .NET 10 hiện chạy thực tế trên gói này, dù nội dung FAQ chưa đồng nhất. Gói free nêu quảng cáo, 150 MB lưu trữ web, 5 GB truyền dữ liệu/tháng, một SQL Server database 30 MB data/30 MB log; database có thể bị xóa nếu không có query 30 ngày. Xem nguồn Somee và FAQ ở phần Nguồn đối chiếu.")
paragraph(doc, "Publish cần dùng thư mục đầu ra mới. Khi nén, lấy nội dung bên trong publish để web.config và wwwroot nằm ở gốc archive; lỗi ZIP chứa thêm một tầng thư mục được nhắc trong transcript video tại 1:01:25. Transcript tại 53:33 cũng nhắc thay footer thôi chưa đủ cá nhân hóa; cần thể hiện thương hiệu, poster, nội dung và bố cục riêng.")
paragraph(doc, "SMTP có hai luồng riêng. Email xác nhận cho đơn thử nghiệm #3 và #4 đều có trong Inbox và Sent của Gmail; #4 trị giá 159.000 VNĐ, được lưu sau triển khai ngày 28/11/2026. Trang xác nhận tài khoản cho biết email đã được xác nhận; đã đăng nhập thành công và menu hiển thị Tài khoản/Đăng xuất. Checkout lưu đơn trong transaction trước khi thử gửi mail.")

heading(doc, "Kết luận và việc còn lại")
paragraph(doc, f"Source ASP.NET Core MVC .NET 10 đã build Release, deploy lại trên Somee và được kiểm tra trực tiếp ở các trang chính tại {LIVE_URL}. Giao diện dùng font Noto Sans, poster chính thức có nguồn tham khảo, đăng ký và đăng nhập hiển thị đúng tiếng Việt. Email xác nhận đơn #4 trị giá 159.000 VNĐ có trong Inbox và Sent; trang xác nhận cho biết email tài khoản đã xác nhận, đăng nhập thành công đã được xác minh. Đã nhúng ảnh cho {len(figure_evidence)} / {len(FIGURES)} mã hình và {len(available_supplemental)} ảnh bổ sung. Migration UseOfficialFilmCatalog đã được ghi nhận trên production sau khi sao lưu và kiểm tra dữ liệu đích; transaction chỉ thêm lịch sử EF Core vì dữ liệu catalog đã đúng. Hình 6.1 có kết quả scaffold trong PowerShell đúng khung Terminal; ảnh lịch sử Somee được ghi rõ nguồn và thời điểm. Sinh viên vẫn cần nộp URL website và repository trên LMS.")

heading(doc, "Nguồn đối chiếu")
paragraph(doc, "Đề mới BaiTap_CaNhan_ASP_CORE_LMS.pdf: ASP.NET Core MVC .NET 10, mục 12–16 và 50 hình minh chứng.")
paragraph(doc, "Hướng dẫn cũ HuongDanLamWebsite_Backend_Core.pdf / huong-dan-cu.txt: .NET 5, chỉ đối chiếu quy trình. Transcript video cũ: MVC5/.NET Framework trong Visual Studio.")
p = paragraph(doc, "Hosting: ")
hyperlink(p, "Somee Free ASP.NET Hosting và FAQ", SOMEE_URL)
p = paragraph(doc, "Repository hiện có: ")
hyperlink(p, REPO_URL, REPO_URL)
p = paragraph(doc, "Website Somee (HTTP): ")
hyperlink(p, LIVE_URL, LIVE_URL)

# Embed only reviewed numbered screenshots and available supplemental evidence.
add_embedded_images(doc, available + available_supplemental)

doc.core_properties.title = "Bài tập cá nhân Back-End ASP.NET Core MVC"
doc.core_properties.subject = "Báo cáo Lab Back-End ASP.NET Core MVC .NET 10"
doc.core_properties.author = "Nguyễn Hoàng Hưng"
doc.core_properties.keywords = "ASP.NET Core MVC, .NET 10, EF Core, SQL Server, Lab Back-End"
update_fields = doc.settings._element.find(qn("w:updateFields"))
if update_fields is None:
    update_fields = OxmlElement("w:updateFields")
    doc.settings._element.append(update_fields)
update_fields.set(qn("w:val"), "true")
doc.save(OUT)
print(str(OUT).encode("ascii", "backslashreplace").decode("ascii"))
print("Embedded numbered figures:", ", ".join(figure_evidence) or "none")
