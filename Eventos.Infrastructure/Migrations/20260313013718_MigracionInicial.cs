using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Lugar = table.Column<string>(type: "TEXT", nullable: false),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroMesa = table.Column<int>(type: "INTEGER", nullable: false),
                    CantidadSillas = table.Column<int>(type: "INTEGER", nullable: false),
                    EventoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesas_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    Telefono = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPersonas = table.Column<int>(type: "INTEGER", nullable: false),
                    CuposIngresado = table.Column<int>(type: "INTEGER", nullable: false),
                    EventoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MesaId = table.Column<int>(type: "INTEGER", nullable: false),
                    TicketId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IngresosLogId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitados_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invitados_Mesas_MesaId",
                        column: x => x.MesaId,
                        principalTable: "Mesas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Acompañante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: false),
                    InvitadoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acompañante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Acompañante_Invitados_InvitadoId",
                        column: x => x.InvitadoId,
                        principalTable: "Invitados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngresosLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FechaHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CantidadPersonas = table.Column<int>(type: "INTEGER", nullable: false),
                    InvitadoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngresosLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngresosLogs_Invitados_InvitadoId",
                        column: x => x.InvitadoId,
                        principalTable: "Invitados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CodigoUnico = table.Column<string>(type: "TEXT", nullable: false),
                    QrUrl = table.Column<string>(type: "TEXT", nullable: false),
                    EstaEscaneado = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaEscaneado = table.Column<DateTime>(type: "TEXT", nullable: false),
                    InvitadoId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Invitados_InvitadoId",
                        column: x => x.InvitadoId,
                        principalTable: "Invitados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acompañante_InvitadoId",
                table: "Acompañante",
                column: "InvitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_IngresosLogs_InvitadoId",
                table: "IngresosLogs",
                column: "InvitadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitados_EventoId",
                table: "Invitados",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitados_MesaId",
                table: "Invitados",
                column: "MesaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_EventoId",
                table: "Mesas",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_InvitadoId",
                table: "Tickets",
                column: "InvitadoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acompañante");

            migrationBuilder.DropTable(
                name: "IngresosLogs");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Invitados");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
