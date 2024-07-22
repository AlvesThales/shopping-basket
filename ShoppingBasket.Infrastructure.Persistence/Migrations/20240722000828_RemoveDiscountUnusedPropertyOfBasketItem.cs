using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingBasket.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDiscountUnusedPropertyOfBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "BasketItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "BasketItem",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
