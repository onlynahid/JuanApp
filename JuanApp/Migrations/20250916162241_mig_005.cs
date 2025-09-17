using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuanApp.Migrations
{
    /// <inheritdoc />
    public partial class mig_005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddtoCartUrl",
                table: "products");

            migrationBuilder.DropColumn(
                name: "QuickViewUrl",
                table: "products");

            migrationBuilder.DropColumn(
                name: "WishlistUrl",
                table: "products");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailDescription",
                table: "products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "InStock",
                table: "products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_ColorId",
                table: "products",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Colors_ColorId",
                table: "products",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Colors_ColorId",
                table: "products");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_products_ColorId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "DetailDescription",
                table: "products");

            migrationBuilder.DropColumn(
                name: "InStock",
                table: "products");

            migrationBuilder.AddColumn<string>(
                name: "AddtoCartUrl",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuickViewUrl",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WishlistUrl",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
