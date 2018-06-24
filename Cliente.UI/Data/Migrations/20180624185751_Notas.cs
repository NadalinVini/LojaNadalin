using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cliente.UI.Data.Migrations
{
    public partial class Notas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotaFiscal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<long>(nullable: false),
                    ClienteId1 = table.Column<string>(nullable: true),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    TipoPagamentoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotaFiscal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_AspNetUsers_ClienteId1",
                        column: x => x.ClienteId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotaFiscal_TipoPagamento_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TipoPagamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemNota",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NotaFiscalId = table.Column<Guid>(nullable: false),
                    PercentualDesconto = table.Column<decimal>(nullable: false),
                    ProdutoId = table.Column<long>(nullable: false),
                    Quantidade = table.Column<decimal>(nullable: false),
                    ValorUnitario = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemNota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemNota_NotaFiscal_NotaFiscalId",
                        column: x => x.NotaFiscalId,
                        principalTable: "NotaFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemNota_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemNota_NotaFiscalId",
                table: "ItemNota",
                column: "NotaFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemNota_ProdutoId",
                table: "ItemNota",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_ClienteId1",
                table: "NotaFiscal",
                column: "ClienteId1");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_TipoPagamentoId",
                table: "NotaFiscal",
                column: "TipoPagamentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemNota");

            migrationBuilder.DropTable(
                name: "NotaFiscal");
        }
    }
}
