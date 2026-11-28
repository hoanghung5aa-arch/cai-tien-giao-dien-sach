IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118022452_InitialIdentity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261118022452_InitialIdentity', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118023220_AddMoviesAndGenres'
)
BEGIN
    CREATE TABLE [Genres] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(80) NOT NULL,
        CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118023220_AddMoviesAndGenres'
)
BEGIN
    CREATE TABLE [Movies] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(150) NOT NULL,
        [Summary] nvarchar(1200) NOT NULL,
        [ReleaseDate] datetime2 NOT NULL,
        [GenreId] int NOT NULL,
        [Price] decimal(18,2) NOT NULL,
        [Rated] nvarchar(10) NOT NULL,
        [PicturePath] nvarchar(260) NULL,
        CONSTRAINT [PK_Movies] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Movies_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE NO ACTION
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118023220_AddMoviesAndGenres'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
        SET IDENTITY_INSERT [Genres] ON;
    EXEC(N'INSERT INTO [Genres] ([Id], [Name])
    VALUES (1, N''Tâm lý''),
    (2, N''Khoa học viễn tưởng''),
    (3, N''Phiêu lưu'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Genres]'))
        SET IDENTITY_INSERT [Genres] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118023220_AddMoviesAndGenres'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Genres_Name] ON [Genres] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118023220_AddMoviesAndGenres'
)
BEGIN
    CREATE INDEX [IX_Movies_GenreId] ON [Movies] ([GenreId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118023220_AddMoviesAndGenres'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261118023220_AddMoviesAndGenres', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
    CREATE TABLE [HoaDons] (
        [Id] int NOT NULL IDENTITY,
        [CreatedAt] datetime2 NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_HoaDons] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
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
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
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
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GenreId', N'PicturePath', N'Price', N'Rated', N'ReleaseDate', N'Summary', N'Title') AND [object_id] = OBJECT_ID(N'[Movies]'))
        SET IDENTITY_INSERT [Movies] ON;
    EXEC(N'INSERT INTO [Movies] ([Id], [GenreId], [PicturePath], [Price], [Rated], [ReleaseDate], [Summary], [Title])
    VALUES (1, 1, N''/images/mua-gio.svg'', 129000.0, N''T13'', ''2027-02-14T00:00:00.0000000'', N''Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.'', N''Mùa Gió Cuối''),
    (2, 2, N''/images/hanh-tinh.svg'', 159000.0, N''T13'', ''2027-03-21T00:00:00.0000000'', N''Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.'', N''Hành Tinh Thứ Chín''),
    (3, 3, N''/images/chuyen-tau.svg'', 139000.0, N''T16'', ''2027-04-18T00:00:00.0000000'', N''Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.'', N''Chuyến Tàu Đêm''),
    (4, 3, N''/images/tin-hieu-bien.svg'', 149000.0, N''T13'', ''2027-05-09T00:00:00.0000000'', N''Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.'', N''Tín Hiệu Biển''),
    (5, 1, N''/images/hem-36.svg'', 119000.0, N''T13'', ''2027-06-01T00:00:00.0000000'', N''Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.'', N''Hẻm 36'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'GenreId', N'PicturePath', N'Price', N'Rated', N'ReleaseDate', N'Summary', N'Title') AND [object_id] = OBJECT_ID(N'[Movies]'))
        SET IDENTITY_INSERT [Movies] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
    CREATE INDEX [IX_ChiTietHoaDons_HoaDonId] ON [ChiTietHoaDons] ([HoaDonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
    CREATE INDEX [IX_ChiTietHoaDons_MovieId] ON [ChiTietHoaDons] ([MovieId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
    CREATE UNIQUE INDEX [IX_ShippingDetails_HoaDonId] ON [ShippingDetails] ([HoaDonId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261118024430_AddShoppingOrders'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261118024430_AddShoppingOrders', N'10.0.15');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    ALTER TABLE [ShippingDetails] ADD [ReleaseDate] datetime2 NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC sp_executesql N'UPDATE [ShippingDetails]
        SET [ReleaseDate] = DATEADD(day, 1, CONVERT(date, GETDATE()))
        WHERE [ReleaseDate] IS NULL;';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShippingDetails]') AND [c].[name] = N'ReleaseDate');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [ShippingDetails] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [ShippingDetails] ALTER COLUMN [ReleaseDate] datetime2 NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    DECLARE @var1 nvarchar(max);
    SELECT @var1 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT ' + @var1 + ';');
    ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(200) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    ALTER TABLE [Movies] ADD [AgeRating] nvarchar(10) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC(N'UPDATE [Movies]
        SET [AgeRating] = CASE
                WHEN TRY_CONVERT(float, [Rated]) IS NULL THEN [Rated]
                ELSE NULL
            END,
            [Rated] = CASE
                WHEN TRY_CONVERT(float, [Rated]) BETWEEN 1 AND 5
                    THEN CONVERT(nvarchar(10), TRY_CONVERT(float, [Rated]))
                ELSE N''4.0''
            END;');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    DECLARE @var2 nvarchar(max);
    SELECT @var2 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Rated');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT ' + @var2 + ';');
    ALTER TABLE [Movies] ALTER COLUMN [Rated] float NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    DROP INDEX [IX_Genres_Name] ON [Genres];
    DECLARE @var3 nvarchar(max);
    SELECT @var3 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Genres]') AND [c].[name] = N'Name');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Genres] DROP CONSTRAINT ' + @var3 + ';');
    ALTER TABLE [Genres] ALTER COLUMN [Name] nvarchar(200) NOT NULL;
    CREATE UNIQUE INDEX [IX_Genres_Name] ON [Genres] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC(N'UPDATE [Movies] SET [AgeRating] = N''T13'', [Rated] = 4.7999999999999998E0
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC(N'UPDATE [Movies] SET [AgeRating] = N''T13'', [Rated] = 4.5999999999999996E0
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC(N'UPDATE [Movies] SET [AgeRating] = N''T16'', [Rated] = 4.9000000000000004E0
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC(N'UPDATE [Movies] SET [AgeRating] = N''T13'', [Rated] = 4.7000000000000002E0
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    EXEC(N'UPDATE [Movies] SET [AgeRating] = N''T13'', [Rated] = 4.5E0
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261123152543_AlignAssignmentModel'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261123152543_AlignAssignmentModel', N'10.0.15');
END;

COMMIT;
GO


-- Official film catalog data migration (2026-11-28)
IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20261128010000_UseOfficialFilmCatalog')
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM [Genres] WHERE [Id] = 7 AND [Name] <> N'Hoạt hình')
            THROW 51001, 'Genre Id 7 is already used by another category. Install stopped without changing data.', 1;
        IF EXISTS (SELECT 1 FROM [Genres] WHERE [Name] = N'Hoạt hình' AND [Id] <> 7)
            THROW 51002, 'Hoạt hình already exists under another Id. Install stopped without changing data.', 1;
        IF EXISTS (
            SELECT 1 FROM [Movies]
            WHERE [Id] BETWEEN 1 AND 5
              AND ([Title] IS NULL OR [Summary] IS NULL OR [ReleaseDate] IS NULL OR [GenreId] IS NULL
                   OR [Price] IS NULL OR [Rated] IS NULL OR [AgeRating] IS NULL OR [PicturePath] IS NULL)
        )
            THROW 51004, 'A seeded Movies row contains NULL data. Install stopped without changing data.', 1;
        IF EXISTS (
            SELECT 1 FROM [Movies]
            WHERE [Id] BETWEEN 1 AND 5
              AND NOT (
                ([Id] = 1 AND (([Title] = N'Mùa Gió Cuối' AND [Summary] = N'Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.' AND [ReleaseDate] = CONVERT(datetime2, '2027-02-14', 23) AND [GenreId] = 1 AND [Price] = 129000 AND [Rated] = 4.8 AND [AgeRating] = N'T13' AND [PicturePath] = N'/images/mua-gio.svg') OR ([Title] = N'Avengers: Doomsday' AND [Summary] = N'Các anh hùng từ nhiều vũ trụ đối mặt với một mối đe dọa có thể thay đổi tất cả.' AND [ReleaseDate] = CONVERT(datetime2, '2026-12-18', 23) AND [GenreId] = 2 AND [Price] = 159000 AND [Rated] = 4.8 AND [AgeRating] = N'' AND [PicturePath] = N'/images/posters/avengers-doomsday.jpg'))
                OR ([Id] = 2 AND (([Title] = N'Hành Tinh Thứ Chín' AND [Summary] = N'Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.' AND [ReleaseDate] = CONVERT(datetime2, '2027-03-21', 23) AND [GenreId] = 2 AND [Price] = 159000 AND [Rated] = 4.6 AND [AgeRating] = N'T13' AND [PicturePath] = N'/images/hanh-tinh.svg') OR ([Title] = N'Dune: Part Three' AND [Summary] = N'Paul Atreides đối diện hậu quả của quyền lực và một âm mưu đang siết chặt đế chế.' AND [ReleaseDate] = CONVERT(datetime2, '2026-12-18', 23) AND [GenreId] = 2 AND [Price] = 159000 AND [Rated] = 4.6 AND [AgeRating] = N'' AND [PicturePath] = N'/images/posters/dune-part-three.jpg'))
                OR ([Id] = 3 AND (([Title] = N'Chuyến Tàu Đêm' AND [Summary] = N'Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.' AND [ReleaseDate] = CONVERT(datetime2, '2027-04-18', 23) AND [GenreId] = 3 AND [Price] = 139000 AND [Rated] = 4.9 AND [AgeRating] = N'T16' AND [PicturePath] = N'/images/chuyen-tau.svg') OR ([Title] = N'Jumanji: Open World' AND [Summary] = N'Thế giới của trò chơi tràn vào đời thực, mở ra một cuộc phiêu lưu mới cho nhóm bạn.' AND [ReleaseDate] = CONVERT(datetime2, '2026-12-25', 23) AND [GenreId] = 3 AND [Price] = 139000 AND [Rated] = 4.9 AND [AgeRating] = N'' AND [PicturePath] = N'/images/posters/jumanji-open-world.jpg'))
                OR ([Id] = 4 AND (([Title] = N'Tín Hiệu Biển' AND [Summary] = N'Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.' AND [ReleaseDate] = CONVERT(datetime2, '2027-05-09', 23) AND [GenreId] = 3 AND [Price] = 149000 AND [Rated] = 4.7 AND [AgeRating] = N'T13' AND [PicturePath] = N'/images/tin-hieu-bien.svg') OR ([Title] = N'Gatto' AND [Summary] = N'Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.' AND [ReleaseDate] = CONVERT(datetime2, '2027-03-05', 23) AND [GenreId] = 7 AND [Price] = 129000 AND [Rated] = 4.7 AND [AgeRating] = N'' AND [PicturePath] = N'/images/posters/gatto.jpg'))
                OR ([Id] = 5 AND (([Title] = N'Hẻm 36' AND [Summary] = N'Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.' AND [ReleaseDate] = CONVERT(datetime2, '2027-06-01', 23) AND [GenreId] = 1 AND [Price] = 119000 AND [Rated] = 4.5 AND [AgeRating] = N'T13' AND [PicturePath] = N'/images/hem-36.svg') OR ([Title] = N'Ice Age: Boiling Point' AND [Summary] = N'Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.' AND [ReleaseDate] = CONVERT(datetime2, '2027-02-05', 23) AND [GenreId] = 7 AND [Price] = 119000 AND [Rated] = 4.5 AND [AgeRating] = N'' AND [PicturePath] = N'/images/posters/ice-age-boiling-point.jpg')))
              )
        )
            THROW 51003, 'A seeded Movies row was customized. Install stopped without changing data; inspect the row before continuing.', 1;
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

    UPDATE [Movies] SET [Title] = N'Avengers: Doomsday', [Summary] = N'Các anh hùng từ nhiều vũ trụ đối mặt với một mối đe dọa có thể thay đổi tất cả.', [ReleaseDate] = '2026-12-18', [GenreId] = 2, [Price] = 159000, [Rated] = 4.8, [AgeRating] = N'', [PicturePath] = N'/images/posters/avengers-doomsday.jpg' WHERE [Id] = 1;
    UPDATE [Movies] SET [Title] = N'Dune: Part Three', [Summary] = N'Paul Atreides đối diện hậu quả của quyền lực và một âm mưu đang siết chặt đế chế.', [ReleaseDate] = '2026-12-18', [GenreId] = 2, [Price] = 159000, [Rated] = 4.6, [AgeRating] = N'', [PicturePath] = N'/images/posters/dune-part-three.jpg' WHERE [Id] = 2;
    UPDATE [Movies] SET [Title] = N'Jumanji: Open World', [Summary] = N'Thế giới của trò chơi tràn vào đời thực, mở ra một cuộc phiêu lưu mới cho nhóm bạn.', [ReleaseDate] = '2026-12-25', [GenreId] = 3, [Price] = 139000, [Rated] = 4.9, [AgeRating] = N'', [PicturePath] = N'/images/posters/jumanji-open-world.jpg' WHERE [Id] = 3;
    UPDATE [Movies] SET [Title] = N'Gatto', [Summary] = N'Chú mèo Nero tìm kiếm tình bạn và lối đi riêng giữa những con kênh của Venice.', [ReleaseDate] = '2027-03-05', [GenreId] = 7, [Price] = 129000, [Rated] = 4.7, [AgeRating] = N'', [PicturePath] = N'/images/posters/gatto.jpg' WHERE [Id] = 4;
    UPDATE [Movies] SET [Title] = N'Ice Age: Boiling Point', [Summary] = N'Manny, Sid và Diego bước vào cuộc phiêu lưu mới giữa khủng long và dung nham của Lost World.', [ReleaseDate] = '2027-02-05', [GenreId] = 7, [Price] = 119000, [Rated] = 4.5, [AgeRating] = N'', [PicturePath] = N'/images/posters/ice-age-boiling-point.jpg' WHERE [Id] = 5;
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20261128010000_UseOfficialFilmCatalog', N'10.0.15');
        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH;
END;
GO
