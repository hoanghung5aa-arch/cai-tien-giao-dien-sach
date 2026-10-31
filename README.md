# E-commerce UI Improvement

## 1. Giới thiệu
Đây là giao diện danh sách sách tĩnh được tiếp tục từ bài tập Buổi 1 môn Phát triển ứng dụng WEB/TMĐT. Khu vực tập trung cải tiến là danh sách và thẻ sản phẩm sách.

## 2. Website tham khảo
- Tên website: Nhà sách trực tuyến FAHASA.
- URL: https://www.fahasa.com/
- Khu vực tham khảo: danh mục và trang sản phẩm sách.
- Khu vực cải tiến: danh sách sách và cách trình bày thẻ sản phẩm.

## 3. Chức năng
- Hiển thị bốn sách tham khảo với ảnh bìa, tên, tác giả, thông tin bìa và giá.
- Hiển thị giá gốc, giá bán và mức giảm được ghi nhận tại thời điểm khảo sát.
- Mở/đóng mô tả sách bằng `details` và `summary`.
- Có liên kết tới trang sản phẩm tương ứng trên FAHASA.
- Tự đổi bố cục danh sách theo chiều rộng màn hình.

## 4. Công nghệ và kỹ thuật
- HTML5: `header`, `nav`, `main`, `section`, `article`, `footer`, tiêu đề, danh sách, ảnh có `alt`, liên kết và `details`/`summary`.
- CSS3: selector, Box Model, Flexbox, Grid, media query, `:hover` và `:focus-visible`.
- Không dùng JavaScript, backend, cơ sở dữ liệu hay thư viện giao diện bên ngoài.

## 5. Cách chạy
Mở `index.html` bằng trình duyệt. Giữ nguyên `style.css` và thư mục `images` để trang tải đúng định dạng và ảnh bìa.

## 6. Cải tiến đã thực hiện
- Thêm nền màu ấm cho phần đầu trang.
- Thay bốn bìa minh họa bằng ảnh sách thật và cập nhật thông tin sách tham khảo từ FAHASA.
- Giữ đầy đủ tên sách và cho phép tên dài tự xuống dòng.
- Căn các thẻ trong cùng một hàng cao bằng nhau bằng Grid kết hợp Flexbox. Khi thấy thẻ thứ tư cao hơn, em đã thử đổi `align-items` từ `start` sang `stretch` và quan sát kết quả trong trình duyệt.
- Bố trí bốn cột trên màn hình lớn, hai cột trên màn hình vừa và một cột trên điện thoại.

Giá sách được ghi nhận ngày 31/10/2026 và có thể thay đổi. Trang chỉ minh họa giao diện, không xử lý đặt hàng.

## 7. Hỗ trợ từ AI
Prompt, kết quả AI hỗ trợ, việc em đã tự kiểm tra và phần cần kiểm tra tiếp được ghi trong [AI_USAGE.md](AI_USAGE.md).

## 8. Ảnh minh chứng
- Trước khi cải tiến: [buoi2-before.png](evidence/buoi2-before.png).
- Trước khi đổi `start` sang `stretch`: [buoi2-self-adjust-before.png](evidence/buoi2-self-adjust-before.png).
- Sau khi đổi sang `stretch`: [buoi2-self-adjust-after.png](evidence/buoi2-self-adjust-after.png).
- Giao diện sau cải tiến: [máy tính](evidence/buoi2-after-1366.png), [máy tính bảng](evidence/buoi2-after-768.png), [điện thoại](evidence/buoi2-after-390.png).
- Kết quả kiểm tra: [KIEM_TRA.txt](KIEM_TRA.txt).

## 9. GitHub
- Repository: https://github.com/hoanghung5aa-arch/cai-tien-giao-dien-sach
- GitHub Pages: https://hoanghung5aa-arch.github.io/cai-tien-giao-dien-sach/
