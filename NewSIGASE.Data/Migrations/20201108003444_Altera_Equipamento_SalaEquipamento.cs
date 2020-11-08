using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class Altera_Equipamento_SalaEquipamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Salas_SalaId",
                table: "Equipamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaEquipamentos",
                table: "SalaEquipamentos");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_SalaId",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SalaEquipamentos");

            migrationBuilder.DropColumn(
                name: "SalaId",
                table: "Equipamentos");

            migrationBuilder.RenameIndex(
                name: "IX_SalaEquipamentos_SalaId",
                table: "SalaEquipamentos",
                newName: "UN_SalaEquipamento_SalaId");

            migrationBuilder.RenameIndex(
                name: "IX_SalaEquipamentos_EquipamentoId",
                table: "SalaEquipamentos",
                newName: "UN_SalaEquipamento_EquipamentoId");

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaEquipamentos",
                table: "SalaEquipamentos",
                columns: new[] { "SalaId", "EquipamentoId" });

            migrationBuilder.CreateIndex(
                name: "UN_Documento",
                table: "Usuarios",
                column: "Documento",
                unique: true,
                filter: "[Documento] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UN_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UN_Serial",
                table: "Equipamentos",
                column: "Serial",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UN_Documento",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "UN_Email",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaEquipamentos",
                table: "SalaEquipamentos");

            migrationBuilder.DropIndex(
                name: "UN_Serial",
                table: "Equipamentos");

            migrationBuilder.RenameIndex(
                name: "UN_SalaEquipamento_SalaId",
                table: "SalaEquipamentos",
                newName: "IX_SalaEquipamentos_SalaId");

            migrationBuilder.RenameIndex(
                name: "UN_SalaEquipamento_EquipamentoId",
                table: "SalaEquipamentos",
                newName: "IX_SalaEquipamentos_EquipamentoId");

            migrationBuilder.AlterColumn<string>(
                name: "Documento",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "SalaEquipamentos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SalaId",
                table: "Equipamentos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaEquipamentos",
                table: "SalaEquipamentos",
                column: "Id");

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
    }
}
