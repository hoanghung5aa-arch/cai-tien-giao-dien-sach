using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _31241027221_NguyenHoangHung.Migrations
{
    /// <inheritdoc />
    public partial class AlignAssignmentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "ShippingDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.Sql("""
                EXEC sp_executesql N'UPDATE [ShippingDetails]
                    SET [ReleaseDate] = DATEADD(day, 1, CONVERT(date, GETDATE()))
                    WHERE [ReleaseDate] IS NULL;';
                """);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "ShippingDetails",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AddColumn<string>(
                name: "AgeRating",
                table: "Movies",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            // Retain the old age classifications while migrating the required numeric score.
            // Older rows with a value outside 1..5 receive a neutral 4.0 score.
            migrationBuilder.Sql("""
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
                """);

            migrationBuilder.AlterColumn<double>(
                name: "Rated",
                table: "Movies",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AgeRating", "Rated" },
                values: new object[] { "T13", 4.7999999999999998 });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AgeRating", "Rated" },
                values: new object[] { "T13", 4.5999999999999996 });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "AgeRating", "Rated" },
                values: new object[] { "T16", 4.9000000000000004 });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "AgeRating", "Rated" },
                values: new object[] { "T13", 4.7000000000000002 });

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "AgeRating", "Rated" },
                values: new object[] { "T13", 4.5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Movies",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Rated",
                table: "Movies",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.Sql("""
                EXEC(N'UPDATE [Movies]
                    SET [Rated] = [AgeRating]
                    WHERE NULLIF([AgeRating], N'''') IS NOT NULL;');
                """);

            migrationBuilder.DropColumn(
                name: "AgeRating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "ShippingDetails");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 1,
                column: "Rated",
                value: "T13");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 2,
                column: "Rated",
                value: "T13");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 3,
                column: "Rated",
                value: "T16");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 4,
                column: "Rated",
                value: "T13");

            migrationBuilder.UpdateData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: 5,
                column: "Rated",
                value: "T13");
        }
    }
}
