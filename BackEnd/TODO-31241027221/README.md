# Orderly — Quản lý đơn hàng

Ứng dụng thực tiễn quản lý đơn hàng của Nguyễn Hoàng Hưng (MSSV 31241027221), dùng HTML/CSS/JavaScript, Node.js và SQLite.

## Chức năng

- Thêm, xem, sửa và xóa đơn hàng; lưu thông tin khách hàng, sản phẩm, tổng tiền và ghi chú.
- Cập nhật trạng thái: Chờ xử lý, Đang giao, Hoàn thành, Đã hủy.
- Thống kê đơn theo trạng thái và doanh thu của đơn Hoàn thành.
- Tìm kiếm/lọc đơn hàng; dữ liệu lưu bền vững trong SQLite.

## Chạy ứng dụng

Cần Node.js 22.13 trở lên. Tại thư mục dự án chạy `npm start`, sau đó mở <http://127.0.0.1:8080>.

## API

- `GET /api/orders` — danh sách và thống kê.
- `POST /api/orders` — tạo đơn.
- `PUT /api/orders/:id` — cập nhật thông tin.
- `PATCH /api/orders/:id` — đổi trạng thái.
- `DELETE /api/orders/:id` — xóa đơn.

## Ảnh minh chứng

- [Bảng điều khiển và thống kê](evidence/quan-ly-don-hang.png)
- [Biểu mẫu tạo đơn](evidence/tao-don-hang.png)
