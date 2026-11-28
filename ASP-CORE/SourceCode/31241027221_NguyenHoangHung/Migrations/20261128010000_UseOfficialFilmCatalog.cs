using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _31241027221_NguyenHoangHung.Migrations
{
    public partial class UseOfficialFilmCatalog : Migration
    {
        private sealed record SeedMovie(int Id, string Title, string Summary, DateTime ReleaseDate,
            int GenreId, decimal Price, double Rated, string AgeRating, string PicturePath);

        private static readonly SeedMovie[] PreviousSeed =
        [
            new(1, "Mùa Gió Cuối", "Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.", new DateTime(2027, 2, 14), 1, 129000m, 4.8, "T13", "/images/mua-gio.svg"),
            new(2, "Hành Tinh Thứ Chín", "Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.", new DateTime(2027, 3, 21), 2, 159000m, 4.6, "T13", "/images/hanh-tinh.svg"),
            new(3, "Chuyến Tàu Đêm", "Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.", new DateTime(2027, 4, 18), 3, 139000m, 4.9, "T16", "/images/chuyen-tau.svg"),
            new(4, "Tín Hiệu Biển", "Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.", new DateTime(2027, 5, 9), 3, 149000m, 4.7, "T13", "/images/tin-hieu-bien.svg"),
            new(5, "Hẻm 36", "Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.", new DateTime(2027, 6, 1), 1, 119000m, 4.5, "T13", "/images/hem-36.svg")
        ];

        private static readonly SeedMovie[] OfficialSeed =
        [
            new(1, "Avengers: Doomsday", "Các anh hùng từ nhiều vũ trụ đối mặt với một mối đe dọa có thể thay đổi tất cả.", new DateTime(2026, 12, 18), 2, 159000m, 4.8, "", "/images/posters/avengers-doomsday.jpg"),
            new(2, "Dune: Part Three", "Paul Atreides đối diện hậu quả của quyền lực và một âm mưu đang siết chặt đế chế.", new DateTime(2026, 12, 18), 2, 159000m, 4.6, "", "/images/posters/dune-part-three.jpg"),
            new(3, "Jumanji: Open World", "Thế giới của trò chơi tràn vào đời thực, mở ra một cuộc phiêu lưu mới cho nhóm bạn.", new DateTime(2026, 12, 25), 3, 139000m, 4.9, "", "/images/posters/jumanji-open-world.jpg"),
            new(4, "Gatto", "Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.", new DateTime(2027, 3, 5), 7, 129000m, 4.7, "", "/images/posters/gatto.jpg"),
            new(5, "Ice Age: Boiling Point", "Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.", new DateTime(2027, 2, 5), 7, 119000m, 4.5, "", "/images/posters/ice-age-boiling-point.jpg")
        ];

        private static string SqlString(string value) => "N'" + value.Replace("'", "''") + "'";

        private static string MatchSeed(SeedMovie movie) =>
            $"ISNULL([Title], N'<NULL>') = {SqlString(movie.Title)} AND ISNULL([Summary], N'<NULL>') = {SqlString(movie.Summary)} " +
            $"AND ISNULL([ReleaseDate], CONVERT(datetime2, '0001-01-01', 23)) = CONVERT(datetime2, '{movie.ReleaseDate:yyyy-MM-dd}', 23) " +
            $"AND ISNULL([GenreId], -2147483648) = {movie.GenreId} AND ISNULL([Price], -1) = {movie.Price.ToString(CultureInfo.InvariantCulture)} " +
            $"AND ISNULL([Rated], -1) = {movie.Rated.ToString("R", CultureInfo.InvariantCulture)} " +
            $"AND ISNULL([AgeRating], N'<NULL>') = {SqlString(movie.AgeRating)} AND ISNULL([PicturePath], N'<NULL>') = {SqlString(movie.PicturePath)}";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Fail closed if a row with a seed ID has been edited. Existing live data is already
            // at the official state; a clean install is still at the previous seed state.
            // Custom values are never overwritten by this migration.
            for (var index = 0; index < PreviousSeed.Length; index++)
            {
                var oldRow = PreviousSeed[index];
                var newRow = OfficialSeed[index];
                migrationBuilder.Sql($"""
                    IF EXISTS (
                        SELECT 1 FROM [Movies]
                        WHERE [Id] = {oldRow.Id}
                          AND NOT (({MatchSeed(oldRow)}) OR ({MatchSeed(newRow)}))
                    )
                        THROW 51003, 'A seeded Movies row was customized. No data was changed; inspect the row before applying this migration.', 1;
                    """);
            }

            // Preserve Genre 1 (Tâm lý). Never rename or delete a category that may own user data.
            migrationBuilder.Sql("""
                IF EXISTS (SELECT 1 FROM [Genres] WHERE [Id] = 7 AND [Name] <> N'Hoạt hình')
                    THROW 51001, 'Genre Id 7 is already used by another category. Migration stopped without changing data.', 1;
                IF EXISTS (SELECT 1 FROM [Genres] WHERE [Name] = N'Hoạt hình' AND [Id] <> 7)
                    THROW 51002, 'Hoạt hình already exists under another Id. Migration stopped without changing data.', 1;
                IF NOT EXISTS (SELECT 1 FROM [Genres] WHERE [Id] = 7 AND [Name] = N'Hoạt hình')
                BEGIN TRY
                    SET IDENTITY_INSERT [Genres] ON;
                    INSERT INTO [Genres] ([Id], [Name]) VALUES (7, N'Hoạt hình');
                    SET IDENTITY_INSERT [Genres] OFF;
                END TRY
                BEGIN CATCH
                    SET IDENTITY_INSERT [Genres] OFF;
                    THROW;
                END CATCH;
                """);
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
                values: new object[] { "", 7, "/images/posters/gatto.jpg", 129000m, 4.7, new DateTime(2027, 3, 5), "Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.", "Gatto" });
            migrationBuilder.UpdateData(table: "Movies", keyColumn: "Id", keyValue: 5,
                columns: new[] { "AgeRating", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[] { "", 7, "/images/posters/ice-age-boiling-point.jpg", 119000m, 4.5, new DateTime(2027, 2, 5), "Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.", "Ice Age: Boiling Point" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Intentionally do not restore starter Movies or delete Genre 7. A rollback must not
            // overwrite edits made after deployment or remove a category now used by user data.
        }
    }
}
