using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixcnstr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("1aa24e87-d3d8-4219-86ea-aaf6d26d281b"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("dfbf9a13-2805-4565-86c4-7ddf907db396"), 0, "ea0bdf61-5354-41de-b12b-605b357649cc", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "AQAAAAIAAYagAAAAEOTQF4VEGcXThb79jzVd9gROm6rm0ipOQ09SDVX4VRs+OPIm/QU4bpTHfNCG6SR6Rw==", "0336154196", false, null, false, "vankiemd3v" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("dfbf9a13-2805-4565-86c4-7ddf907db396"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("1aa24e87-d3d8-4219-86ea-aaf6d26d281b"), 0, "a885e265-ac55-462e-8230-ae16d7910fbb", "vankiemd3v@gmail.com", false, "Văn Kiếm", false, null, null, null, "AQAAAAIAAYagAAAAEP0OJEbLUAWo6Rs+cAmULP/1iCDtXj3PfV045bQ/brbLRYNgta69VIaRkdvvMq0WkQ==", "0336154196", false, null, false, "vankiemd3v" });
        }
    }
}
