using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SindautoHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cargos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DescricaoAtribuicoes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "setores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeSetor = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    HorarioFuncionamento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FotoUrl = table.Column<string>(type: "text", nullable: true),
                    HorarioInicio = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    HorarioFim = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    TipoContratacao = table.Column<int>(type: "integer", nullable: false),
                    SetorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CargoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_funcionarios_cargos_CargoId",
                        column: x => x.CargoId,
                        principalTable: "cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_funcionarios_setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Lida = table.Column<bool>(type: "boolean", nullable: false),
                    Mensagem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LinkDestino = table.Column<string>(type: "text", nullable: true),
                    DataCriacao = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    NotificacaoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notificacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notificacoes_funcionarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notificacoes_notificacoes_NotificacaoId",
                        column: x => x.NotificacaoId,
                        principalTable: "notificacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "postagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    DataPublicacao = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    AutorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_postagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postagens_funcionarios_AutorId",
                        column: x => x.AutorId,
                        principalTable: "funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_CargoId",
                table: "funcionarios",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_funcionarios_SetorId",
                table: "funcionarios",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_notificacoes_NotificacaoId",
                table: "notificacoes",
                column: "NotificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_notificacoes_UsuarioId",
                table: "notificacoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_postagens_AutorId",
                table: "postagens",
                column: "AutorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notificacoes");

            migrationBuilder.DropTable(
                name: "postagens");

            migrationBuilder.DropTable(
                name: "funcionarios");

            migrationBuilder.DropTable(
                name: "cargos");

            migrationBuilder.DropTable(
                name: "setores");
        }
    }
}
