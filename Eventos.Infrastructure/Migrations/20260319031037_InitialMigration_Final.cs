using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration_Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acompañante_Invitados_InvitadoId",
                table: "Acompañante");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acompañante",
                table: "Acompañante");

            migrationBuilder.RenameTable(
                name: "Acompañante",
                newName: "Acompañantes");

            migrationBuilder.RenameIndex(
                name: "IX_Acompañante_InvitadoId",
                table: "Acompañantes",
                newName: "IX_Acompañantes_InvitadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acompañantes",
                table: "Acompañantes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acompañantes_Invitados_InvitadoId",
                table: "Acompañantes",
                column: "InvitadoId",
                principalTable: "Invitados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acompañantes_Invitados_InvitadoId",
                table: "Acompañantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Acompañantes",
                table: "Acompañantes");

            migrationBuilder.RenameTable(
                name: "Acompañantes",
                newName: "Acompañante");

            migrationBuilder.RenameIndex(
                name: "IX_Acompañantes_InvitadoId",
                table: "Acompañante",
                newName: "IX_Acompañante_InvitadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Acompañante",
                table: "Acompañante",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acompañante_Invitados_InvitadoId",
                table: "Acompañante",
                column: "InvitadoId",
                principalTable: "Invitados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
