BEGIN TRANSACTION;
CREATE TABLE [HoaDons] (
    [Id] int NOT NULL IDENTITY,
    [CreatedAt] datetime2 NOT NULL,
    [Total] decimal(18,2) NOT NULL,
    [UserId] nvarchar(450) NULL,
    CONSTRAINT [PK_HoaDons] PRIMARY KEY ([Id])
);

CREATE TABLE [ChiTietHoaDons] (
    [Id] int NOT NULL IDENTITY,
    [HoaDonId] int NOT NULL,
    [MovieId] int NOT NULL,
    [Quantity] int NOT NULL,
    [UnitPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_ChiTietHoaDons] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ChiTietHoaDons_HoaDons_HoaDonId] FOREIGN KEY ([HoaDonId]) REFERENCES [HoaDons] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ChiTietHoaDons_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE NO ACTION
);

CREATE TABLE [ShippingDetails] (
    [Id] int NOT NULL IDENTITY,
    [HoaDonId] int NOT NULL,
    [FullName] nvarchar(120) NOT NULL,
    [Address] nvarchar(250) NOT NULL,
    [Email] nvarchar(254) NOT NULL,
    [Mobile] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_ShippingDetails] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ShippingDetails_HoaDons_HoaDonId] FOREIGN KEY ([HoaDonId]) REFERENCES [HoaDons] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GenreId', N'PicturePath', N'Price', N'Rated', N'ReleaseDate', N'Summary', N'Title') AND [object_id] = OBJECT_ID(N'[Movies]'))
    SET IDENTITY_INSERT [Movies] ON;
INSERT INTO [Movies] ([Id], [GenreId], [PicturePath], [Price], [Rated], [ReleaseDate], [Summary], [Title])
VALUES (1, 1, N'/images/mua-gio.svg', 129000.0, N'T13', '2027-02-14T00:00:00.0000000', N'Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.', N'Mùa Gió Cuối'),
(2, 2, N'/images/hanh-tinh.svg', 159000.0, N'T13', '2027-03-21T00:00:00.0000000', N'Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.', N'Hành Tinh Thứ Chín'),
(3, 3, N'/images/chuyen-tau.svg', 139000.0, N'T16', '2027-04-18T00:00:00.0000000', N'Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.', N'Chuyến Tàu Đêm'),
(4, 3, N'/images/tin-hieu-bien.svg', 149000.0, N'T13', '2027-05-09T00:00:00.0000000', N'Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.', N'Tín Hiệu Biển'),
(5, 1, N'/images/hem-36.svg', 119000.0, N'T13', '2027-06-01T00:00:00.0000000', N'Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.', N'Hẻm 36');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GenreId', N'PicturePath', N'Price', N'Rated', N'ReleaseDate', N'Summary', N'Title') AND [object_id] = OBJECT_ID(N'[Movies]'))
    SET IDENTITY_INSERT [Movies] OFF;

CREATE INDEX [IX_ChiTietHoaDons_HoaDonId] ON [ChiTietHoaDons] ([HoaDonId]);

CREATE INDEX [IX_ChiTietHoaDons_MovieId] ON [ChiTietHoaDons] ([MovieId]);

CREATE UNIQUE INDEX [IX_ShippingDetails_HoaDonId] ON [ShippingDetails] ([HoaDonId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20261118024430_AddShoppingOrders', N'10.0.15');

COMMIT;
GO

