using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VKStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixClOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalPayment",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPayment",
                table: "Orders");
        }
    }
}
