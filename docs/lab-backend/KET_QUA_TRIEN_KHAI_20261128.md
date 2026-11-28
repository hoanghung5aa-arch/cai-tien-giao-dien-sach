# Kết quả cập nhật LAB ngày 28/11/2026

- Sửa khoảng chừa banner cố định theo chiều cao thực và đồng bộ menu ở breakpoint 992px. Giữ quảng cáo Somee.
- Hai dịch vụ email khai báo UTF-8 cho subject/body. Build Release: 0 lỗi, 0 cảnh báo; publish thành công bằng SDK 10.0.404.
- Gói delta chứa đúng 3 tệp: DLL ứng dụng, wwwroot/css/site.css, wwwroot/js/site.js. Somee xác nhận giải nén 3 tệp, 523,6 KB. Website đã khởi động lại; trang chủ HTTP 200; CSS và JavaScript public khớp source.
- Đơn thử #3 và #4 đều 159.000 VNĐ, Avengers: Doomsday ×1. Đơn #4 chạy sau deploy; trang hoàn tất xác nhận đã lưu và gửi email. Gmail xác minh thư #4 trong Inbox và Sent, đúng tổng tiền và dữ liệu thử. Không dùng thông tin giao hàng thật.
- Trang xác nhận tài khoản nhxmynhh… báo email đã được xác nhận. Người dùng đã nhập mật khẩu; đã xác minh menu Tài khoản/Đăng xuất sau đăng nhập thành công.
- Có thêm ảnh thật cho đăng nhập thành công, website local và form chỉnh sửa; các ảnh trước gồm: tài khoản đã xác nhận, giỏ hàng, Checkout đã điền, đơn hoàn tất và thư thực nhận. Đã cắt thanh Chrome; ảnh không có taskbar/con trỏ trong nội dung. Chỉ cắt khung ảnh, không thay đổi kết quả website.
- Builder chèn ảnh trực tiếp trong DOCX, chú thích bên dưới, không dùng hyperlink thay hình. Word cuối chưa tạo khi kiểm tra local/luồng còn tiếp tục.
- Hiện có 4/50 mã hình có tệp được duyệt và 8 ảnh bổ sung. Còn 46 mã cần chụp đúng nội dung; bộ ảnh cũ đã loại không được tính lại. Các trạng thái lịch sử/cảnh báo phải được tái hiện thật trên bản sao, không dựng ảnh.

- Bản local tại http://127.0.0.1:5080 đã mở được trong trình duyệt với dữ liệu phim; đã lưu ảnh minh chứng.

## Kiểm tra cuối

- 20/20 kiểm tra HTTP đạt; phim QA được xóa sau kiểm tra, không đổi dữ liệu chính.
- SQL chỉ đọc: Movies 10, Genres 6, HoaDons 4, ChiTietHoaDons 4, ShippingDetails 4, AspNetUsers 1 và đã xác nhận.
- Bộ ảnh hiện có 11 mã theo đề và 9 ảnh bổ sung. Còn 39 cảnh lịch sử chưa có ảnh đúng chú thích; không đánh dấu hoàn tất 50/50.
- Báo cáo Word mới nhúng trực tiếp 20 ảnh minh chứng, giữ khung bìa/logo UEH và cập nhật mục lục.

- Chốt sau rà soát: tránh vòng MutationObserver tự kích hoạt bằng cách chỉ đổi CSS variable khi chiều cao thật thay đổi. Deploy lại ba tệp thành công (523,77 KB); HTTP 200 và JS live khớp source. Hiện có 14 mã hình và 11 ảnh bổ sung, còn 36 cảnh lịch sử theo đề.

- GitHub đã nhận source d9012ff. Bộ Word cuối có 14 mã hình và 12 ảnh bổ sung (26 ảnh nhúng); giữ bìa UEH và mục lục. Còn 36 cảnh lịch sử trong ma trận, không tuyên bố đủ 50/50.


## Cập nhật báo cáo sau bổ sung ảnh

Word 75 trang đã nhúng trực tiếp 65 ảnh minh chứng, cập nhật mục lục và kiểm tra render. Có ảnh cho 46/50 mã theo đề; 3.3, 5.5, 7.1 chỉ một phần. Bốn cảnh chưa chụp được: 5.4, 5.7, 6.1, 7.4 do Windows Application Control chặn DLL bản tái hiện. Chi tiết trong TIEN_DO_ANH_TAI_HIEN_20261128.md và ma trận CSV. Không thay đổi source/deploy production trong lượt bổ sung báo cáo này.


## Bổ sung cuối ngày 28/11: bốn mã ảnh và ba vị trí cần làm rõ

Đã hoàn tất chụp và nhúng thêm cho 5.4, 5.7, 6.1, 7.4; thay/bổ sung 3.3, 5.5, 7.1. Báo cáo hiện 80 trang, 50/50 mã có ảnh, 9 ảnh đi kèm và 12 ảnh mục 14, tổng 71 ảnh minh chứng (73 hình nhúng tính cả bìa). Các ảnh mới không có thanh Chrome, taskbar hay con trỏ. Hình 6.1 là nhật ký scaffold thành công mở trong editor, chưa đúng khung Terminal; được đánh dấu ảnh một phần, không tuyên bố đáp ứng đủ mọi chi tiết của đề. Các trạng thái lỗi/Index rỗng được tái hiện trên SQL Express local, không phải lỗi production. Đã render và kiểm tra lại Word, cập nhật mục lục và sao chép vào file người dùng.
