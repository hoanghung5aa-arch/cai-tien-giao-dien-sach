# AI Usage Log

## 1. Công cụ AI
- ChatGPT: hỗ trợ phân tích website.
- Codex: hỗ trợ khảo sát, đề xuất thiết kế, tạo và chỉnh sửa mã nguồn.

## 2. Mục đích sử dụng
- Phân tích giao diện FAHASA.
- Đề xuất bố cục thẻ sách ở mức HTML/CSS cơ bản.
- Tạo mã nguồn, ảnh bìa minh họa và hướng dẫn chạy trang.
- Giải thích HTML/CSS và hỗ trợ điều chỉnh giao diện điện thoại.

## 3. Prompt đã sử dụng
Các prompt chính được chuẩn hóa cách trình bày từ nội dung trao đổi:

### Prompt 1 - Phân tích website
Bạn là chuyên gia phân tích nghiệp vụ và thiết kế trải nghiệm người dùng cho website thương mại điện tử. Tôi đang khảo sát website FAHASA: https://www.fahasa.com/.

Hãy lập bảng phân tích năm khu vực: Trang chủ, Danh mục sản phẩm, Chi tiết sản phẩm, Tìm kiếm và Giỏ hàng. Với mỗi khu vực, nêu mục đích chức năng, thành phần giao diện cần có, điểm thuận tiện, một vấn đề có thể phát sinh và một đề xuất cải tiến. Không khẳng định tính năng chưa được kiểm chứng. Phân biệt nhận xét dựa trên quan sát và đề xuất của AI.

### Prompt 2 - Đề xuất giao diện
Tôi đang làm bài tập HTML5/CSS3 và muốn cải tiến thẻ sản phẩm sách, đặc biệt cách hiển thị tên sách dài. Đối tượng sử dụng là khách hàng mua sắm trực tuyến tại Việt Nam. Hãy đề xuất bố cục, thành phần HTML, tổ chức CSS, cách hiển thị trên máy tính và điện thoại, các điểm cần kiểm tra. Giải thích phương án trước, chưa tạo toàn bộ mã nguồn. Thiết kế ở mức sinh viên, dễ hiểu và tự sửa.

### Prompt 3 - Tạo mã nguồn
Dựa trên phương án đã chọn, tạo một trang HTML5/CSS3 có tiêu đề và ít nhất bốn sản phẩm, mỗi sản phẩm có hình ảnh, tên, giá và nút thao tác. Hỗ trợ máy tính và điện thoại, CSS tách riêng, không sử dụng backend, cơ sở dữ liệu hoặc thư viện không cần thiết. Giải thích cách chạy và kiểm tra. Thiết kế theo mức sinh viên.

### Yêu cầu hỗ trợ tiếp theo
Hướng dẫn mở trang, bật giả lập điện thoại, nhận xét ảnh và đoạn quay, giải thích và điều chỉnh khoảng cách trong CSS.

## 4. Kết quả AI đề xuất
- Một trang danh sách gồm bốn thẻ sách mẫu, tên sách không bị cắt bằng dấu ba chấm.
- CSS Grid chia cột, Flexbox sắp xếp nội dung trong thẻ và media query đổi bố cục theo chiều rộng.
- Ảnh bìa SVG minh họa lưu trong thư mục images.
- Giá bán màu đỏ, giá gốc gạch ngang và nhãn giảm giá.
- Dùng details/summary để mở mô tả trong thẻ, thay cho phương án ban đầu là liên kết sang FAHASA.

## 5. Kiểm tra và điều chỉnh
- Tôi đã mở trang trên máy tính, bật chế độ giả lập điện thoại và gửi ảnh, đoạn quay để đối chiếu giao diện.
- Tôi đã phản hồi nhận xét về quảng cáo lớn che trang vì ảnh khảo sát của tôi không có cửa sổ này; nhận xét đã được thay bằng nhận xét về diện tích banner.
- AI hỗ trợ điều chỉnh CSS trên điện thoại: giảm margin-bottom của .edition từ 18px xuống 8px; đặt margin-top và padding-top của .price-area về 0.
- Sau lần sửa cuối, còn cần tải lại trang, so sánh khoảng cách, thử mở/đóng mô tả cả bốn thẻ và kiểm tra tràn ngang. Nhật ký chưa ghi các thao tác này là đã hoàn thành.

## 6. Phần tôi hiểu và có thể giải thích
Nội dung giải thích để đối chiếu khi tự đọc mã:
- header, main, section, article: tổ chức phần đầu trang, nội dung chính, danh sách và từng sản phẩm.
- img và alt: hiển thị ảnh và cung cấp mô tả thay thế.
- h1 là tiêu đề trang, h2 là tiêu đề danh sách, h3 là tên sách.
- del tạo giá gốc gạch ngang.
- details và summary mở/đóng mô tả bằng HTML.
- Grid chia cột sản phẩm; gap tạo khoảng cách giữa thẻ.
- object-fit: contain giữ ảnh bìa không bị cắt.
- margin tạo khoảng cách bên ngoài; padding tạo khoảng đệm bên trong.
- Media query áp dụng CSS theo kích thước màn hình.

Tôi cần tự đối chiếu các giải thích này với mã nguồn trước khi nộp.
