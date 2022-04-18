using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EwareTeste.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelosCaminhoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelosCaminhoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Caminhoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    AnoFabricacao = table.Column<int>(type: "int", nullable: false),
                    AnoModelo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caminhoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Caminhoes_ModelosCaminhoes_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "ModelosCaminhoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_ModeloId",
                table: "Caminhoes",
                column: "ModeloId");

            migrationBuilder.InsertData(table: "ModelosCaminhoes", columns: new string[] { "Descricao" }, values: new string[] { "FH" });
            migrationBuilder.InsertData(table: "ModelosCaminhoes", columns: new string[] { "Descricao" }, values: new string[] { "FM" });
            migrationBuilder.InsertData(table: "ModelosCaminhoes", columns: new string[] { "Descricao" }, values: new string[] { "Teste" });
            migrationBuilder.InsertData(table: "ModelosCaminhoes", columns: new string[] { "Descricao" }, values: new string[] { "HV" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Caminhoes");

            migrationBuilder.DropTable(
                name: "ModelosCaminhoes");
        }
    }
}
