using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car2Go.Migrations
{
    /// <inheritdoc />
    public partial class ChangeReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "hasReview",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "AvailableDate",
                value: new DateOnly(2024, 12, 15));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2,
                column: "AvailableDate",
                value: new DateOnly(2024, 12, 16));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 3,
                column: "AvailableDate",
                value: new DateOnly(2024, 12, 17));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "hasReview",
                table: "Reviews");

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 1,
                column: "AvailableDate",
                value: new DateOnly(2024, 12, 11));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 2,
                column: "AvailableDate",
                value: new DateOnly(2024, 12, 12));

            migrationBuilder.UpdateData(
                table: "Cars",
                keyColumn: "CarId",
                keyValue: 3,
                column: "AvailableDate",
                value: new DateOnly(2024, 12, 13));
        }
    }
}
