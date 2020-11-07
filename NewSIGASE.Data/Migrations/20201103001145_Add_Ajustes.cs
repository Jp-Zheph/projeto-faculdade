using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class Add_Ajustes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Usuarios",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Andar",
                table: "Salas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Area",
                table: "Salas",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Altura",
                table: "Equipamentos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Comprimento",
                table: "Equipamentos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Equipamentos",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Largura",
                table: "Equipamentos",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Peso",
                table: "Equipamentos",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Documento",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Andar",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "Salas");

            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "Comprimento",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "Largura",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Equipamentos");
        }
    }
}
