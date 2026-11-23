# Lab Back-End Góc Phim Hưng

Bài tập cá nhân xây dựng website cửa hàng phim bằng ASP.NET Core MVC .NET 10. Người dùng có thể duyệt phim, lọc/tìm kiếm, quản lý thể loại và phim, tải poster, thêm sản phẩm vào giỏ hàng và hoàn tất đơn đặt hàng. Dự án lưu dữ liệu trong SQL Server, dùng ASP.NET Core Identity cho tài khoản và dùng session cho giỏ hàng.

**Sinh viên:** Nguyễn Hoàng Hưng  
**MSSV:** 31241027221  
**Lớp:** K50-EE0002  
**Học phần:** 26C1INF50902702 – Phát triển ứng dụng thương mại điện tử  
**Giảng viên:** TS. Nguyễn Mạnh Tuấn

**Repository hiện có:** [hoanghung5aa-arch/cai-tien-giao-dien-sach](https://github.com/hoanghung5aa-arch/cai-tien-giao-dien-sach)  
**Website Somee:** [http://gocphimhung31241027221.somee.com](http://gocphimhung31241027221.somee.com) đã deploy và trả HTTP 200 ở trang danh mục phim.

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
- Tải poster JPG, JPEG, PNG, GIF hoặc WEBP tối đa 5 MB. Máy chủ kiểm tra chữ ký tệp, đặt tên ngẫu nhiên và lưu ảnh dưới `wwwroot/images/uploads`.
- Cột `PicturePath` trong SQL Server chỉ lưu đường dẫn tương đối của ảnh. Tệp ảnh vẫn nằm trên hệ thống tệp trong `wwwroot`; database không tự sao chép hoặc đồng bộ tệp ảnh. Khi triển khai, cần đưa ảnh tĩnh vào gói publish và giữ thư mục ảnh người dùng tải lên bền vững.
- Giỏ hàng lưu trong session; có thể thêm sản phẩm, cập nhật số lượng (1–50), xóa dòng và xem tổng tiền.
- Checkout kiểm tra thông tin nhận hàng, tải lại giá và thông tin phim từ SQL Server, rồi lưu hóa đơn, chi tiết hóa đơn và địa chỉ giao hàng trong cùng một transaction. `ShippingDetail` có quan hệ một-một với `HoaDon`; một hóa đơn có nhiều `ChiTietHoaDon`.
- Các POST thay đổi dữ liệu trong luồng giỏ hàng, checkout, phim và thể loại dùng antiforgery token.
- Tài khoản Identity cần xác nhận email. Cần cấu hình SMTP hợp lệ để gửi liên kết xác nhận ở Production. Email xác nhận đơn hàng gửi sau khi lưu đơn; lỗi SMTP được ghi log và không rollback đơn hàng.

Migration khởi tạo năm phim và ba thể loại mẫu. Trên site Somee, đã nhập thêm năm phim và ba thể loại qua các form quản trị; kiểm tra danh mục sau khi lưu thấy đủ năm tên phim mới và sáu thể loại tổng cộng.

**Tình trạng kiểm tra:** build Release đã thành công với SDK 10.0.404. Bản v3 đã được triển khai lên Somee và trang công khai trả HTTP 200. Browser QA trước deploy xác nhận CRUD, tải poster và giữ ảnh sau sửa phim, giá 5.000 được chấp nhận còn 4.999 bị từ chối, cart Add/Update/Remove, antiforgery khi thiếu token (HTTP 400), và validation checkout. Rating-boundary QA trên v3 xác nhận 0.9 và 5.1 bị từ chối, còn 1, 4.5 và 5 được chấp nhận. Đã xóa các phim thử và xác nhận trang chi tiết trả 404; không tạo đơn trong vòng QA mới.

Windows Smart App Control hiện chặn chạy local binary bằng policy VerifiedAndReputableDesktop (Code Integrity event 3077). Bản source cũ từng chạy để QA; bản hiện tại được kiểm tra trên Somee. Không tắt Smart App Control hoặc thêm ngoại lệ ứng dụng để vượt qua chính sách máy.

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

Dùng SQL Server local hoặc tạo một database rỗng trên hosting. Cấu hình `ConnectionStrings:DefaultConnection` bằng User Secrets khi phát triển, hoặc bằng biến môi trường/cơ chế secrets của hosting. Chuỗi kết nối SQL Server trên Somee trong tài liệu đề có `TrustServerCertificate=True`; xác nhận thông tin máy chủ và database từ tài khoản của chính mình. Không đưa connection string, mật khẩu SQL hoặc Gmail App Password vào GitHub, README hay báo cáo.

Chạy PowerShell tại thư mục `ASP-CORE` để cấu hình secrets local:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "YOUR_SQL_SERVER_CONNECTION_STRING" --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
dotnet user-secrets set "Email:UserName" "YOUR_GMAIL_ADDRESS" --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
dotnet user-secrets set "Email:AppPassword" "YOUR_GMAIL_APP_PASSWORD" --project SourceCode/31241027221_NguyenHoangHung/31241027221_NguyenHoangHung.csproj
```

Có thể đặt các khóa email khác nếu cần: `Email:Host` (`smtp.gmail.com`), `Email:Port` (`587`) và `Email:FromName` (`Góc Phim Hưng`). Trên hosting, dùng `ConnectionStrings__DefaultConnection`, `Email__UserName`, `Email__AppPassword`, `Email__Host`, `Email__Port` và `Email__FromName` trong phần quản lý biến môi trường bí mật của host. Gmail App Password cần được tạo theo quy trình bảo mật hiện hành của Google; không dùng mật khẩu đăng nhập Gmail thông thường.

> **Cài database:** `Database/InstallDatabase.sql` hiện gồm đủ schema đến migration `AlignAssignmentModel` và đã được áp dụng thành công trên database Somee rỗng. Chỉ chạy script này một lần trên database SQL Server mới, rỗng. Với database đã có migration history, dùng `dotnet ef database update` để áp dụng các migration còn thiếu; không chạy lại script cài mới.

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

Website đã chạy tại URL nêu trên và trả HTTP 200. Trong vòng QA trước đây có một đơn giả lập orderId 1, tổng 258.000 VNĐ, dùng dữ liệu không phải địa chỉ thật. SMTP email chưa được xác minh gửi thành công; checkout vẫn lưu đơn khi email gặp lỗi. Không lưu mật khẩu hoặc ảnh chụp lộ connection string vào repository.

## Báo cáo và minh chứng

Báo cáo theo mục 14–15 của đề được lưu tại `Report/BaiTap_CaNhan_ASP_CORE_LMS.docx`. Ma trận có đủ 50 mã `[Hình x.y]` lấy từ đề .NET 10. Ảnh phải là ảnh toàn màn hình do sinh viên tự chụp; file tương ứng có tên `Report/images/Hinh_x_y.png`. Nếu ảnh chưa có, báo cáo ghi “chưa có minh chứng”. Không thay bằng ảnh mẫu từ tài liệu hướng dẫn.

Các ảnh minh chứng thật được lưu theo tên `Hinh_x_y.png`; builder đếm tệp đang có và đánh dấu rõ những vị trí chưa có ảnh thay vì tạo ảnh giả.

Bản hướng dẫn cũ dùng .NET 5 và transcript video dùng ASP.NET MVC 5/.NET Framework trong Visual Studio. Chỉ lấy quy trình chung khi phù hợp; thao tác cũ như `Startup.cs`, cổng cố định, publish profile hay cách lưu ảnh của framework cũ không thay thế hướng dẫn hiện hành .NET 10 và mã nguồn này.
