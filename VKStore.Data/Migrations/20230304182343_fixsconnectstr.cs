using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixsconnectstr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("18f80137-77d1-42c3-929c-aeb9460e867f"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("052d7e4c-5bc5-40bd-942b-31a1664f457d"), 0, "f9527fcf-f79a-44df-8de4-30828a00adf5", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "AQAAAAIAAYagAAAAEKZVSX8r7TpvFYcKVya1DYFrAqAhWDMzAZc7/WthvkZuzQyJrdXJsjpquN1ENNxSbA==", "0336154196", false, null, false, "vankiemd3v" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("052d7e4c-5bc5-40bd-942b-31a1664f457d"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("18f80137-77d1-42c3-929c-aeb9460e867f"), 0, "d2e5649f-3a33-4ebc-b1ce-2675318090f3", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "AQAAAAIAAYagAAAAELZX/r/X5XXHdXs0vBtcwrlPYwr/9NQZf6JOMne4EQamiMxYK6UQQhIDBuOASTuRig==", "0336154196", false, null, false, "vankiemd3v" });
        }
    }
}
