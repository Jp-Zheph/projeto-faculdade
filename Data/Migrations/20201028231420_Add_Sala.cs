using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class Add_Sala : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Tipo = table.Column<int>(nullable: false),
                    IdentificadorSala = table.Column<string>(nullable: false),
                    Observacao = table.Column<string>(nullable: false),
                    CapacidadeAlunos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_SalaId",
                table: "Equipamentos",
                column: "SalaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Salas_SalaId",
                table: "Equipamentos",
                column: "SalaId",
                principalTable: "Salas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Salas_SalaId",
                table: "Equipamentos");

            migrationBuilder.DropTable(
                name: "Salas");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_SalaId",
                table: "Equipamentos");
        }
    }
}
