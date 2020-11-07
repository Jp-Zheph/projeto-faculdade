using Microsoft.EntityFrameworkCore.Migrations;

namespace NewSIGASE.Migrations
{
    public partial class Altera_Colunas_Decimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "Salas",
                type: "DECIMAL(18,3)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "Equipamentos",
                type: "DECIMAL(18,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Largura",
                table: "Equipamentos",
                type: "DECIMAL(18,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Comprimento",
                table: "Equipamentos",
                type: "DECIMAL(18,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Altura",
                table: "Equipamentos",
                type: "DECIMAL(18,3)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Area",
                table: "Salas",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,3)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "Equipamentos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Largura",
                table: "Equipamentos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Comprimento",
                table: "Equipamentos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,3)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Altura",
                table: "Equipamentos",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(18,3)",
                oldNullable: true);
        }
    }
}
