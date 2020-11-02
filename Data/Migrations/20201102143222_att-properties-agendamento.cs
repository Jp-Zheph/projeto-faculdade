using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class attpropertiesagendamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AprovadorId",
                table: "Agendamentos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAtualizacaoStatus",
                table: "Agendamentos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Justificativa",
                table: "Agendamentos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AprovadorId",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "DataAtualizacaoStatus",
                table: "Agendamentos");

            migrationBuilder.DropColumn(
                name: "Justificativa",
                table: "Agendamentos");
        }
    }
}
