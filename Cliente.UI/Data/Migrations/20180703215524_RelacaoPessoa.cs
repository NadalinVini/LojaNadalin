using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Cliente.UI.Data.Migrations
{
    public partial class RelacaoPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_AspNetUsers_ClienteId1",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_ClienteId1",
                table: "NotaFiscal");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "NotaFiscal");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "NotaFiscal",
                newName: "PessoaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_PessoaId",
                table: "NotaFiscal",
                column: "PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_Pessoa_PessoaId",
                table: "NotaFiscal",
                column: "PessoaId",
                principalTable: "Pessoa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotaFiscal_Pessoa_PessoaId",
                table: "NotaFiscal");

            migrationBuilder.DropIndex(
                name: "IX_NotaFiscal_PessoaId",
                table: "NotaFiscal");

            migrationBuilder.RenameColumn(
                name: "PessoaId",
                table: "NotaFiscal",
                newName: "ClienteId");

            migrationBuilder.AddColumn<string>(
                name: "ClienteId1",
                table: "NotaFiscal",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotaFiscal_ClienteId1",
                table: "NotaFiscal",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_NotaFiscal_AspNetUsers_ClienteId1",
                table: "NotaFiscal",
                column: "ClienteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
