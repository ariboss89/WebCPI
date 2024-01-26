using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPenilaianToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Penilaians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alternatif = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kriteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subkriteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nilai = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penilaians", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penilaians");
        }
    }
}
