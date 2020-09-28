using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addSheetAdmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sheet");

            migrationBuilder.CreateTable(
                name: "SheetAdmission",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileId = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    ProductName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UnitaryValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetAdmission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetAdmission_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                }
                );

            migrationBuilder.CreateIndex(
                name: "IX_SheetAdmission_FileId",
                table: "SheetAdmission",
                column: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheetAdmission");

            migrationBuilder.CreateTable(
                name: "Sheet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnoFabricacao = table.Column<int>(nullable: false),
                    AnoModelo = table.Column<int>(nullable: false),
                    BloqueiaEdicao = table.Column<string>(nullable: true),
                    CodigoFabrica = table.Column<string>(nullable: true),
                    CodigoModelo = table.Column<string>(nullable: true),
                    CodigoServico = table.Column<string>(nullable: true),
                    Combustivel = table.Column<string>(nullable: true),
                    DescricaoOS = table.Column<string>(nullable: true),
                    Diag0600 = table.Column<string>(nullable: true),
                    MdeloVeiculo = table.Column<string>(nullable: true),
                    NomePacote = table.Column<string>(nullable: true),
                    Pecas = table.Column<string>(nullable: true),
                    PrecoMO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoPecas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadePeca = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Revisao = table.Column<int>(nullable: false),
                    TempoDuoTec = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TempoREPARO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheet", x => x.Id);
                });
        }
    }
}
