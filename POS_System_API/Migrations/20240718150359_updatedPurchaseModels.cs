using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS_System_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedPurchaseModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "PurchaseProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BillProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PurchaseProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BillProducts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
