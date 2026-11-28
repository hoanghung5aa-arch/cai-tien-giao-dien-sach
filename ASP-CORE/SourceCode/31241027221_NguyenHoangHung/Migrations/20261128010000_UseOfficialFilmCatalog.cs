using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _31241027221_NguyenHoangHung.Migrations
{
    public partial class UseOfficialFilmCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(table: "Genres", keyColumn: "Id", keyValue: 1, column: "Name", value: "Hoạt hình");
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 1,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "", 2, "/images/posters/avengers-doomsday.jpg", 159000m, 4.8, new DateTime(2026, 12, 18), "Các anh hùng từ nhiều vũ trụ đối mặt với một mối đe dọa có thể thay đổi tất cả.", "Avengers: Doomsday" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 2,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "", 2, "/images/posters/dune-part-three.jpg", 159000m, 4.6, new DateTime(2026, 12, 18), "Paul Atreides đối diện hậu quả của quyền lực và một âm mưu đang siết chặt đế chế.", "Dune: Part Three" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 3,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "", 3, "/images/posters/jumanji-open-world.jpg", 139000m, 4.9, new DateTime(2026, 12, 25), "Thế giới của trò chơi tràn vào đời thực, mở ra một cuộc phiêu lưu mới cho nhóm bạn.", "Jumanji: Open World" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 4,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "", 1, "/images/posters/gatto.jpg", 129000m, 4.7, new DateTime(2027, 3, 5), "Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.", "Gatto" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 5,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "", 1, "/images/posters/ice-age-boiling-point.jpg", 119000m, 4.5, new DateTime(2027, 2, 5), "Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.", "Ice Age: Boiling Point" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(table: "Genres", keyColumn: "Id", keyValue: 1, column: "Name", value: "Tâm lý");
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 1,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "T13", 1, "/images/mua-gio.svg", 129000m, 4.8, new DateTime(2027, 2, 14), "Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.", "Mùa Gió Cuối" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 2,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "T13", 2, "/images/hanh-tinh.svg", 159000m, 4.6, new DateTime(2027, 3, 21), "Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.", "Hành Tinh Thứ Chín" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 3,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "T16", 3, "/images/chuyen-tau.svg", 139000m, 4.9, new DateTime(2027, 4, 18), "Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.", "Chuyến Tàu Đêm" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 4,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "T13", 3, "/images/tin-hieu-bien.svg", 149000m, 4.7, new DateTime(2027, 5, 9), "Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.", "Tín Hiệu Biển" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 5,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "T13", 1, "/images/hem-36.svg", 119000m, 4.5, new DateTime(2027, 6, 1), "Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.", "Hẻm 36" });
        }
    }
}
