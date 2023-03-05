using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixcnstr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("052d7e4c-5bc5-40bd-942b-31a1664f457d"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1aa24e87-d3d8-4219-86ea-aaf6d26d281b"), 0, "a885e265-ac55-462e-8230-ae16d7910fbb", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "AQAAAAIAAYagAAAAEP0OJEbLUAWo6Rs+cAmULP/1iCDtXj3PfV045bQ/brbLRYNgta69VIaRkdvvMq0WkQ==", "0336154196", false, null, false, "vankiemd3v" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("1aa24e87-d3d8-4219-86ea-aaf6d26d281b"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("052d7e4c-5bc5-40bd-942b-31a1664f457d"), 0, "f9527fcf-f79a-44df-8de4-30828a00adf5", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "AQAAAAIAAYagAAAAEKZVSX8r7TpvFYcKVya1DYFrAqAhWDMzAZc7/WthvkZuzQyJrdXJsjpquN1ENNxSbA==", "0336154196", false, null, false, "vankiemd3v" });
        }
    }
}
