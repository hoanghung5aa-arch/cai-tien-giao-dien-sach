# E-commerce UI Improvement

## 1. Giới thiệu
Góc sách nhỏ là trang giao diện danh sách sách trong bài tập cá nhân môn Phát triển ứng dụng WEB/TMĐT. Khu vực cải tiến là thẻ sản phẩm, giúp hiển thị đầy đủ tên sách và trình bày thông tin giá rõ ràng hơn.

## 2. Website tham khảo
- Tên website: Nhà sách trực tuyến FAHASA.
- URL: https://www.fahasa.com/
- Khu vực đã khảo sát: Trang chủ, danh mục sản phẩm, chi tiết sản phẩm, tìm kiếm và giỏ hàng.
- Khu vực cải tiến: Thẻ sản phẩm trong danh sách sách.

## 3. Chức năng
- Hiển thị bốn sản phẩm sách mẫu có ảnh bìa, tên và giá.
- Hiển thị giá gốc và tỷ lệ giảm giá với sản phẩm có khuyến mãi.
- Mở và đóng mô tả bằng mục “Xem chi tiết”.
- Hiển thị bốn cột trên màn hình lớn, hai cột trên màn hình vừa và một cột trên điện thoại.

## 4. Công nghệ
- HTML5.
- CSS3.
- Không sử dụng JavaScript, thư viện ngoài, backend hoặc cơ sở dữ liệu.

## 5. Cách chạy
Tải toàn bộ dự án, giữ nguyên các file và thư mục images, sau đó mở file index.html bằng Chrome hoặc Edge.

Cấu trúc dự án:

```text
index.html
style.css
images/
  book-1.svg
  book-2.svg
  book-3.svg
  book-4.svg
README.md
AI_USAGE.md
```

## 6. Cải tiến đã thực hiện
- Vấn đề ban đầu: Một số tên sách dài trên danh mục FAHASA bị rút gọn bằng dấu “…”, gây khó khăn khi phân biệt sản phẩm ngay trong danh sách.
- Cho tên sách hiển thị đầy đủ và tự xuống dòng.
- Đặt ảnh trong khung cùng chiều cao, giữ nguyên tỷ lệ ảnh bìa.
- Làm nổi bật giá bán bằng màu đỏ, gạch ngang giá gốc và thêm nhãn giảm giá.
- Thay đổi số cột theo kích thước màn hình.
- Thu gọn khoảng cách giữa thông tin loại bìa và giá trên điện thoại.

Tên sách, giá, mô tả và ảnh bìa là dữ liệu minh họa được tạo cho bài tập, không phải dữ liệu sản phẩm FAHASA.

## 7. Hỗ trợ từ AI
ChatGPT và Codex hỗ trợ phân tích, đề xuất bố cục, tạo HTML/CSS, bìa minh họa và giải thích mã nguồn. Tôi đã mở trang trên máy tính, bật chế độ giả lập điện thoại và gửi ảnh, đoạn quay để đối chiếu. AI hỗ trợ sửa khoảng cách trên điện thoại. Các prompt chính và nội dung kiểm tra được ghi trong AI_USAGE.md.
