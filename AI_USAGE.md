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

### Prompt tiêu biểu đã sử dụng trong Buổi 2

> “nè chỗ này nè tôi thấy cái cuốn sách thứ 4 nó đang lạc lõng với các cuốn còn lại chỉnh ngay chỗ đó, đúng yêu cầu thầy chứ”

Prompt trên được dùng để nhờ AI phân tích vấn đề căn thẻ; phần đổi giá trị CSS và quan sát kết quả được em thực hiện trong VS Code.

## 4. Kết quả AI hỗ trợ
- Tạo cấu trúc trang và định dạng thẻ sách trong `index.html` và `style.css`.
- Thay ảnh minh họa bằng ảnh bìa của bốn sách tham khảo FAHASA: Nhà Giả Kim (Tái Bản 2025), Hoàng Tử Bé (Tái Bản 2025), Đắc Nhân Tâm và ấn bản bìa cứng Tuổi Trẻ Đáng Giá Bao Nhiêu.
- Thêm nền màu ấm, bố cục Grid/Flexbox, cách hiển thị theo kích thước màn hình và hiệu ứng cho liên kết.
- Hỗ trợ phân tích vì sao thẻ có tên dài cao hơn và giải thích cách thử `align-items` trong Grid. Phần gợi ý của AI là một phương án để em kiểm tra; em tự thay giá trị trong VS Code và quan sát giao diện.

## 5. Kiểm tra và điều chỉnh
- Em đã tự mở trang trên trình duyệt và kiểm tra ảnh, mục “Xem chi tiết” cùng bố cục ở 1366 px, 768 px và 390 px; kết quả lần lượt là bốn cột, hai cột và một cột.
- Em tự nhận ra thẻ sách thứ tư cao hơn vì tên sách dài. Trong `style.css`, em thử đổi `align-items` trong `.product-list` thành `start`, quan sát các thẻ không cao bằng nhau, rồi đổi thành `stretch` và lưu file. Sau khi tải lại trang, em thấy các thẻ trong cùng một hàng cao bằng nhau mà tên sách không bị cắt. `.product-item { display: flex; }` cùng `.product-card { flex: 1; }` giúp phần thẻ có nền và viền lấp đầy chiều cao hàng. Hai ảnh so sánh trước và sau lần điều chỉnh này được lưu trong `evidence/buoi2-self-adjust-before.png` và `evidence/buoi2-self-adjust-after.png`.
- Các tên sách, bìa và giá được ghi theo thông tin tham khảo trên FAHASA ngày 31/10/2026; giá có thể thay đổi.
- Em đã dùng chuột để kiểm tra hover, nhấn Tab để kiểm tra focus-visible và bấm liên kết sản phẩm; các thao tác hoạt động trong lần kiểm tra. Em sẽ mở lại website để trình bày trực tiếp nếu giảng viên yêu cầu.

## 6. Phần em hiểu và có thể giải thích
- `header`, `nav`, `main`, `section`, `article` và `footer` chia nội dung trang theo vai trò.
- `img` hiển thị ảnh; `alt` mô tả ảnh khi ảnh không tải được hoặc khi dùng trình đọc màn hình.
- `ul` và `li` biểu diễn danh sách sản phẩm; `h1`, `h2`, `h3` tạo thứ bậc tiêu đề.
- `del` gạch ngang giá gốc; `details` và `summary` mở/đóng mô tả bằng HTML.
- Grid chia danh sách thành các cột. Trong `.product-list`, `align-items: stretch` kéo các mục trong cùng một hàng theo chiều cao của mục cao nhất. `display: flex` trên `.product-item` và `flex: 1` trên `.product-card` giúp thẻ lấp đầy chiều cao đó.
- Flexbox bên trong từng mục giúp thẻ sản phẩm lấp đầy phần tử cha; `margin-top: auto` trong `.price-area` đẩy khu vực giá xuống dưới.
- Media query đổi số cột theo chiều rộng màn hình; `:hover` và `:focus-visible` thể hiện trạng thái tương tác chuột và bàn phím.
- Phần này là nội dung ôn tập; khi trả lời, em đối chiếu từng khái niệm với mã nguồn thực tế của mình.

## 7. Nguồn tham khảo sản phẩm
- [Nhà Giả Kim (Tái Bản 2025)](https://www.fahasa.com/nha-gia-kim-tai-ban-2025.html)
- [Hoàng Tử Bé (Tái Bản 2025)](https://www.fahasa.com/hoang-tu-be-tai-ban-2025.html)
- [Đắc Nhân Tâm](https://www.fahasa.com/dac-nhan-tam-425244.html)
- [Tuổi Trẻ Đáng Giá Bao Nhiêu - Ấn Bản Đặc Biệt 20 Năm Thành Lập Nhã Nam - Bìa Cứng](https://www.fahasa.com/tuoi-tre-dang-gia-bao-nhieu-an-ban-dac-biet-20-nam-thanh-lap-bia-cung.html)
