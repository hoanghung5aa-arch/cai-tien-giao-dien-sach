# Đối chiếu và kế hoạch hoàn thiện LAB - 28/11/2026

## Kết luận

Chưa hoàn thiện toàn bộ theo đề. Chức năng chính đã có trong source; bản web hiện có 10 phim thật và 6 thể loại. Font Noto Sans và poster mới đang hoạt động ở các màn hình vừa xem. Các phần chưa chốt là xác nhận tài khoản/đăng nhập, email đơn hàng, vòng kiểm tra chức năng trên bản cuối, minh chứng dữ liệu tự nhập, minh chứng local và bộ ảnh theo đề. README và một số số liệu trong Word còn cũ. Word hoàn thiện sau cùng.

Bản này thay trạng thái cũ của các kế hoạch trước. Đây là kết quả audit và kế hoạch, không phải tuyên bố đã chạy lại mọi thao tác, deploy hay gửi thư trong phiên này.

## Căn cứ đã kiểm tra

- [Đề .NET 10 hiện hành](https://drive.google.com/file/d/1lyr8_vkYUxNXR2uwQgxLO_mcpsR9o_YP/view): 41 trang, 683.403 bytes, Drive sửa ngày 16/11/2026. Mục 12-16 ở trang 38-40; có đúng 50 mã [Hình x.y]. Đã đọc nội dung qua Drive và xem các trang yêu cầu/rubric.
- [PDF cũ](https://drive.google.com/file/d/1iqktMdR1E7kfZdnU5iE1MujOEBwY6jYu/view): 48 trang, 4.000.018 bytes, bản .NET 5 năm 2023. Hai hyperlink đầu đề mới trỏ đến PDF này và video bên dưới.
- [Video của thầy](https://www.youtube.com/watch?v=kvruKkwPPWw): 1 giờ 11 phút 16 giây, xuất bản 07/12/2023. Đã lấy transcript đầy đủ 818 dòng trong phiên này, đối chiếu với bản người dùng đã cung cấp. Video dùng MVC 5/.NET Framework; đoạn 1:06:42 trở đi minh họa nhập phim trực tiếp và sửa bố cục. Mô tả video không có tài liệu bổ sung khác.
- Liên kết hosting huynhthaihung.com trong PDF cũ hiện trả 404. Hướng dẫn Microsoft cũ không đọc được qua web fetch; đã mở [hướng dẫn MVC .NET 10 chính thức](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-10.0&tabs=visual-studio-code). Các link cài VS Code/SDK và trang bảo mật/App Password là tài liệu công cụ, không thêm đầu việc chấm điểm ngoài đề.
- [Somee chính thức](https://somee.com/FreeAspNetHosting.aspx) xác nhận gói free chèn quảng cáo. Điều chỉnh khoảng trống của ứng dụng để nội dung cuộn ra khỏi banner.
- [Google App Password](https://support.google.com/accounts/answer/185833): mật khẩu ứng dụng dùng với xác minh hai bước. Cấu hình tên Gmail và App Password phải cùng tài khoản; cấu hình thật giữ ngoài Git.
- Dùng Unslop của Damir Sokolovsky (https://heydamir.design/unslop), CC BY 4.0, cho lượt rà giao diện.

## Trạng thái thực tế

| Hạng mục | Đã ghi nhận | Còn thiếu để kết luận đạt |
| --- | --- | --- |
| .NET và môi trường | Source target net10.0; SDK cục bộ 10.0.404 tại C:/Users/Admin/.dotnet; nhật ký trước ghi build Release thành công | Ghi nhận lại build/run/debug trên bản cuối; phiên này chưa rebuild. Lệnh dotnet trong PATH có thể trỏ đến thư mục không có SDK |
| Database | Source có Identity, Movies, Genres, HoaDons, ChiTietHoaDons, ShippingDetails và migration | Ảnh SQL/migration history hiện tại; đối chiếu bản DB thực và source; ảnh dữ liệu được lưu sau thao tác |
| CRUD, upload | Code có CRUD, kiểm tra giá/rating/ngày/quan hệ, giữ poster khi không thay và kiểm tra file ảnh | Vòng thao tác cuối trên bản chốt và bằng chứng phù hợp |
| Giỏ hàng, checkout | Code có session, cập nhật số lượng 1-50, tính lại giá từ DB, lưu đơn trong transaction và gửi email sau commit | Kiểm tra giỏ/đơn/tổng tiền và email trên bản chạy cuối |
| Nội dung | Live hiện 10 phim và 6 thể loại; 10 poster load được sau khi cuộn | Đối chiếu tối thiểu 5 phim + 3 thể loại đã nhập qua form theo mục 12. Số lượng hiện có/seed/script chưa tự chứng minh cách nhập |
| Font và poster | Desktop 1280x720 và mobile 390x844: Noto Sans loaded; heading và control dùng cùng font; danh mục/form thêm phim không tràn ngang | Rà các trang còn lại, trạng thái lỗi/đăng nhập và cold load. Chưa tái hiện lỗi dấu hoặc nhảy font ở các trang vừa xem |
| Tài khoản/email | Gmail kết nối tìm thấy 1 thư “Confirm your email”, nhãn SENT, tên Góc Phim Hưng, lúc 00:33 ngày 28/11 giờ Việt Nam | Chưa chứng minh người nhận đã nhận/kích hoạt và đăng nhập thành công; không tìm thấy email đơn hàng trong truy vấn liên quan. Không suy ra SMTP chắc chắn hỏng hay chắc chắn hoàn tất từ kết quả tìm kiếm này |
| GitHub | Repo hiện có Public; remote main và local HEAD cùng ebf20c8, đã có ASP-CORE và Word/ảnh cập nhật | README còn mô tả deploy 26/11 với 5 SVG; cập nhật trạng thái cuối và kiểm tra ảnh/secrets trước commit tiếp |
| Minh chứng | Có 4 ảnh mới: Hinh_7_6.png và 3 ảnh Audit bổ sung; DOCX đang nhúng đúng 4 ảnh | 49/50 mã chưa có ảnh sạch tương ứng. Hình 7.6 đã có tệp nhưng còn cần khung chụp tốt hơn: đầu poster bị khuất do cuộn, banner che đáy. Nền đen của teaser logo không phải ảnh lỗi |
| Word | Có bản đang dựng và mục lục | Chưa phải bản nộp hoàn chỉnh; số liệu 5/3 và 10/6 đang mâu thuẫn; còn thiếu minh chứng. Đối chiếu bìa/mục lục với BTVN5 - Copy.docx theo yêu cầu người dùng trước khi chốt |

## Hướng Unslop

- Sản phẩm: cửa hàng phim tiếng Việt để duyệt, lọc và đặt hàng.
- Tính cách: ấm, dễ đọc, có chất điện ảnh.
- Thế giới: chương trình chiếu phim trên giấy kem, dùng xanh rừng đã có.
- Điểm nhấn: poster hãng phim mang màu sắc; tên phim, ngày, giá và nút đặt hàng rõ ràng.
- Dấu nhận diện: biểu tượng dải phim của Góc Phim Hưng.

Rà theo thứ tự khoảng cách, màu, icon, câu chữ, chữ, bố cục, chi tiết, hình ảnh, chuyển động. Bản hiện tại chỉ cần chỉnh các lỗi còn thấy; giữ Noto Sans hỗ trợ tiếng Việt và bộ poster đã được thay.

## Kế hoạch thực hiện theo thứ tự

### 1. Chốt giao diện và chữ - ưu tiên P1

- Sửa khoảng trống cuối trang để footer, nút cuối form và nội dung cuối danh mục cuộn hoàn toàn ra khỏi quảng cáo Somee ở desktop/mobile. Nguồn: site.css dòng 46-50 đặt padding-bottom 80px, trong khi banner mobile có thể xuống nhiều dòng.
- Đồng bộ breakpoint menu: _Layout.cshtml dùng navbar-expand-lg (992px); CSS riêng bắt đầu ở max-width 900px. Kiểm tra và điều chỉnh riêng nhóm menu cho cỡ 901-991px.
- Rà danh mục, chi tiết, thêm/sửa/xóa phim, thể loại, giỏ, checkout, cảm ơn, đăng ký/xác nhận và đăng nhập. Kiểm tra chữ dài, tên phim, số tiền, thông báo lỗi, file upload và menu đã đăng nhập.
- Kiểm tra 390px, 768px, 960px và desktop; font tải lần đầu và sau reload. Chỉ sửa lỗi fallback/giật chữ khi tái hiện được; ghi rõ trang và chiều rộng gây lỗi.
- Login trong source đã Việt hóa. Dune đã giảm từ 1,99 MB xuống 98.484 bytes, Jumanji từ 1,31 MB xuống 271.357 bytes; không lặp lại hai việc này.
- 10 poster hiện đều là tài sản hãng, có nguồn trong ASP-CORE/Report/poster-sources.md. Frozen 3, Shrek 5, Incredibles 3 là teaser logo; không gọi là thiếu ảnh/AI. Giữ tỉ lệ 2:3 và object-fit:contain.

Đạt khi: dấu không bị cắt, chữ không tràn, không nhảy bố cục sau font load; menu hoạt động ở breakpoint giữa; poster đủ ảnh; nút/footer cuộn ra khỏi banner.

### 2. Xác nhận tài khoản và SMTP - ưu tiên P0

- Dùng tài khoản thử đã có, kiểm tra đúng Gmail gửi trên host và App Password của Gmail đó. Cấu hình local trống là chủ ý dùng secrets; không mặc định coi đó là cấu hình host.
- Kiểm tra thư xác nhận ở hộp nhận, mở link, thấy kết quả xác nhận; đối chiếu EmailConfirmed hoặc đăng nhập thành công. Thử gửi lại xác nhận nếu cần.
- Tạo một đơn thử với hộp thư của người dùng và thông tin thử; xác minh đơn/chi tiết/giao hàng được lưu, giỏ được dọn, trang hoàn tất và email thực nhận đúng sản phẩm/số lượng/tổng tiền.
- Nếu lỗi, đọc phân loại log SMTP hiện có (settings, authentication, connection/TLS, timeout) để sửa đúng nguyên nhân, rồi chạy lại phần lỗi. Không đổi mật khẩu tùy đoán.
- Ghi riêng ba bằng chứng: SMTP nhận yêu cầu gửi, người nhận nhận thư, tài khoản xác nhận/đăng nhập. Thư tài khoản và thư đơn hàng là hai luồng riêng.

Đạt khi: xác nhận tài khoản và đăng nhập thành công; có kết quả thực nhận email đơn hàng. Nếu hosting thực sự chặn SMTP, ghi đúng lỗi và đơn đã lưu theo hướng dẫn xử lý lỗi của thầy, không báo gửi thành công.

### 3. Kiểm tra chức năng và dữ liệu - ưu tiên P0

| Nhóm | Kiểm tra trên bản chốt |
| --- | --- |
| Phim/thể loại | Thêm/xem/sửa/xóa bản ghi thử; giữ ảnh khi sửa không chọn tệp; thể loại có phim không bị xóa; tên thể loại trùng không tạo được |
| Validation | Giá 4.999/5.000; rating 0,9/1/4,5/5/5,1; ngày quá khứ/hôm nay/ngày mai; chuỗi ngắn/chỉ khoảng trắng; thể loại không tồn tại |
| Upload | File ảnh hợp lệ, file giả/sai định dạng/quá 5 MB, ảnh mặc định; xem lại đường dẫn DB và ảnh sau reload |
| Giỏ hàng | Thêm trùng, cập nhật 1 và 50, giá trị ngoài giới hạn, xóa, giỏ rỗng; số lượng và tổng tiền trên menu khớp trang giỏ |
| Checkout | Dữ liệu thiếu/sai không lưu; dữ liệu đúng lưu đủ ba bảng; tổng tiền lấy từ DB; SMTP lỗi không làm mất đơn |
| Dữ liệu tự nhập | Xác định tối thiểu 5 phim và 3 thể loại từ thao tác form; ghi các bước/chụp kết quả lưu thực. Không dùng seed/script làm bằng chứng nhập tay |
| Local và DB | Build/run/debug, truy cập local, kết nối SQL Server, đối chiếu migration history và dữ liệu live. Làm rõ ngày máy chủ so với ngày Việt Nam khi thử biên ngày |

Đạt khi: có nhật ký ngày/phiên bản/kết quả thực, dữ liệu và tổng tiền đúng. Nếu Windows còn chặn binary local, xác định cách chạy trong môi trường phát triển được phép và ghi đúng giới hạn còn lại.

### 4. Build/publish/deploy bản chốt - ưu tiên P1

- Dùng SDK .NET 10 tại đường dẫn đúng; restore/build Release và publish ra thư mục mới sau khi các sửa đổi hoàn tất.
- Đối chiếu DLL, views biên dịch, CSS/fonts và toàn bộ poster với source; web.config/DLL/wwwroot phải nằm ở gốc gói, không thư mục bọc.
- Giữ cấu hình SQL/SMTP và ảnh người dùng trên host. Giữ default.asp theo lựa chọn đã có của người dùng; không tạo lại DB hoặc chạy lại script refresh nếu dữ liệu live đã đúng.
- Áp dụng migration thiếu có đối chiếu trước; source migration đổi seed ID 1-5 còn RefreshRealFilmCatalog.sql xử lý cả ID 16-20, không đồng nhất hai việc này.
- Sau deploy chỉ lặp lại các kiểm tra bị ảnh hưởng và một luồng chính từ chọn phim đến hoàn tất đơn/tài khoản. Ghi phiên bản thực chạy và kết quả.

### 5. Chụp minh chứng theo ma trận - ưu tiên P0 cho bài nộp

[MA_TRAN_MINH_CHUNG_20261128.csv](MA_TRAN_MINH_CHUNG_20261128.csv) có đủ 50 mã, số trang PDF, chú thích gốc, tên tệp và việc cần làm. Số lượng hiện tại: 1 mã có tệp, 49 mã thiếu; ngoài ra có 3 ảnh Audit bổ sung. Có tệp không tự động nghĩa là ảnh đã đạt yêu cầu.

- Ưu tiên ảnh bắt buộc mục 14: VS Code/cấu trúc dự án, local, CRUD, upload, thể loại/DB, giỏ/tổng tiền, checkout/hoàn tất, hosting/URL, GitHub/README; sau đó hoàn thành từng mã còn lại.
- Chụp đúng thời điểm/kết quả theo chú thích. Hình 5.1 cần menu tạo file/class; không thay bằng đoạn code Movie. Hình 7.1 cần cây thư mục ảnh mặc định; không dùng preview SVG cũ.
- Hình 2.2/5.4/5.7 là trạng thái lịch sử (cảnh báo chứng chỉ/lỗi DB/danh mục rỗng). Chỉ chụp lại quy trình thật trên bản sao phát triển phù hợp, ghi thời điểm/phạm vi; không làm hỏng ứng dụng đang chạy hoặc dựng ảnh lỗi.
- Ảnh giao diện lấy toàn vùng nội dung trang, không con trỏ/quầng sáng, không thanh Chrome, không taskbar; không cần F11. Chờ font và ảnh tải đủ. Không xóa poster/chữ/nút để làm ảnh trông đúng.
- Một số mã, đặc biệt 2.3/10.7, yêu cầu URL. Lưu thêm ảnh có địa chỉ hoặc chú thích bằng chứng URL rõ ràng; bộ ảnh giao diện sạch không thay thế phần URL bắt buộc.
- Ảnh code/SQL/terminal giữ đủ tên công cụ, file và kết quả cần kiểm tra; che password, connection string thật, App Password và token. Không đưa link xác nhận còn hiệu lực vào ảnh công khai.
- Rà tất cả ảnh cũ/Word: dùng ảnh hiện tại cho trạng thái cuối; ảnh lịch sử hợp lệ phải ghi rõ là lịch sử và có mục đích. Hình 7.6 cần chụp khung tốt hơn để thấy poster và nội dung, tránh banner đè khu vực minh chứng.

Đạt khi: mỗi hình khớp chú thích, sạch con trỏ, đủ thông tin; không lấy ảnh ví dụ trong đề hay ảnh giả. Chưa đủ thì ghi thiếu rõ ràng.

### 6. Đồng bộ README/GitHub và làm Word cuối cùng - ưu tiên P1

- Cập nhật README từ cùng kết quả cuối: ngày deploy, 10 poster/nguồn, Noto Sans, kết quả SMTP tài khoản và đơn hàng, số liệu DB, cách restore/configure/run/publish. Thay thông tin deploy 26/11 với 5 SVG đã lỗi thời.
- Cập nhật builder/Word: thống nhất 10 phim/6 thể loại live với 5 phim/3 thể loại seed; không ghi hai số này như cùng một số liệu DB hiện tại. Chú thích 50 ảnh lấy từ CSV chuẩn.
- Kiểm tra cấu hình/ảnh không lộ secrets; chỉ stage ASP-CORE và tài liệu LAB đã xem. Giữ các bài JavaScript và bài nộp cũ; không tạo repo mới.
- Push bản chốt lên repo Public hiện có, đọc lại remote, chụp thư mục ASP-CORE/README sau push.
- Dùng BTVN5 - Copy.docx làm căn cứ bìa/mục lục theo yêu cầu; giữ phần người dùng muốn giữ, cập nhật nội dung LAB và ảnh cuối. Xuất BaiTap_CaNhan_ASP_CORE_LMS.docx, cập nhật mục lục, render và xem từng trang trước giao.
- Gói nộp cuối gồm Word + URL GitHub/thư mục ASP-CORE + URL website. Nộp LMS là bước riêng sau khi tài liệu đạt và theo yêu cầu lớp.

## Phân công và điều kiện hoàn tất

| Luồng | Nhiệm vụ | Bàn giao |
| --- | --- | --- |
| UI / Unslop | Banner/menu, rà chữ và các màn hình còn lại | Source sửa đúng lỗi + kết quả desktop/mobile |
| Backend / SMTP | Tài khoản, email đơn, validation/cart/checkout, DB | Nhật ký kết quả thật và các bằng chứng tương ứng |
| Tích hợp | Build/publish/deploy bản đã sửa | Gói/phiên bản khớp source và kết quả sau deploy |
| Minh chứng / GitHub | Ma trận 50 hình, ảnh mục 14, README/commit | Ảnh đúng cảnh, remote Public có bản cuối |
| Word | Bìa, mục lục, nội dung và ảnh cuối | DOCX đọc được, không mâu thuẫn số liệu, đủ nội dung mục 14-15 |

Ba agent Luna xhigh đã audit độc lập các phần source/yêu cầu, backend/email, giao diện. Kết quả lượt này: chốt lại kế hoạch và CSV 50 hình; không sửa source, deploy, gửi email hay chỉnh Word trong lượt lập kế hoạch.
