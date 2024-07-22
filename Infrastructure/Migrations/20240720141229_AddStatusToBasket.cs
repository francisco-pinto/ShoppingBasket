using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPriceInEuros",
                table: "Baskets",
                newName: "TotalPriceInCents");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseStatus",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseStatus",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "TotalPriceInCents",
                table: "Baskets",
                newName: "TotalPriceInEuros");
        }
    }
}
