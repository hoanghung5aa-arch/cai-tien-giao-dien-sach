# CSS nâng cao – Lab 07

## Thông tin sinh viên

- **Họ và tên:** Nguyễn Hoàng Hưng
- **Mã số sinh viên:** 31241027221
- **Lớp học phần:** K50-EE0002
- **Học phần:** Phát triển ứng dụng TMĐT
- **GitHub:** [hoanghung5aa-arch](https://github.com/hoanghung5aa-arch)
- **Repository:** [cai-tien-giao-dien-sach](https://github.com/hoanghung5aa-arch/cai-tien-giao-dien-sach)
- **GitHub Pages:** [CSS-NangCao](https://hoanghung5aa-arch.github.io/cai-tien-giao-dien-sach/CSS-NangCao/)

## Mục tiêu

Lab 07 thực hành cách CSS điều khiển document flow, định vị và xếp lớp phần tử; sử dụng float/clear và multi-column layout; xây dựng giao diện responsive bằng media queries và viewport; tạo transforms, transitions, filters, animations; và áp dụng Bootstrap grid cùng các thành phần giao diện.

## Cấu trúc thư mục

```text
CSS-NangCao/
├── index.html                 # Portfolio cá nhân và điều hướng đến bài tập
├── README.md
├── assets/
│   └── avatar-monogram.svg    # Avatar initials, không dùng ảnh cá nhân
├── css/
│   └── portfolio.css          # Giao diện portfolio responsive
└── Lab07/
    ├── index.html             # Mục lục các bài thực hành
    ├── lab07-index.css
    ├── lab07-exercise01.html / .css
    ├── ...
    ├── lab07-exercise12.html
    ├── lab07-exercise12-desktop.css
    ├── lab07-exercise12-mobile.css
    ├── lab07-exercise12.css
    ├── lab07-exercise14.html / .css
    ├── ...
    ├── lab07-exercise19.html
    ├── bootstrap/              # Thư viện Bootstrap của bộ bài Lab
    └── images/                 # Hình ảnh của các Exercise
```

Exercise 7.13 tiếp tục thực hiện trong các file Exercise 7.12 theo hướng dẫn Lab 07; vì vậy không có file 13 riêng. Các file và thư mục `bootstrap/`, `images/` được giữ để các liên kết tương đối hoạt động.

## Danh sách bài thực hành

| Bài | Nội dung | Trang |
|---|---|---|
| 7.1–7.4 | Document flow, relative/absolute positioning, z-index | [7.1](Lab07/lab07-exercise01.html) · [7.2](Lab07/lab07-exercise02.html) · [7.3](Lab07/lab07-exercise03.html) · [7.4](Lab07/lab07-exercise04.html) |
| 7.5–7.9 | Float, clear và positioning | [7.5](Lab07/lab07-exercise05.html) · [7.6](Lab07/lab07-exercise06.html) · [7.7](Lab07/lab07-exercise07.html) · [7.8](Lab07/lab07-exercise08.html) · [7.9](Lab07/lab07-exercise09.html) |
| 7.10–7.13 | Bố cục nhiều cột, media queries và viewport | [7.10](Lab07/lab07-exercise10.html) · [7.11](Lab07/lab07-exercise11.html) · [7.12](Lab07/lab07-exercise12.html) · [7.13](Lab07/lab07-exercise12.html#exercise-713) |
| 7.14–7.18 | Transforms, transitions, filters và animations | [7.14](Lab07/lab07-exercise14.html) · [7.15](Lab07/lab07-exercise15.html) · [7.16](Lab07/lab07-exercise16.html) · [7.17](Lab07/lab07-exercise17.html) · [7.18](Lab07/lab07-exercise18.html) |
| 7.19 | Bootstrap grid và thành phần giao diện | [Mở Exercise 7.19](Lab07/lab07-exercise19.html) |

## Hướng dẫn truy cập

1. Mở [trang portfolio CSS nâng cao](https://hoanghung5aa-arch.github.io/cai-tien-giao-dien-sach/CSS-NangCao/).
2. Chọn nhóm bài hoặc mở [trang Lab 07](Lab07/) để vào từng Exercise.
3. Có thể mở trực tiếp các file HTML bằng trình duyệt khi chạy từ máy cá nhân; giữ nguyên các thư mục CSS, Bootstrap và ảnh.

## Nguồn bộ bài khởi đầu

Các file bài tập và tài nguyên gốc được lấy từ [Chapter 7 Lab Starter Files](https://github.com/funwebdev-2nd-ed/ch07-lab-start), bộ tài liệu đi kèm *Fundamentals of Web Development, 2nd Edition* của Randy Connolly và Ricardo Hoar. Nội dung hoàn thiện trong từng bài được trình bày trong các file HTML/CSS tương ứng.

## Ảnh minh chứng và báo cáo

Báo cáo Word được đặt trong thư mục này theo tên `31241027221_NguyenHoangHung_CSS_NangCao.docx`. Ảnh chụp màn hình thực tế được lưu trong `evidence/` và có chú thích trong báo cáo.

## Ghi chú thiết kế

Portfolio dùng cách trình bày như sổ tay thực hành: nền trung tính, mực than, một màu xanh kỹ thuật và sơ đồ positioning minh họa trực tiếp nội dung Lab. Hướng rà soát giao diện được tham khảo từ [unslop của Damir Sokolovsky](https://github.com/heydamir/unslop), giấy phép CC BY 4.0.
