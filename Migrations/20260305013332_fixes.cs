using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tp1.Migrations
{
    /// <inheritdoc />
    public partial class fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Factures_FactureId",
                table: "Commandes");

            migrationBuilder.DropIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures");

            migrationBuilder.DropIndex(
                name: "IX_Commandes_FactureId",
                table: "Commandes");

            migrationBuilder.RenameColumn(
                name: "StripeChargeId",
                table: "Factures",
                newName: "PaiementId");

            migrationBuilder.RenameColumn(
                name: "StripeChargeId",
                table: "Commandes",
                newName: "PaiementId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures",
                column: "CommandeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures");

            migrationBuilder.RenameColumn(
                name: "PaiementId",
                table: "Factures",
                newName: "StripeChargeId");

            migrationBuilder.RenameColumn(
                name: "PaiementId",
                table: "Commandes",
                newName: "StripeChargeId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_FactureId",
                table: "Commandes",
                column: "FactureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Factures_FactureId",
                table: "Commandes",
                column: "FactureId",
                principalTable: "Factures",
                principalColumn: "Id");
        }
    }
}
