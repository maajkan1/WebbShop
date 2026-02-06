using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbShopApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_users_user_id",
                table: "cart");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_cart_cart_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_item_products_product_id",
                table: "cart_item");

            migrationBuilder.DropForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item");

            migrationBuilder.DropForeignKey(
                name: "fk_product_category_category_category_id",
                table: "product_category");

            migrationBuilder.DropForeignKey(
                name: "fk_product_category_products_product_id",
                table: "product_category");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_category",
                table: "product_category");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_item",
                table: "order_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_category",
                table: "category");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_item",
                table: "cart_item");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart",
                table: "cart");

            migrationBuilder.RenameTable(
                name: "product_category",
                newName: "product_categories");

            migrationBuilder.RenameTable(
                name: "order_item",
                newName: "order_items");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "cart_item",
                newName: "cart_items");

            migrationBuilder.RenameTable(
                name: "cart",
                newName: "carts");

            migrationBuilder.RenameIndex(
                name: "ix_product_category_category_id",
                table: "product_categories",
                newName: "ix_product_categories_category_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_item_order_id",
                table: "order_items",
                newName: "ix_order_items_order_id");

            migrationBuilder.RenameIndex(
                name: "ix_category_category_name",
                table: "categories",
                newName: "ix_categories_category_name");

            migrationBuilder.RenameIndex(
                name: "ix_cart_item_product_id",
                table: "cart_items",
                newName: "ix_cart_items_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_cart_item_cart_id",
                table: "cart_items",
                newName: "ix_cart_items_cart_id");

            migrationBuilder.RenameIndex(
                name: "ix_cart_user_id",
                table: "carts",
                newName: "ix_carts_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_categories",
                table: "product_categories",
                columns: new[] { "product_id", "category_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_items",
                table: "order_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_categories",
                table: "categories",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_carts",
                table: "carts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_items_carts_cart_id",
                table: "cart_items",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cart_items_products_product_id",
                table: "cart_items",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_carts_users_user_id",
                table: "carts",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_items_orders_order_id",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_product_categories_categories_category_id",
                table: "product_categories",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_categories_products_product_id",
                table: "product_categories",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_carts_cart_id",
                table: "cart_items");

            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_products_product_id",
                table: "cart_items");

            migrationBuilder.DropForeignKey(
                name: "fk_carts_users_user_id",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "fk_order_items_orders_order_id",
                table: "order_items");

            migrationBuilder.DropForeignKey(
                name: "fk_product_categories_categories_category_id",
                table: "product_categories");

            migrationBuilder.DropForeignKey(
                name: "fk_product_categories_products_product_id",
                table: "product_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_product_categories",
                table: "product_categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_order_items",
                table: "order_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_categories",
                table: "categories");

            migrationBuilder.DropPrimaryKey(
                name: "pk_carts",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items");

            migrationBuilder.RenameTable(
                name: "product_categories",
                newName: "product_category");

            migrationBuilder.RenameTable(
                name: "order_items",
                newName: "order_item");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "category");

            migrationBuilder.RenameTable(
                name: "carts",
                newName: "cart");

            migrationBuilder.RenameTable(
                name: "cart_items",
                newName: "cart_item");

            migrationBuilder.RenameIndex(
                name: "ix_product_categories_category_id",
                table: "product_category",
                newName: "ix_product_category_category_id");

            migrationBuilder.RenameIndex(
                name: "ix_order_items_order_id",
                table: "order_item",
                newName: "ix_order_item_order_id");

            migrationBuilder.RenameIndex(
                name: "ix_categories_category_name",
                table: "category",
                newName: "ix_category_category_name");

            migrationBuilder.RenameIndex(
                name: "ix_carts_user_id",
                table: "cart",
                newName: "ix_cart_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_cart_items_product_id",
                table: "cart_item",
                newName: "ix_cart_item_product_id");

            migrationBuilder.RenameIndex(
                name: "ix_cart_items_cart_id",
                table: "cart_item",
                newName: "ix_cart_item_cart_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_product_category",
                table: "product_category",
                columns: new[] { "product_id", "category_id" });

            migrationBuilder.AddPrimaryKey(
                name: "pk_order_item",
                table: "order_item",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_category",
                table: "category",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart",
                table: "cart",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_item",
                table: "cart_item",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_users_user_id",
                table: "cart",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cart_item_cart_cart_id",
                table: "cart_item",
                column: "cart_id",
                principalTable: "cart",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_cart_item_products_product_id",
                table: "cart_item",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_order_item_orders_order_id",
                table: "order_item",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_product_category_category_category_id",
                table: "product_category",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_product_category_products_product_id",
                table: "product_category",
                column: "product_id",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
