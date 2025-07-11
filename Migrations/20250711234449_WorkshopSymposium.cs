using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apis.Migrations
{
    /// <inheritdoc />
    public partial class WorkshopSymposium : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymposiumWorkshopEnrollment",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    SymposiumId = table.Column<int>(type: "int", nullable: false),
                    IsLecturer = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymposiumWorkshopEnrollment", x => new { x.SymposiumId, x.PersonId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_SymposiumWorkshopEnrollment_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SymposiumWorkshopEnrollment_Symposium_SymposiumId",
                        column: x => x.SymposiumId,
                        principalTable: "Symposium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SymposiumWorkshopEnrollment_Workshop_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkshopSymposium",
                columns: table => new
                {
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    SymposiumId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MaxParticipants = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkshopSymposium", x => new { x.SymposiumId, x.WorkshopId });
                    table.ForeignKey(
                        name: "FK_WorkshopSymposium_Symposium_SymposiumId",
                        column: x => x.SymposiumId,
                        principalTable: "Symposium",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkshopSymposium_Workshop_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "Workshop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SymposiumWorkshopEnrollment_PersonId",
                table: "SymposiumWorkshopEnrollment",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SymposiumWorkshopEnrollment_WorkshopId",
                table: "SymposiumWorkshopEnrollment",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopSymposium_WorkshopId",
                table: "WorkshopSymposium",
                column: "WorkshopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymposiumWorkshopEnrollment");

            migrationBuilder.DropTable(
                name: "WorkshopSymposium");
        }
    }
}
