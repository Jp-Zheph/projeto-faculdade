using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Serial = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Modelo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    IdentificadorSala = table.Column<string>(nullable: true),
                    Observacao = table.Column<string>(nullable: true),
                    CapacidadeAlunos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agendamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    DataInicial = table.Column<DateTime>(nullable: false),
                    DataFinal = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    SalaId = table.Column<Guid>(nullable: false),
                    UsuarioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agendamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Agendamento_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agendamento_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalaEquipamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SalaId = table.Column<Guid>(nullable: false),
                    EquipamentoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaEquipamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaEquipamento_Equipamento_EquipamentoId",
                        column: x => x.EquipamentoId,
                        principalTable: "Equipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalaEquipamento_Sala_SalaId",
                        column: x => x.SalaId,
                        principalTable: "Sala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_SalaId",
                table: "Agendamento",
                column: "SalaId");

            migrationBuilder.CreateIndex(
                name: "IX_Agendamento_UsuarioId",
                table: "Agendamento",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaEquipamento_EquipamentoId",
                table: "SalaEquipamento",
                column: "EquipamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaEquipamento_SalaId",
                table: "SalaEquipamento",
                column: "SalaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agendamento");

            migrationBuilder.DropTable(
                name: "SalaEquipamento");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Sala");
        }
    }
}
