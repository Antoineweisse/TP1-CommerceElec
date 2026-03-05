using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp1.Migrations
{
    /// <inheritdoc />
    public partial class AjoutFacturesEtLigneCommandes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Utilisateurs_UtilisateurId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Utilisateurs_VendeurId",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "IDStripe",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "VendeurId",
                table: "Factures",
                newName: "CommandeId");

            migrationBuilder.RenameColumn(
                name: "UtilisateurId",
                table: "Factures",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Factures_VendeurId",
                table: "Factures",
                newName: "IX_Factures_CommandeId");

            migrationBuilder.RenameIndex(
                name: "IX_Factures_UtilisateurId",
                table: "Factures",
                newName: "IX_Factures_ClientId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFacture",
                table: "Factures",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "MontantTotal",
                table: "Factures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "StripeChargeId",
                table: "Factures",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Commandes",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<string>(
                name: "StripeChargeId",
                table: "Commandes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LigneCommandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommandeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProduitId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantite = table.Column<int>(type: "INTEGER", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LigneCommandes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LigneCommandes_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LigneCommandes_Produits_ProduitId",
                        column: x => x.ProduitId,
                        principalTable: "Produits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandes_CommandeId",
                table: "LigneCommandes",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_LigneCommandes_ProduitId",
                table: "LigneCommandes",
                column: "ProduitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Commandes_CommandeId",
                table: "Factures",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Utilisateurs_ClientId",
                table: "Factures",
                column: "ClientId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Commandes_CommandeId",
                table: "Factures");

            migrationBuilder.DropForeignKey(
                name: "FK_Factures_Utilisateurs_ClientId",
                table: "Factures");

            migrationBuilder.DropTable(
                name: "LigneCommandes");

            migrationBuilder.DropColumn(
                name: "DateFacture",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "MontantTotal",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "StripeChargeId",
                table: "Factures");

            migrationBuilder.DropColumn(
                name: "StripeChargeId",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "CommandeId",
                table: "Factures",
                newName: "VendeurId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Factures",
                newName: "UtilisateurId");

            migrationBuilder.RenameIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures",
                newName: "IX_Factures_VendeurId");

            migrationBuilder.RenameIndex(
                name: "IX_Factures_ClientId",
                table: "Factures",
                newName: "IX_Factures_UtilisateurId");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Commandes",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "IDStripe",
                table: "Commandes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Utilisateurs_UtilisateurId",
                table: "Factures",
                column: "UtilisateurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Factures_Utilisateurs_VendeurId",
                table: "Factures",
                column: "VendeurId",
                principalTable: "Utilisateurs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
