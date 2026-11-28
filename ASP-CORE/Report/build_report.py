from datetime import date
from pathlib import Path

from docx import Document
from docx.enum.section import WD_ORIENT, WD_SECTION
from docx.enum.table import WD_CELL_VERTICAL_ALIGNMENT, WD_TABLE_ALIGNMENT
from docx.enum.text import WD_ALIGN_PARAGRAPH
from docx.oxml import OxmlElement
from docx.oxml.ns import qn
from docx.shared import Inches, Pt, RGBColor

REPORT = Path(__file__).resolve().parent
IMAGES = REPORT / "images"
OUT = REPORT / "BaiTap_CaNhan_ASP_CORE_LMS.docx"
REPORT_DATE = date(2026, 11, 28)
REPO_URL = "https://github.com/hoanghung5aa-arch/cai-tien-giao-dien-sach"
SOMEE_URL = "https://somee.com/FreeAspNetHosting.aspx"

INK, BODY, GREEN, NAVY = "151515", "26302C", "236B57", "17372F"
PALE, MINT, GRAY, RED, WHITE, BORDER = "F3F6F3", "EAF3ED", "63716B", "A84438", "FFFFFF", "D9D9D9"

# All 50 labels and captions correspond to the [Hình x.y] prompts in the current .NET 10 PDF.
FIGURES = [
    ("1.1", "Terminal chạy dotnet --version và dotnet --list-sdks, thấy dòng 10.0.xxx."),
    ("2.1", "Terminal sau dotnet new mvc, thấy template MVC tạo thành công và restore hoàn tất."),
    ("2.2", "Hộp thoại Windows HTTPS Security Warning với nút Yes."),
    ("2.3", "Terminal có Now listening on và trình duyệt hiện trang Welcome, thấy thanh địa chỉ."),
    ("2.4", "Program.cs, khoanh dòng options.UseSqlServer(connectionString)."),
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
    ("7.2", "Create.cshtml có enctype multipart/form-data và khối PictureUpload."),
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
    ("10.5", "File Manager đã chọn publish.zip và nút Upload and Unzip archives."),
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
    ("Hinh_Audit_DanhMuc_20261128.png", "Danh mục website live với poster phim thật; ảnh trang không có con trỏ, thanh Chrome hay taskbar."),
    ("Hinh_Audit_PosterThat_20261128.png", "Chi tiết phim Avengers: Doomsday với poster thật đang hiển thị trên bản deploy."),
    ("Hinh_Audit_TheLoai_20261128.png", "Danh sách thể loại trên website live sau bản deploy mới."),
]

# Legacy numbered screenshots contained a visible cursor or no longer matched the deployed site.
# Keep them out of the report until clean, matching evidence is recaptured.
CLEAN_FIGURE_NUMBERS = {"7.6"}



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


def add_field_images(doc, image_entries):
    if not image_entries:
        return
    sec = doc.add_section(WD_SECTION.NEW_PAGE)
    sec.orientation = WD_ORIENT.LANDSCAPE
    sec.page_width, sec.page_height = Inches(11.69), Inches(8.27)
    sec.top_margin = sec.bottom_margin = Inches(.42)
    sec.left_margin = sec.right_margin = Inches(.58)
    sec.header_distance = sec.footer_distance = Inches(.18)
    heading(doc, "Ảnh minh chứng đã nhận", 1)
    paragraph(doc, "Chỉ ảnh đã kiểm tra sạch con trỏ mới xuất hiện ở đây. Ma trận giữ đủ 50 vị trí; ảnh chưa có bản chụp sạch được đánh dấu “Cần ảnh sạch”.", 9, color=GRAY, after=7)
    for index, (label, desc, path) in enumerate(image_entries):
        if index:
            doc.add_page_break()
        paragraph(doc, f"[{label}]. {desc}", 10, True, INK, WD_ALIGN_PARAGRAPH.CENTER, after=2, keep=True)
        p = paragraph(doc, "Tệp ảnh: ", 8.5, color=GRAY, align=WD_ALIGN_PARAGRAPH.CENTER, after=5, keep=True)
        hyperlink(p, f"images/{path.name}", f"images/{path.name}")
        p = doc.add_paragraph()
        p.alignment = WD_ALIGN_PARAGRAPH.CENTER
        p.paragraph_format.space_after = Pt(0)
        shape = p.add_run().add_picture(str(path), width=Inches(9.9))
        shape._inline.docPr.set("descr", f"[{label}]. {desc}")


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

available = []
for number, desc in FIGURES:
    path = IMAGES / f"Hinh_{number.replace('.', '_')}.png"
    if number in CLEAN_FIGURE_NUMBERS and path.is_file():
        available.append((f"Hình {number}", desc, path))
available_supplemental = [(f"Mục 14", desc, IMAGES / name) for name, desc in SUPPLEMENTAL if (IMAGES / name).is_file()]
missing = len(FIGURES) - len(available)

# Cover
for _ in range(2):
    paragraph(doc, "", after=8)
paragraph(doc, "ĐẠI HỌC KINH TẾ THÀNH PHỐ HỒ CHÍ MINH", 11.5, True, INK, WD_ALIGN_PARAGRAPH.CENTER, after=4)
paragraph(doc, "TRƯỜNG CÔNG NGHỆ VÀ THIẾT KẾ", 10.5, True, INK, WD_ALIGN_PARAGRAPH.CENTER, after=4)
paragraph(doc, "KHOA CÔNG NGHỆ THÔNG TIN KINH DOANH", 9.5, color=GRAY, align=WD_ALIGN_PARAGRAPH.CENTER, after=36)
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
paragraph(doc, f"Ảnh minh chứng sạch con trỏ theo đề: {len(available)} / 50  |  Còn thiếu: {missing}", 10, True, GREEN if not missing else RED, WD_ALIGN_PARAGRAPH.CENTER, after=5, before=13)
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
paragraph(doc, "Bài tập xây dựng cửa hàng phim Góc Phim Hưng bằng ASP.NET Core MVC .NET 10, Entity Framework Core và SQL Server. Source hiện có Identity, quản lý phim/thể loại, tải poster, giỏ hàng trong session, checkout, lưu đơn hàng và dịch vụ email. Root xác nhận build Release thành công với SDK 10.0.404; Microsoft.NETCore.App và Microsoft.AspNetCore.App runtime là 10.0.15.")
paragraph(doc, "Bản mới đã được triển khai lên Somee ngày 28/11/2026. Kiểm tra trực tiếp trên website xác nhận trang danh mục, thể loại, chi tiết phim, đăng nhập và đăng ký đều hiển thị; footer còn đúng MSSV. Giao diện dùng font Noto Sans nhúng cho tiếng Việt và poster quảng bá chính thức được liệt kê nguồn trong báo cáo. Thư xác nhận tài khoản đã tới Gmail và liên kết xác nhận được mở ngày 28/11/2026. Source có xử lý SMTP lỗi sau khi lưu đơn; email thông báo đơn hàng chưa được xác nhận gửi/nhận đầu cuối. Build Release của source hiện tại đã thành công với SDK 10.0.404.")
table(doc, ["Hạng mục", "Tình trạng"], [
    ("Project .NET 10", "Target net10.0; build Release đã được root xác nhận thành công."),
    ("Phim và thể loại", "Source seed năm phim và ba thể loại. Giao diện công khai hiện liệt kê mười phim; ảnh thể loại hiện có sáu dòng. Không có ảnh SQL mới để xác nhận dữ liệu nào do sinh viên nhập."),
    ("Upload poster", "Source kiểm tra loại tệp, dung lượng và chữ ký; ảnh là file trên đĩa. Ảnh thao tác upload chưa có."),
    ("Cart và checkout", "Source có session, tải lại giá từ database và transaction; chưa có ảnh kết quả thao tác thật."),
    ("SQL Server Somee", "Source có schema và migration EF Core. Ảnh xác nhận các bảng/dữ liệu live còn thiếu trong bộ minh chứng hiện có."),
    ("Website public", "Bản mới đã deploy ngày 28/11/2026; danh mục, thể loại, chi tiết phim, đăng nhập và đăng ký đã hiển thị khi kiểm tra trực tiếp."),
    ("50 ảnh theo đề", f"Đã có {len(available)} ảnh sạch con trỏ đúng mã trong 50 vị trí; còn {missing} vị trí cần chụp lại. Ảnh cũ có con trỏ hoặc không còn khớp bản deploy được loại khỏi Word. Có ba ảnh giao diện live mới ở mục 14."),
], [1.55, 4.65], size=8.6, status_col=1)

# Scope and section 14/15.
heading(doc, "Phạm vi và tài liệu đối chiếu")
paragraph(doc, "PDF bài tập ASP.NET Core MVC .NET 10 dài 41 trang là căn cứ chính cho yêu cầu source, các bước, 50 vị trí [Hình x.y], mục 14–15 và rubric 100 điểm. Hướng dẫn cũ dùng .NET 5; transcript video thao tác ASP.NET MVC 5/.NET Framework trong Visual Studio. Hai tài liệu cũ chỉ giúp đối chiếu quy trình, không thay cho phiên bản package, Program.cs, route, session, lưu ảnh và publish trong source .NET 10 này.")
paragraph(doc, "Ma trận dưới đây giữ đủ 50 vị trí theo PDF mới. Chỉ ảnh đã kiểm tra sạch con trỏ và còn khớp nội dung hiện tại mới được liên kết, rồi chèn ở phụ lục. Ảnh cũ dính con trỏ hoặc không còn khớp bản deploy đã được loại khỏi Word; vị trí còn lại ghi “Cần ảnh sạch”.")
heading(doc, "Đối chiếu yêu cầu báo cáo và nộp bài")
table(doc, ["Yêu cầu mục 14–15", "Nội dung và trạng thái"], [
    ("Trang đầu", "Có họ tên, MSSV, lớp, học phần, tên bài, ngày báo cáo và URL repository."),
    ("Môi trường", "Windows 11 Home Single Language build 26300; SDK 10.0.404; runtime 10.0.15. Phiên bản VS Code chưa ghi nhận."),
    ("Thiết kế dữ liệu", "Mô tả các model và quan hệ; ảnh bảng database còn thiếu."),
    ("Chức năng", "Cloud QA bản trước xác nhận CRUD, upload/giữ poster, giá 5.000/4.999, cart, antiforgery và checkout. Source có thêm chuẩn hóa Title/Summary trước khi kiểm tra độ dài; đã build nhưng chưa runtime retest riêng."),
    ("Triển khai", "Bản mới đã deploy ngày 28/11/2026; trang danh mục, thể loại, phim, đăng nhập và đăng ký hiển thị."),
    ("Khó khăn và kết luận", "Nêu khác biệt tài liệu .NET 5/MVC5, giới hạn runtime hosting, SMTP best-effort và các ảnh theo đề còn thiếu."),
    ("Ảnh mục 14", "Bảng ảnh bổ sung riêng cho cấu trúc VS Code, local, CRUD, upload, database, cart, checkout, hosting và GitHub."),
    ("GitHub, README, LMS", "Source, poster/font và ảnh thể loại đã được đẩy lên repository; Word này sẽ được bổ sung riêng sau QA. URL LMS sinh viên tự nộp."),
], [1.55, 4.65], size=8.3)

# Environment.
heading(doc, "Môi trường thực hiện")
table(doc, ["Thành phần", "Thông tin ghi nhận"], [
    ("Hệ điều hành", "Windows 11 Home Single Language, build 26300."),
    ("IDE", "Visual Studio Code theo quy trình đề; chưa ghi số phiên bản."),
    ("SDK", ".NET SDK 10.0.404; global.json yêu cầu 10.0.100 và rollForward latestFeature."),
    ("Runtime", "Microsoft.NETCore.App 10.0.15 và Microsoft.AspNetCore.App 10.0.15."),
    ("ORM/database", "Entity Framework Core 10.0.15 và SQL Server."),
    ("Somee", "SQL Server 2025 Express database PTUDW31241027221; database được root xác nhận có bốn migration, năm Movies và ba Genres."),
    ("Tooling", "dotnet-ef 10.0.15; dotnet-aspnet-codegenerator 10.0.3."),
    ("PATH", "SDK nằm trong %USERPROFILE%\\.dotnet; terminal mặc định không tìm thấy dotnet. Dùng đường dẫn đầy đủ hoặc thêm SDK folder vào PATH của phiên PowerShell."),
], [1.5, 4.7], size=8.6)
paragraph(doc, "Build Release được root xác nhận bằng C:\\Users\\Admin\\.dotnet\\dotnet.exe. Ứng dụng đã publish và được kiểm tra trên Somee. Trên máy local, Windows Smart App Control chặn binary bằng policy VerifiedAndReputableDesktop (Code Integrity event 3077); không tắt chính sách này để chạy bản mới.", 9.1, color=GRAY)

# Project design and personal work.
heading(doc, "Thiết kế và phần điều chỉnh cá nhân")
paragraph(doc, "Website mang tên Góc Phim Hưng, dùng nội dung tiếng Việt, thương hiệu và danh mục phim riêng. Thay MSSV/họ tên ở footer là một yêu cầu trong đề, nhưng phần cá nhân hóa còn thể hiện ở nội dung, poster, cách trình bày và luồng mua hàng.")
bullet(doc, "Quan hệ thể loại. ", "Mỗi Movie tham chiếu đúng một Genre qua GenreId và khóa ngoại. GenreExists kiểm tra id hợp lệ; unique index ngăn tên trùng. Đây là quan hệ một-nhiều chuẩn hóa, không lưu chuỗi thể loại tự do.")
bullet(doc, "Ràng buộc Movie. ", "Title dài 3–200; Summary 3–1.200; Price từ 5.000 VNĐ; Rated dùng double trong 1–5; ReleaseDate phải ở tương lai; tên thể loại tối đa 200 ký tự.")
bullet(doc, "Chống POST giả mạo. ", "Các POST thay đổi dữ liệu trong phim/thể loại, cart và checkout dùng ValidateAntiForgeryToken.")
bullet(doc, "Giá do server xác nhận. ", "Checkout tải lại tiêu đề, ảnh và giá từ database, không dùng giá session làm nguồn cuối. Số lượng đơn hàng được giới hạn 1–50.")
bullet(doc, "Transaction đơn hàng. ", "HoaDon, ChiTietHoaDon và ShippingDetail được lưu trong một transaction. Một hóa đơn có nhiều dòng chi tiết; ShippingDetail liên hệ một-một với HoaDon.")
bullet(doc, "Ảnh lưu riêng. ", "SQL chỉ lưu PicturePath; poster nằm dưới wwwroot. Database không tự đồng bộ file. Khi backup/chuyển host cần giữ cả các tệp ảnh.")
bullet(doc, "SMTP sau commit. ", "Identity xác nhận tài khoản qua email thật đã tới hộp thư và liên kết đã được mở. Email xác nhận đơn hàng vẫn cần kiểm tra gửi/nhận riêng. Checkout lưu đơn trước rồi mới thử SMTP; lỗi gửi được ghi log và không rollback đơn.")

heading(doc, "Thiết kế dữ liệu")
table(doc, ["Model/bảng", "Vai trò và quan hệ"], [
    ("IdentityUser / AspNetUsers", "Tài khoản ASP.NET Core Identity; yêu cầu xác nhận email."),
    ("Genre", "Tên tối đa 200, unique; một Genre có nhiều Movie."),
    ("Movie", "Thông tin phim, giá và PicturePath; mỗi phim có một GenreId."),
    ("HoaDon", "Thời gian, người mua và tổng tiền; một hóa đơn có nhiều chi tiết."),
    ("ChiTietHoaDon", "MovieId, số lượng và đơn giá tại thời điểm đặt; tham chiếu hóa đơn và phim."),
    ("ShippingDetail", "Người nhận và ngày giao; HoaDonId duy nhất tạo quan hệ một-một."),
], [1.6, 4.6], size=8.5)
paragraph(doc, "Migration có năm phim và ba thể loại hư cấu làm dữ liệu khởi đầu. Đề yêu cầu tối thiểu năm sản phẩm và ba thể loại do sinh viên tự nhập; dữ liệu seed không tự chứng minh yêu cầu đó. Giao diện hiện có mười phim và sáu thể loại hiển thị, nhưng bộ ảnh chưa chứng minh nguồn gốc từng dòng hay ảnh bảng database.", 9.2, True, RED)

heading(doc, "Quá trình thực hiện")
bullet(doc, "Chốt target. ", "Dùng đề .NET 10, global.json và tool manifest ghim dotnet-ef 10.0.15; không áp dụng nguyên lệnh .NET 5/MVC5.")
bullet(doc, "Mô hình hóa dữ liệu. ", "Tạo Identity context, Movie, Genre, HoaDon, ChiTietHoaDon, ShippingDetail, khóa ngoại và migration EF Core.")
bullet(doc, "Xây dựng tính năng. ", "Movies/Genres cung cấp CRUD và lọc/tìm; MovieImageStorage kiểm tra và lưu poster; CartController giữ cart trong session.")
bullet(doc, "Hoàn thiện đặt hàng. ", "Checkout kiểm tra input, tải lại giá sản phẩm, lưu hóa đơn/chi tiết/địa chỉ trong transaction rồi thử gửi email.")
bullet(doc, "Chuẩn bị triển khai. ", "Dùng secrets ngoài source; sau publish kiểm tra web.config/wwwroot ở gốc ZIP và thử URL thật trước khi ghi hoàn tất.")

# Results and current QA.
heading(doc, "Kiểm tra và tình trạng hiện tại")
table(doc, ["Kiểm tra", "Kết quả ghi nhận", "Việc còn lại"], [
    ("Build Release", "Root xác nhận pass với SDK 10.0.404.", "Ảnh terminal nếu dùng làm minh chứng."),
    ("Create/Edit phim", "Cloud QA bản trước đã pass CRUD; Price 5.000 được chấp nhận và 4.999 bị từ chối. Source mới chuẩn hóa Title/Summary trước kiểm tra; Release build pass.", "Chưa có runtime retest riêng cho chuỗi có khoảng trắng."),
    ("Database Somee", "Bản triển khai hiện chạy; ảnh 3.4 có thông tin kết nối nhưng không chứng minh bảng và dữ liệu.", "Ảnh bảng SQL live còn thiếu."),
    ("Dữ liệu phim/thể loại", "Source seed năm phim và ba thể loại; website hiển thị mười phim và sáu thể loại.", "Chưa xác minh năm phim/ba thể loại nào do sinh viên nhập."),
    ("Upload poster", "Cloud QA pass upload poster và giữ ảnh sau khi sửa thông tin phim.", "Ảnh thao tác cụ thể còn chờ chụp."),
    ("Cart và checkout", "Cloud QA pass Add/Update/Remove; thiếu ngày giao bị từ chối; đơn hợp lệ lưu và cart được xóa. Thiếu antiforgery token trả 400; ThankYou với order không hợp lệ trả 404.", "Ảnh luồng trình duyệt/bảng đơn hàng còn chờ chụp."),
    ("SMTP", "Thư xác nhận tài khoản đã được gửi tới Gmail và liên kết xác nhận đã mở. Trước đó email đơn thử báo EmailSent=false.", "Email thông báo đơn hàng chưa được xác nhận đầu cuối."),
    ("Somee .NET 10", "Bản mới đã deploy ngày 28/11/2026; danh mục, thể loại, phim, đăng nhập và đăng ký đã hiển thị.", "Ảnh SQL, thao tác CRUD/upload và checkout trên bản mới vẫn thiếu."),
    ("GitHub", "Source, poster/font, nguồn poster và ảnh thể loại đã được đẩy lên nhánh main.", "Word được cập nhật riêng sau khi hoàn tất QA; LMS cần sinh viên nộp URL."),
], [1.18, 2.65, 2.37], size=7.8)

# Extra evidence required by section 14.
heading(doc, "Ảnh bổ sung theo mục 14")
paragraph(doc, "Ba ảnh live dưới đây được chụp lại không có con trỏ, thanh Chrome hay taskbar. Ảnh cũ có con trỏ hoặc poster cũ đã được loại khỏi báo cáo; các ảnh này không thay thế 50 mã [Hình x.y] của đề.")
supplement_rows = []
for filename, description in SUPPLEMENTAL:
    if (IMAGES / filename).is_file():
        state = filename
    else:
        state = "chưa có minh chứng"
    supplement_rows.append((description, state))
supp_table = table(doc, ["Ảnh bổ sung", "Tệp"], supplement_rows, [4.8, 1.4], 8.3, status_col=1)
for row_index, (filename, _description) in enumerate(SUPPLEMENTAL, start=1):
    if (IMAGES / filename).is_file():
        p = supp_table.rows[row_index].cells[1].paragraphs[0]
        for child in list(p._p):
            if child.tag != qn("w:pPr"):
                p._p.remove(child)
        hyperlink(p, filename, f"images/{filename}")

# Exactly fifty figure evidence rows.
heading(doc, "Ma trận 50 hình theo đề")
paragraph(doc, "Mã và nội dung theo 50 vị trí [Hình x.y] trong PDF .NET 10. Chỉ ảnh đã kiểm tra sạch con trỏ và khớp nội dung mới được liên kết; ảnh cũ có con trỏ hoặc không còn khớp bản deploy được loại khỏi Word.")
grouped = {}
for number, desc in FIGURES:
    grouped.setdefault(number.split(".")[0], []).append((number, desc))
for chapter, chapter_title in CHAPTERS.items():
    if chapter == "9":
        doc.add_page_break()
    heading(doc, f"Hình {chapter}  {chapter_title}", 2)
    rows = []
    for number, desc in grouped[chapter]:
        filename = f"Hinh_{number.replace('.', '_')}.png"
        is_clean = number in CLEAN_FIGURE_NUMBERS and (IMAGES / filename).is_file()
        rows.append((f"[Hình {number}]", desc, filename if is_clean else "Cần ảnh sạch"))
    fig_table = table(doc, ["Hình", "Nội dung cần chụp theo PDF", "Ảnh"], rows, [.78, 4.55, .92], 7.9, 2)
    for row_index, (number, _desc) in enumerate(grouped[chapter], start=1):
        filename = f"Hinh_{number.replace('.', '_')}.png"
        if number in CLEAN_FIGURE_NUMBERS and (IMAGES / filename).is_file():
            p = fig_table.rows[row_index].cells[2].paragraphs[0]
            for child in list(p._p):
                if child.tag != qn("w:pPr"):
                    p._p.remove(child)
            p.alignment = WD_ALIGN_PARAGRAPH.CENTER
            hyperlink(p, filename, f"images/{filename}")

# Hosting and submission status.
heading(doc, "Triển khai lên Somee")
paragraph(doc, "Bản publish mới được đưa lên Somee ngày 28/11/2026; File Manager ghi nhận DLL và static assets mới, còn `appsettings.Production.json` được giữ nguyên. Website đã khởi động và các trang công khai chính hiển thị khi mở trực tiếp. Ảnh thể loại mới được chụp vùng trang, không có thanh Chrome, taskbar hay con trỏ. Ảnh SQL live và ảnh thao tác CRUD, upload, checkout trên phiên bản mới chưa có.")
paragraph(doc, "Somee hiện quảng bá ASP.NET Core 3–10 ở phần đặc điểm gói, nhưng FAQ trên cùng trang ghi runtime đến .NET 9. Website .NET 10 hiện chạy thực tế trên gói này, dù nội dung FAQ chưa đồng nhất. Gói free nêu quảng cáo, 150 MB lưu trữ web, 5 GB truyền dữ liệu/tháng, một SQL Server database 30 MB data/30 MB log; database có thể bị xóa nếu không có query 30 ngày. Xem [trang Somee và FAQ](https://somee.com/FreeAspNetHosting.aspx).")
paragraph(doc, "Publish cần dùng thư mục đầu ra mới. Khi nén, lấy nội dung bên trong publish để web.config và wwwroot nằm ở gốc archive; lỗi ZIP chứa thêm một tầng thư mục được nhắc trong transcript video tại 1:01:25. Transcript tại 53:33 cũng nhắc thay footer thôi chưa đủ cá nhân hóa; cần thể hiện thương hiệu, poster, nội dung và bố cục riêng.")
paragraph(doc, "SMTP có hai luồng riêng. Thư xác nhận tài khoản đã được gửi tới Gmail và liên kết xác nhận đã mở trong lần kiểm tra ngày 28/11/2026. Thử nghiệm email đơn hàng trước đó hiển thị `EmailSent=false`; chưa có lần gửi/nhận thành công để xác nhận luồng này. Checkout lưu đơn trong transaction trước khi thử gửi mail, nên không suy ra thư đã gửi chỉ từ trang ThankYou.")

heading(doc, "Kết luận và việc còn lại")
paragraph(doc, "Source ASP.NET Core MVC .NET 10 đã build Release, deploy lại trên Somee và được kiểm tra trực tiếp ở các trang chính. Giao diện dùng font Noto Sans, poster chính thức có nguồn tham khảo, đăng ký và đăng nhập hiển thị đúng tiếng Việt. Email xác nhận tài khoản đã tới Gmail và liên kết đã được mở; email thông báo đơn hàng chưa có bằng chứng gửi/nhận thành công. Checkout lưu đơn trước khi gửi mail, và SMTP lỗi không làm mất đơn.")
paragraph(doc, f"Ma trận đề hiện có {len(available)} ảnh sạch con trỏ đúng mã trong 50 vị trí; còn {missing} vị trí cần chụp lại theo đúng nội dung đề. Ảnh giao diện live mới, gồm danh mục có poster thật, chi tiết phim và thể loại, được đưa vào mục 14. Ảnh cũ có con trỏ hoặc giao diện poster cũ đã được loại khỏi báo cáo. Cần bổ sung ảnh SQL live và các thao tác CRUD, upload, giỏ hàng, checkout trên bản deploy mới. Sinh viên vẫn cần nộp URL website và repository trên LMS.")

heading(doc, "Nguồn đối chiếu")
paragraph(doc, "Đề mới BaiTap_CaNhan_ASP_CORE_LMS.pdf: ASP.NET Core MVC .NET 10, mục 12–16 và 50 hình minh chứng.")
paragraph(doc, "Hướng dẫn cũ HuongDanLamWebsite_Backend_Core.pdf / huong-dan-cu.txt: .NET 5, chỉ đối chiếu quy trình. Transcript video cũ: MVC5/.NET Framework trong Visual Studio.")
p = paragraph(doc, "Hosting: ")
hyperlink(p, "Somee Free ASP.NET Hosting và FAQ", SOMEE_URL)
p = paragraph(doc, "Repository hiện có: ")
hyperlink(p, REPO_URL, REPO_URL)

# Embed only images that exist; each [Hình x.y] links to its real file above.
add_field_images(doc, available + available_supplemental)

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
print(f"Figures: {len(FIGURES)} total; {len(available)} linked actual images; {missing} missing")
print(f"Additional section 14 images: {len(available_supplemental)}")
