using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuanApp.Migrations
{
    /// <inheritdoc />
    public partial class mig_007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Categories_CategoriyId",
                table: "products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoriyId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategoriyId",
                table: "products");

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.AddColumn<int>(
                name: "CategoriyId",
                table: "products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoriyId",
                table: "products",
                column: "CategoriyId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Categories_CategoriyId",
                table: "products",
                column: "CategoriyId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
