using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp1.Migrations
{
    /// <inheritdoc />
    public partial class PanierItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Paniers_PanierId",
                table: "Produits");

            migrationBuilder.DropIndex(
                name: "IX_Produits_PanierId",
                table: "Produits");

            migrationBuilder.DropColumn(
                name: "PanierId",
                table: "Produits");

            migrationBuilder.CreateTable(
                name: "PanierItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: false),
                    PanierId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PanierItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PanierItems_Paniers_PanierId",
                        column: x => x.PanierId,
                        principalTable: "Paniers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PanierItems_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PanierItems_PanierId",
                table: "PanierItems",
                column: "PanierId");

            migrationBuilder.CreateIndex(
                name: "IX_PanierItems_ProduitId",
                table: "PanierItems",
                column: "ProduitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PanierItems");

            migrationBuilder.AddColumn<int>(
                name: "PanierId",
                table: "Produits",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produits_PanierId",
                table: "Produits",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Paniers_PanierId",
                table: "Produits",
                column: "PanierId",
                principalTable: "Paniers",
                principalColumn: "Id");
        }
    }
}
