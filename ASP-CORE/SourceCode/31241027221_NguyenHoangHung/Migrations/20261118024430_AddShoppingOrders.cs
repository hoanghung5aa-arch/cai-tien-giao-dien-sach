using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _31241027221_NguyenHoangHung.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_HoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShippingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShippingDetails_HoaDons_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "GenreId", "PicturePath", "Price", "Rated", "ReleaseDate", "Summary", "Title" },
                values: new object[,]
                {
                    { 1, 1, "/images/mua-gio.svg", 129000m, "T13", new DateTime(2027, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một họa sĩ trở về miền biển cũ để tìm lại những bức thư chưa từng gửi.", "Mùa Gió Cuối" },
                    { 2, 2, "/images/hanh-tinh.svg", 159000m, "T13", new DateTime(2027, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Phi hành đoàn nhận được tín hiệu bí ẩn từ rìa ngoài của hệ Mặt Trời.", "Hành Tinh Thứ Chín" },
                    { 3, 3, "/images/chuyen-tau.svg", 139000m, "T16", new DateTime(2027, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hành khách trên chuyến tàu cuối ngày cùng khám phá một bí mật bị chôn giấu.", "Chuyến Tàu Đêm" },
                    { 4, 3, "/images/tin-hieu-bien.svg", 149000m, "T13", new DateTime(2027, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Một người gác hải đăng lần theo tín hiệu phát ra giữa những đêm bão.", "Tín Hiệu Biển" },
                    { 5, 1, "/images/hem-36.svg", 119000m, "T13", new DateTime(2027, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Những người hàng xóm xa lạ tìm cách giữ lại con hẻm thân quen của mình.", "Hẻm 36" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_HoaDonId",
                table: "ChiTietHoaDons",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MovieId",
                table: "ChiTietHoaDons",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingDetails_HoaDonId",
                table: "ShippingDetails",
                column: "HoaDonId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "ShippingDetails");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
