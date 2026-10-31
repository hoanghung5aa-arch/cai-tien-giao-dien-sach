# AI Usage Log

## 1. Công cụ AI
- ChatGPT và Codex.

## 2. Mục đích sử dụng
- Hỗ trợ phân tích giao diện tham khảo FAHASA và giải thích HTML/CSS.
- Đề xuất, tạo và chỉnh sửa một số phần HTML/CSS cho giao diện danh sách sách.
- Hỗ trợ tìm thông tin sách, ảnh bìa thật, và rà soát cách hiển thị trên nhiều kích thước.

## 3. Prompt chính đã sử dụng
- Phân tích năm khu vực của website FAHASA: trang chủ, danh mục, chi tiết sản phẩm, tìm kiếm và giỏ hàng; phân biệt quan sát với đề xuất.
- Đề xuất bố cục thẻ sản phẩm sách bằng HTML5/CSS3, ưu tiên cách trình bày tên và giá dễ đọc.
- Tạo giao diện danh sách sách tĩnh có ít nhất bốn sản phẩm, CSS riêng và bố cục co giãn.
- Yêu cầu thêm nền cho phần đầu trang, thay bìa minh họa bằng sách thật và ghi tiêu đề chính xác.
- Sau khi em nhận thấy thẻ thứ tư cao hơn các thẻ cùng hàng, em yêu cầu chỉnh bố cục để các thẻ cân đối.

## 4. Kết quả AI hỗ trợ
- Tạo cấu trúc trang và định dạng thẻ sách trong `index.html` và `style.css`.
- Thay ảnh minh họa bằng ảnh bìa của bốn sách tham khảo FAHASA: Nhà Giả Kim (Tái Bản 2025), Hoàng Tử Bé (Tái Bản 2025), Đắc Nhân Tâm và ấn bản bìa cứng Tuổi Trẻ Đáng Giá Bao Nhiêu.
- Thêm nền màu ấm, bố cục Grid/Flexbox, cách hiển thị theo kích thước màn hình và hiệu ứng cho liên kết.
- Đề xuất dùng `align-items: stretch` kết hợp Flexbox để thẻ trong cùng một hàng có chiều cao bằng nhau mà không cắt tên sách.

## 5. Kiểm tra và điều chỉnh
- Codex mở trang cục bộ, kiểm tra ảnh, mục “Xem chi tiết” và bố cục ở chiều rộng 1366 px, 768 px và 390 px; kết quả lần lượt là bốn cột, hai cột và một cột.
- Em tự nhận ra thẻ sách thứ tư cao hơn do tên dài. Em đổi `align-items` từ `start` sang `stretch` trong VS Code, lưu và quan sát hai trạng thái trên trình duyệt. Hai ảnh so sánh được lưu trong `evidence/buoi2-self-adjust-before.png` và `evidence/buoi2-self-adjust-after.png`.
- Các tên sách, bìa và giá được ghi theo thông tin tham khảo trên FAHASA ngày 31/10/2026; giá có thể thay đổi.
- Trước khi nộp, em cần tự bấm kiểm tra các liên kết sản phẩm và xem lại trang trên thiết bị của mình.

## 6. Phần em hiểu và có thể giải thích
- `header`, `nav`, `main`, `section`, `article` và `footer` chia nội dung trang theo vai trò.
- `img` hiển thị ảnh; `alt` mô tả ảnh khi ảnh không tải được hoặc khi dùng trình đọc màn hình.
- `ul` và `li` biểu diễn danh sách sản phẩm; `h1`, `h2`, `h3` tạo thứ bậc tiêu đề.
- `del` gạch ngang giá gốc; `details` và `summary` mở/đóng mô tả bằng HTML.
- Grid chia danh sách thành các cột. `align-items: stretch` giúp các phần tử trong cùng một hàng lấp đầy chiều cao hàng.
- Flexbox bên trong từng mục giúp thẻ sản phẩm lấp đầy phần tử cha; `margin-top: auto` trong `.price-area` đẩy khu vực giá xuống dưới.
- Media query đổi số cột theo chiều rộng màn hình; `:hover` và `:focus-visible` thể hiện trạng thái tương tác chuột và bàn phím.
- Em cần đối chiếu các giải thích này với mã nguồn và thực hành trực tiếp để trả lời câu hỏi của giảng viên.

## 7. Nguồn tham khảo sản phẩm
- [Nhà Giả Kim (Tái Bản 2025)](https://www.fahasa.com/nha-gia-kim-tai-ban-2025.html)
- [Hoàng Tử Bé (Tái Bản 2025)](https://www.fahasa.com/hoang-tu-be-tai-ban-2025.html)
- [Đắc Nhân Tâm](https://www.fahasa.com/dac-nhan-tam-425244.html)
- [Tuổi Trẻ Đáng Giá Bao Nhiêu - Ấn Bản Đặc Biệt 20 Năm Thành Lập Nhã Nam - Bìa Cứng](https://www.fahasa.com/tuoi-tre-dang-gia-bao-nhieu-an-ban-dac-biet-20-nam-thanh-lap-bia-cung.html)
