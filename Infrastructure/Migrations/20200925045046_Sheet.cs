using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Sheet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sheet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CodigoFabrica = table.Column<string>(nullable: true),
                    CodigoModelo = table.Column<string>(nullable: true),
                    DescricaoOS = table.Column<string>(nullable: true),
                    Revisao = table.Column<int>(nullable: false),
                    Diag0600 = table.Column<string>(nullable: true),
                    NomePacote = table.Column<string>(nullable: true),
                    CodigoServico = table.Column<string>(nullable: true),
                    Combustivel = table.Column<string>(nullable: true),
                    TempoREPARO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoMO = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Pecas = table.Column<string>(nullable: true),
                    QuantidadePeca = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Unidade = table.Column<string>(nullable: true),
                    PrecoPecas = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MdeloVeiculo = table.Column<string>(nullable: true),
                    AnoFabricacao = table.Column<int>(nullable: false),
                    AnoModelo = table.Column<int>(nullable: false),
                    TempoDuoTec = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BloqueiaEdicao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sheet");
        }
    }
}
