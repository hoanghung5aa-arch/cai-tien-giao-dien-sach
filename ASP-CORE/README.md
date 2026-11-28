# Lab Back-End Góc Phim Hưng

Bài tập cá nhân xây dựng website cửa hàng phim bằng ASP.NET Core MVC .NET 10. Người dùng có thể duyệt phim, lọc/tìm kiếm, quản lý thể loại và phim, tải poster, thêm sản phẩm vào giỏ hàng và hoàn tất đơn đặt hàng. Dự án lưu dữ liệu trong SQL Server, dùng ASP.NET Core Identity cho tài khoản và dùng session cho giỏ hàng.

**Sinh viên:** Nguyễn Hoàng Hưng  
**MSSV:** 31241027221  
**Lớp:** K50-EE0002  
**Học phần:** 26C1INF50902702 – Phát triển ứng dụng thương mại điện tử  
**Giảng viên:** TS. Nguyễn Mạnh Tuấn

**Repository hiện có:** [hoanghung5aa-arch/cai-tien-giao-dien-sach](https://github.com/hoanghung5aa-arch/cai-tien-giao-dien-sach)  
**Website Somee:** [Góc Phim Hưng](http://gocphimhung31241027221.somee.com). Bản ngày 28/11/2026 đã triển khai lại DLL, CSS và JavaScript; File Manager xác nhận ba tệp (523,77 KB). Trang chủ HTTP 200; CSS/JavaScript live khớp source. Danh mục hiện có mười phim dùng poster quảng bá chính thức; nguồn ảnh ghi trong [poster-sources.md](Report/poster-sources.md).

**Kiểm tra cuối:** 20 kiểm tra HTTP đạt trên bản local dùng SQL Server Somee, gồm chuẩn hóa chuỗi trước validation, biên giá/điểm/ngày/thể loại, chữ ký upload, giữ poster khi sửa, antiforgery và giỏ hàng. Dòng phim QA đã được xóa sau kiểm tra; dữ liệu chính còn 10 phim, 6 thể loại. Email đơn #4 (159.000 VNĐ) đã nhận trong Gmail Inbox/Sent sau deploy; tài khoản đã xác nhận email và đăng nhập thành công. Xem [kết quả QA](../docs/lab-backend/QA_FINAL_20261128.json).

## Công nghệ

- .NET 10 / ASP.NET Core MVC; ứng dụng nhắm `net10.0`.
- Entity Framework Core 10 và SQL Server.
- ASP.NET Core Identity với xác nhận email.
- Session và `System.Text.Json` để lưu giỏ hàng.
- HTML, CSS, JavaScript và Bootstrap cho giao diện responsive.
- Gmail SMTP cho email xác nhận tài khoản và email thông báo đơn hàng khi cấu hình.

## Chức năng và quy tắc dữ liệu

- Duyệt danh mục phim, tìm theo tiêu đề, lọc theo thể loại, xem chi tiết, tạo/sửa/xóa phim và thể loại.
- Mỗi phim tham chiếu đúng một `Genre` bằng `GenreId`; thể loại không thể xóa khi còn phim tham chiếu.
- Kiểm tra dữ liệu phía máy chủ: tiêu đề 3–200 ký tự; giới thiệu 3–1.200 ký tự; giá từ 5.000 VNĐ; xếp hạng `double` từ 1 đến 5; ngày phát hành phải ở tương lai; tên thể loại tối đa 200 ký tự và không trùng.
- Tải poster JPG, JPEG, PNG, GIF hoặc WEBP tối đa 5 MB. Máy chủ kiểm tra chữ ký tệp, đặt tên ngẫu nhiên và lưu ảnh dưới `wwwroot/images/uploads`. Danh mục live đã chuyển sang poster thật trong `wwwroot/images/posters`; ảnh SVG minh họa cũ không còn dùng trong bộ ảnh báo cáo mới.
- Cột `PicturePath` trong SQL Server chỉ lưu đường dẫn tương đối của ảnh. Tệp ảnh vẫn nằm trên hệ thống tệp trong `wwwroot`; database không tự sao chép hoặc đồng bộ tệp ảnh. Khi triển khai, cần đưa ảnh tĩnh vào gói publish và giữ thư mục ảnh người dùng tải lên bền vững.
- Giỏ hàng lưu trong session; có thể thêm sản phẩm, cập nhật số lượng (1–50), xóa dòng và xem tổng tiền.
- Checkout kiểm tra thông tin nhận hàng, tải lại giá và thông tin phim từ SQL Server, rồi lưu hóa đơn, chi tiết hóa đơn và địa chỉ giao hàng trong cùng một transaction. `ShippingDetail` có quan hệ một-một với `HoaDon`; một hóa đơn có nhiều `ChiTietHoaDon`.
- Các POST thay đổi dữ liệu trong luồng giỏ hàng, checkout, phim và thể loại dùng antiforgery token.
- Tài khoản Identity cần xác nhận email. Form đăng ký tự gửi liên kết xác nhận; khi SMTP lỗi, trang thông báo rằng tài khoản đã tạo nhưng thư chưa gửi và cho phép gửi lại. Liên kết trực tiếp chỉ hiển thị ở môi trường Development. Cần cấu hình SMTP hợp lệ để gửi email ở Production. Email xác nhận đơn hàng gửi sau khi lưu đơn; lỗi SMTP được ghi log và không rollback đơn hàng.

Migration khởi tạo năm phim và ba thể loại mẫu. Trên site Somee, đã nhập thêm năm phim và ba thể loại qua các form quản trị; kiểm tra danh mục sau khi lưu thấy đủ năm tên phim mới và sáu thể loại tổng cộng.

**Tình trạng kiểm tra:** Release build/publish thành công với SDK 10.0.404, 0 lỗi và 0 cảnh báo. Bản local chạy tại `http://127.0.0.1:5080`; không thay đổi Smart App Control. Kiểm tra SQL chỉ đọc ghi nhận 10 Movies, 6 Genres, 4 HoaDons, 4 ChiTietHoaDons, 4 ShippingDetails và 1 AspNetUser đã xác nhận. Hai đơn #3 và #4 đều 159.000 VNĐ; email thực nhận được xác minh trong Gmail.

## Cấu trúc

```text
ASP-CORE/
├── README.md
├── global.json
├── dotnet-tools.json
├── MvcMovie_Core.sln
├── SourceCode/
│   └── 31241027221_NguyenHoangHung/
│       ├── Areas/Identity/
│       ├── Controllers/       # Home, Movies, Genres, Cart, Checkout
│       ├── Data/              # ApplicationDbContext
│       ├── Database/          # SQL scripts; xem lưu ý bên dưới
│       ├── Extension/         # SessionExtensions
│       ├── Migrations/
│       ├── Models/
│       ├── Services/          # SMTP và lưu poster
│       ├── Validation/
│       ├── Views/
│       ├── wwwroot/           # CSS, JavaScript, poster và ảnh tải lên
│       ├── appsettings.json   # Chỉ chứa placeholder/cấu hình không bí mật
│       └── appsettings.example.json
└── Report/
    ├── BaiTap_CaNhan_ASP_CORE_LMS.docx
    ├── build_report.py
    └── images/                # Minh chứng thật Hinh_1_1.png … Hinh_10_7.png
```

## Cấu hình cơ sở dữ liệu và email

`appsettings.Development.json` không được chép vào gói publish và đã được loại trừ khỏi Git. Cấu hình mẫu không chứa thông tin bí mật.

Dùng SQL Server Somee cho môi trường hosting. Để bản local và hosting cùng truy cập dữ liệu theo yêu cầu bài lab, đặt cùng `ConnectionStrings:DefaultConnection` vào User Secrets ở Development; chỉ dùng SQL Server local riêng nếu chủ động chọn tách dữ liệu. Cấu hình bằng User Secrets khi phát triển, hoặc bằng biến môi trường/cơ chế secrets của hosting. Chuỗi kết nối SQL Server trên Somee trong tài liệu đề có `TrustServerCertificate=True`; xác nhận thông tin máy chủ và database từ tài khoản của chính mình. Không đưa connection string, mật khẩu SQL hoặc Gmail App Password vào GitHub, README hay báo cáo.

Chạy PowerShell tại thư mục `ASP-CORE` để cấu hình secrets local:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_SQL_SERVER_CONNECTION_STRING" --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
dotnet user-secrets set "Email:UserName" "YOUR_GMAIL_ADDRESS" --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
dotnet user-secrets set "Email:AppPassword" "YOUR_GMAIL_APP_PASSWORD" --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
```

Có thể đặt các khóa email khác nếu cần: `Email:Host` (`smtp.gmail.com`), `Email:Port` (`587`) và `Email:FromName` (`Góc Phim Hưng`). Trên hosting, dùng `ConnectionStrings__DefaultConnection`, `Email__UserName`, `Email__AppPassword`, `Email__Host`, `Email__Port` và `Email__FromName` trong phần quản lý biến môi trường bí mật của host. Hai luồng email bỏ khoảng trắng trong App Password trước khi xác thực SMTP. Gmail App Password cần được tạo theo quy trình bảo mật hiện hành của Google; không dùng mật khẩu đăng nhập Gmail thông thường.

> **Cài database:** `Database/InstallDatabase.sql` gồm schema và dữ liệu khởi tạo, chỉ chạy một lần trên database SQL Server mới, rỗng. Với database đã có migration history, dùng `dotnet ef database update` để áp dụng migration còn thiếu; không chạy lại script cài mới.

### Migration bảo vệ dữ liệu

`UseOfficialFilmCatalog` chỉ cập nhật các dòng seed 1–5 nếu mỗi dòng khớp hoàn toàn với giá trị khởi tạo hoặc đã là giá trị danh mục đích. Nếu dòng seed đã được sửa khác dự kiến, migration dừng để tránh ghi đè. Migration giữ nguyên Genre ID 1, chỉ thêm `Hoạt hình` ở ID 7 khi ID này còn trống, không thay đổi phim người dùng tạo, đơn hàng hay tài khoản. `Down` không khôi phục dữ liệu seed và không xóa Genre 7 để bảo vệ các chỉnh sửa phát sinh sau này.

Lần cập nhật báo cáo này đã build source và kiểm tra không còn thay đổi model chưa được migration ghi nhận. Migration mới chưa được áp dụng vào production; trước khi chạy trên hosting cần sao lưu database và xác nhận trạng thái `__EFMigrationsHistory`.

## Khôi phục và chạy local

Cài .NET 10 SDK, SQL Server và (nếu cần) công cụ `sqlcmd` hoặc SQL Server Management Studio. Từ thư mục `ASP-CORE`, khôi phục local tools và package, rồi áp dụng migration lên database đã tạo:

```powershell
dotnet --version
dotnet tool restore
dotnet restore SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
dotnet ef database update --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
dotnet run --launch-profile https --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
```

Mở địa chỉ HTTPS in ở dòng `Now listening on` trong terminal. Cổng chạy local có thể khác nhau theo máy. Dự án ghim SDK tối thiểu 10.0.100 trong `global.json` và cho phép cập nhật lên feature band mới hơn của .NET 10; tool manifest ghim `dotnet-ef` 10.0.15.

**Windows PATH:** ở môi trường hiện tại, SDK cài tại `%USERPROFILE%\.dotnet` nhưng lệnh `dotnet` không nằm sẵn trong `PATH`. Nếu PowerShell báo không tìm thấy SDK, thêm thư mục SDK vào `PATH` của cửa sổ hiện tại rồi kiểm tra lại:

```powershell
$dotnetRoot = Join-Path $env:USERPROFILE ".dotnet"
$env:DOTNET_ROOT = $dotnetRoot
$env:PATH = "$dotnetRoot;$dotnetRoot\tools;$env:PATH"
dotnet --version
```

Một lần kiểm tra môi trường phát triển ghi nhận SDK 10.0.404 và runtime `Microsoft.NETCore.App`/`Microsoft.AspNetCore.App` 10.0.15. Build Release của source hiện tại đã được xác nhận thành công bằng SDK này.

## Publish và triển khai

Tạo gói publish framework-dependent sau khi cấu hình connection string/SMTP cho môi trường đích và xác minh host có ASP.NET Core Runtime 10:

```powershell
dotnet publish SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj -c Release -o publish-for-host
```

Mỗi lần sửa mã nguồn, publish ra thư mục đầu ra mới. Khi nén để dùng File Manager, nén **các tệp và thư mục bên trong** `publish-for-host`, để `web.config`, `.dll` và `wwwroot` nằm ở gốc archive; không nén thêm một thư mục bọc `publish-for-host`. Kiểm tra kỹ `appsettings.Development.json` không có trong gói publish. Không đưa thư mục publish, ZIP triển khai hoặc file tạm vào repository.

Somee Free ASP.NET Hosting quảng bá hỗ trợ ASP.NET Core 3–10, nhưng FAQ trên cùng trang còn ghi runtime đến .NET 9. Ứng dụng hiện đã deploy và chạy trên gói Somee này, dù thông tin FAQ chưa đồng nhất. Trang này cũng nêu gói miễn phí có quảng cáo, 150 MB lưu trữ web, 5 GB truyền dữ liệu mỗi tháng, một SQL Server database 30 MB data/30 MB log. Database miễn phí có thể bị xóa nếu 30 ngày không có truy vấn; website và database cần được giữ hoạt động theo quy định hiện hành của Somee. Xem [thông tin gói và FAQ chính thức](https://somee.com/FreeAspNetHosting.aspx).

Website đã chạy tại URL nêu trên. Đơn #1 và #2 là các thử nghiệm cũ; #2 từng có `EmailSent=false`. Sau khi cấu hình SMTP đúng tài khoản, đơn #3 và #4 được gửi thành công; email #4 đã nhận sau lần triển khai ngày 28/11/2026. Các địa chỉ giao hàng là dữ liệu LAB. Mật khẩu SQL/Gmail App Password không được đưa vào repository hoặc báo cáo.

## Báo cáo và minh chứng

Báo cáo theo mục 14–15 nằm tại [BaiTap_CaNhan_ASP_CORE_LMS.docx](Report/BaiTap_CaNhan_ASP_CORE_LMS.docx). Ảnh được **nhúng trực tiếp** vào Word, chú thích bên dưới; giữ khung bìa và logo UEH của mẫu. Ảnh website giữ thanh địa chỉ nơi đề yêu cầu URL; tab Chrome, taskbar và con trỏ không xuất hiện. Bộ ảnh dùng poster thật.

Ma trận giữ đủ 50 mã theo PDF .NET 10; hiện **50/50 mã có ảnh nhúng và 12 ảnh bổ sung**. Đã bổ sung lỗi Movies trước migration, Index rỗng, hai migration, tệp no-image.jpg và cột PicturePath. Hình 3.3 gồm hai phần cắt của cùng ảnh lịch sử để giữ đủ trường form và loại con trỏ. **Hình 6.1 đã thay bằng ảnh PowerShell thật, hiển thị scaffold Genre thành công cùng controller và đủ năm View.** Không còn mã đánh dấu ảnh một phần trong manifest. Chú thích phân biệt ảnh live, tái hiện local và ảnh lịch sử. [Ma trận minh chứng](../docs/lab-backend/MA_TRAN_MINH_CHUNG_20261128.csv).

Bản hướng dẫn cũ dùng .NET 5 và transcript video dùng ASP.NET MVC 5/.NET Framework trong Visual Studio. Chỉ lấy quy trình chung khi phù hợp; thao tác cũ như `Startup.cs`, cổng cố định, publish profile hay cách lưu ảnh của framework cũ không thay thế hướng dẫn hiện hành .NET 10 và mã nguồn này.
