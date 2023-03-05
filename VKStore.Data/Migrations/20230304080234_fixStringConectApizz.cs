using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixStringConectApizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("2f0657bc-2544-4260-bdbe-170b73888225"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1e18b8c7-3d13-49d8-9893-d0f4d2252efe"), 0, "f861f83a-ae5b-4b3c-9965-b026b824fa9c", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "Admin123@", "0336154196", false, null, false, "vankiemd3v" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("1e18b8c7-3d13-49d8-9893-d0f4d2252efe"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("2f0657bc-2544-4260-bdbe-170b73888225"), 0, "57591f76-2be9-41ce-9ef1-16c14137598c", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "Admin123@", "0336154196", false, null, false, "vankiemd3v" });
        }
    }
}
