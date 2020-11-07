using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class Add_Endereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Logradouro = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Numero = table.Column<string>(type: "VARCHAR(20)", nullable: true),
                    Complemento = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Bairro = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Cep = table.Column<string>(type: "VARCHAR(8)", nullable: true),
                    Cidade = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    UF = table.Column<string>(type: "VARCHAR(2)", nullable: true),
                    Pais = table.Column<string>(type: "VARCHAR(2)", nullable: true),
                    PontoReferencia = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");
        }
    }
}
