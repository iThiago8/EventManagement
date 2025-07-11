using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apis.Migrations
{
    /// <inheritdoc />
    public partial class PersonSymposium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonSymposium",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    SymposiumId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSymposium", x => new { x.PersonId, x.SymposiumId });
                    table.ForeignKey(
                        name: "FK_PersonSymposium_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonSymposium_Symposium_SymposiumId",
                        column: x => x.SymposiumId,
                        principalTable: "Symposium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSymposium_SymposiumId",
                table: "PersonSymposium",
                column: "SymposiumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonSymposium");
        }
    }
}
