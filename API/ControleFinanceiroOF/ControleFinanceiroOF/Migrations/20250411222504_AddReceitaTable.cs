using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiroOF.Migrations
{
    /// <inheritdoc />
    public partial class AddReceitaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                schema: "Sistema",
                table: "Despesas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Despesas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Receitas",
                schema: "Sistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Valor = table.Column<decimal>(type: "money", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Extra = table.Column<bool>(type: "bit", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    UsuarioModelIdUsuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receitas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalSchema: "Seguranca",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receitas_Usuarios_UsuarioModelIdUsuario",
                        column: x => x.UsuarioModelIdUsuario,
                        principalSchema: "Seguranca",
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioId",
                schema: "Sistema",
                table: "Despesas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Despesas_UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Despesas",
                column: "UsuarioModelIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuarioId",
                schema: "Sistema",
                table: "Receitas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Receitas",
                column: "UsuarioModelIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_UsuarioId",
                schema: "Sistema",
                table: "Despesas",
                column: "UsuarioId",
                principalSchema: "Seguranca",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Despesas_Usuarios_UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Despesas",
                column: "UsuarioModelIdUsuario",
                principalSchema: "Seguranca",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuarioId",
                schema: "Sistema",
                table: "Despesas");

            migrationBuilder.DropForeignKey(
                name: "FK_Despesas_Usuarios_UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Despesas");

            migrationBuilder.DropTable(
                name: "Receitas",
                schema: "Sistema");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuarioId",
                schema: "Sistema",
                table: "Despesas");

            migrationBuilder.DropIndex(
                name: "IX_Despesas_UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                schema: "Sistema",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "UsuarioModelIdUsuario",
                schema: "Sistema",
                table: "Despesas");
        }
    }
}
