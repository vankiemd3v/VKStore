using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class adduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("6aa1dc3d-6ac8-47c6-b619-fe7362ee27c6"), 0, "72ce5143-adfd-4d7c-aaca-129985a44cc6", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "Admin123@", "0336154196", false, null, false, "vankiemd3v" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("6aa1dc3d-6ac8-47c6-b619-fe7362ee27c6"));
        }
    }
}
