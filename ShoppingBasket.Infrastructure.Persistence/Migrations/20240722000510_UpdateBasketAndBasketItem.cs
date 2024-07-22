using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingBasket.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBasketAndBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "BasketItem",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "BasketItem");
        }
    }
}
