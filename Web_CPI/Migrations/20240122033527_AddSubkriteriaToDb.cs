using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSubkriteriaToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subkriterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kriteria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pilihan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nilai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subkriterias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subkriterias");
        }
    }
}
