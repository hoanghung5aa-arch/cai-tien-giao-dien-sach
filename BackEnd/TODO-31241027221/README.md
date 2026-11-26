# TODO App — Nguyễn Hoàng Hưng

Ứng dụng quản lý công việc cho môn Phát triển ứng dụng Thương mại Điện tử.

- **Sinh viên:** Nguyễn Hoàng Hưng
- **MSSV:** 31241027221
- **Frontend:** HTML, CSS và JavaScript thuần, giao diện tiếng Việt responsive.
- **Backend:** Node.js HTTP server, API JSON.
- **Database:** SQLite có sẵn trong Node.js; database được tạo tại `data/tasks.sqlite`.

## Chức năng

- Thêm, xem, sửa và xóa công việc; lưu tên, mô tả, mức ưu tiên, hạn hoàn thành.
- Cập nhật trạng thái Cần làm / Đang làm / Hoàn thành.
- Thống kê tổng số việc, số lượng từng trạng thái và tỷ lệ hoàn thành.
- Lọc theo trạng thái, tìm kiếm theo tên và mô tả.
- Kiểm tra dữ liệu nhập và lưu bền vững sau khi đóng ứng dụng.

## Cách chạy

Cần Node.js 22.13 trở lên; không cần cài gói ngoài.

```bash
npm start
```

Mở <http://127.0.0.1:8080>. Nhấn `Ctrl+C` để dừng. File SQLite được tạo tự động trong `data/`.

## API

- `GET /api/tasks` — lấy danh sách và số liệu thống kê.
- `POST /api/tasks` — tạo công việc.
- `PUT /api/tasks/:id` — sửa đầy đủ thông tin.
- `PATCH /api/tasks/:id` — cập nhật trạng thái.
- `DELETE /api/tasks/:id` — xóa công việc.

## Ảnh minh chứng

- [Tổng quan và thống kê](evidence/tong-quan.jpg)
- [Thêm và sửa công việc](evidence/cap-nhat-cong-viec.jpg)
- [Lọc công việc theo trạng thái](evidence/loc-cong-viec.jpg)

## Tệp chính

- `Bai2.js` — server Node.js, API và SQLite.
- `public/Bai1.html` — giao diện chính.
- `public/app.js`, `public/styles.css` — tương tác và giao diện.
- `Bai1_css_demo_cu.html` — bài thực hành CSS ban đầu được giữ kèm.
- `data/` — dữ liệu SQLite cục bộ, không đưa vào Git.


