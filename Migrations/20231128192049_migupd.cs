using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentHiveV2.Migrations
{
    public partial class migupd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(346));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(377));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 4,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 5,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(383));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 6,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(387));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 7,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 20, 20, 49, 164, DateTimeKind.Local).AddTicks(390));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 2,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 16, 8, 58, 932, DateTimeKind.Local).AddTicks(6301));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 3,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 16, 8, 58, 932, DateTimeKind.Local).AddTicks(6332));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 4,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 16, 8, 58, 932, DateTimeKind.Local).AddTicks(6336));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 5,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 16, 8, 58, 932, DateTimeKind.Local).AddTicks(6339));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 6,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 16, 8, 58, 932, DateTimeKind.Local).AddTicks(6342));

            migrationBuilder.UpdateData(
                table: "Listing",
                keyColumn: "ListingId",
                keyValue: 7,
                column: "CreatedDateTime",
                value: new DateTime(2023, 11, 28, 16, 8, 58, 932, DateTimeKind.Local).AddTicks(6345));
        }
    }
}
