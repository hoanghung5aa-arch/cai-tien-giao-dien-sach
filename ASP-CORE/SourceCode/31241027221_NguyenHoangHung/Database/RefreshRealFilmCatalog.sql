-- Official film catalog refresh, researched 2026-11-28.
-- Run after the final application deployment; keep a database backup.
-- Only the ten identified lab records are updated. Existing order item titles/prices remain historical.
-- Ratings and prices are lab sample values, not public review scores or ticket offers.
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF NOT EXISTS (SELECT 1 FROM [Genres] WHERE [Name] = N'Hoạt hình') INSERT INTO [Genres] ([Name]) VALUES (N'Hoạt hình');
IF NOT EXISTS (SELECT 1 FROM [Genres] WHERE [Name] = N'Khoa học viễn tưởng') INSERT INTO [Genres] ([Name]) VALUES (N'Khoa học viễn tưởng');
IF NOT EXISTS (SELECT 1 FROM [Genres] WHERE [Name] = N'Phiêu lưu') INSERT INTO [Genres] ([Name]) VALUES (N'Phiêu lưu');
IF NOT EXISTS (SELECT 1 FROM [Genres] WHERE [Name] = N'Hành động') INSERT INTO [Genres] ([Name]) VALUES (N'Hành động');

UPDATE [Movies] SET [Title] = N'Avengers: Doomsday', [Summary] = N'Các anh hùng từ nhiều vũ trụ đối mặt với một mối đe dọa có thể thay đổi tất cả.', [ReleaseDate] = '2026-12-18', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Khoa học viễn tưởng'), [Price] = 159000, [Rated] = 4.8, [AgeRating] = N'', [PicturePath] = N'/images/posters/avengers-doomsday.jpg'
WHERE [Id] = 1 AND [Title] IN (N'Mùa Gió Cuối', N'Avengers: Doomsday');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 1 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Dune: Part Three', [Summary] = N'Paul Atreides đối diện hậu quả của quyền lực và một âm mưu đang siết chặt đế chế.', [ReleaseDate] = '2026-12-18', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Khoa học viễn tưởng'), [Price] = 159000, [Rated] = 4.6, [AgeRating] = N'', [PicturePath] = N'/images/posters/dune-part-three.jpg'
WHERE [Id] = 2 AND [Title] IN (N'Hành Tinh Thứ Chín', N'Dune: Part Three');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 2 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Jumanji: Open World', [Summary] = N'Thế giới của trò chơi tràn vào đời thực, mở ra một cuộc phiêu lưu mới cho nhóm bạn.', [ReleaseDate] = '2026-12-25', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Phiêu lưu'), [Price] = 139000, [Rated] = 4.9, [AgeRating] = N'', [PicturePath] = N'/images/posters/jumanji-open-world.jpg'
WHERE [Id] = 3 AND [Title] IN (N'Chuyến Tàu Đêm', N'Jumanji: Open World');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 3 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Gatto', [Summary] = N'Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.', [ReleaseDate] = '2027-03-05', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hoạt hình'), [Price] = 129000, [Rated] = 4.7, [AgeRating] = N'', [PicturePath] = N'/images/posters/gatto.jpg'
WHERE [Id] = 4 AND [Title] IN (N'Tín Hiệu Biển', N'Gatto');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 4 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Ice Age: Boiling Point', [Summary] = N'Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.', [ReleaseDate] = '2027-02-05', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hoạt hình'), [Price] = 119000, [Rated] = 4.5, [AgeRating] = N'', [PicturePath] = N'/images/posters/ice-age-boiling-point.jpg'
WHERE [Id] = 5 AND [Title] IN (N'Hẻm 36', N'Ice Age: Boiling Point');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 5 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'The Bluey Movie', [Summary] = N'Một bất ngờ lớn biến ngày bình thường của Bluey và gia đình thành chuyến phiêu lưu đáng nhớ.', [ReleaseDate] = '2027-08-06', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hoạt hình'), [Price] = 119000, [Rated] = 4.5, [AgeRating] = N'', [PicturePath] = N'/images/posters/bluey-movie.jpg'
WHERE [Id] = 16 AND [Title] IN (N'Ký ức chiều mưa', N'The Bluey Movie');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 16 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Frozen 3', [Summary] = N'Anna, Elsa, Kristoff và Olaf trở lại trong chương tiếp theo của loạt phim Frozen.', [ReleaseDate] = '2027-11-24', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hoạt hình'), [Price] = 129000, [Rated] = 4.6, [AgeRating] = N'', [PicturePath] = N'/images/posters/frozen-3.jpg'
WHERE [Id] = 17 AND [Title] IN (N'Dải ngân hà tĩnh lặng', N'Frozen 3');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 17 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Shrek 5', [Summary] = N'Shrek, Fiona và Donkey trở lại cùng thế hệ mới trong gia đình yêu tinh.', [ReleaseDate] = '2027-06-30', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hoạt hình'), [Price] = 129000, [Rated] = 4.7, [AgeRating] = N'', [PicturePath] = N'/images/posters/shrek-5.webp'
WHERE [Id] = 18 AND [Title] IN (N'Đêm cuối ở bến cảng', N'Shrek 5');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 18 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Incredibles 3', [Summary] = N'Gia đình siêu anh hùng Parr trở lại trong phần phim thứ ba của Pixar.', [ReleaseDate] = '2028-06-16', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hoạt hình'), [Price] = 139000, [Rated] = 4.6, [AgeRating] = N'', [PicturePath] = N'/images/posters/incredibles-3.jpg'
WHERE [Id] = 19 AND [Title] IN (N'Vũ điệu của những vì sao', N'Incredibles 3');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 19 was missing or renamed. Transaction canceled for review.', 1;

UPDATE [Movies] SET [Title] = N'Violent Night 2', [Summary] = N'Ông già Noel do David Harbour thủ vai trở lại trong phần tiếp theo của phim hành động mùa lễ hội.', [ReleaseDate] = '2026-12-04', [GenreId] = (SELECT [Id] FROM [Genres] WHERE [Name] = N'Hành động'), [Price] = 149000, [Rated] = 4.5, [AgeRating] = N'', [PicturePath] = N'/images/posters/violent-night-2.webp'
WHERE [Id] = 20 AND [Title] IN (N'Hành trình qua thung lũng', N'Violent Night 2');
IF @@ROWCOUNT <> 1 THROW 51000, 'Catalog record 20 was missing or renamed. Transaction canceled for review.', 1;

COMMIT;
GO
