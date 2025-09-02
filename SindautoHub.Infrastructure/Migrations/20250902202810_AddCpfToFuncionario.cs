using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SindautoHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCpfToFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "funcionarios",
                type: "character varying(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_Cpf",
                table: "funcionarios",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_funcionarios_Cpf",
                table: "funcionarios");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "funcionarios");
        }
    }
}
