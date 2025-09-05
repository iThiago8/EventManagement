using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(type: "longtext", nullable: false),
                    Number = table.Column<string>(type: "longtext", nullable: false),
                    Complement = table.Column<string>(type: "longtext", nullable: true),
                    Neighborhood = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    State = table.Column<string>(type: "longtext", nullable: false),
                    PostalCode = table.Column<string>(type: "longtext", nullable: false),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Cpf = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subject", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Symposium",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LocationAddressId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symposium", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symposium_Address_LocationAddressId",
                        column: x => x.LocationAddressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Abstract = table.Column<string>(type: "longtext", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ScientificCommittee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientificCommittee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientificCommittee_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workshop_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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

            migrationBuilder.CreateTable(
                name: "ArticlePerson",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePerson", x => new { x.ArticleId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_ArticlePerson_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePerson_Person_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArticleReview",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    ScientificCommitteeId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<float>(type: "float", nullable: false),
                    Review = table.Column<string>(type: "longtext", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleReview", x => new { x.ArticleId, x.ScientificCommitteeId });
                    table.ForeignKey(
                        name: "FK_ArticleReview_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleReview_ScientificCommittee_ScientificCommitteeId",
                        column: x => x.ScientificCommitteeId,
                        principalTable: "ScientificCommittee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PersonScientificCommittee",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    ScientificCommiteeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonScientificCommittee", x => new { x.PersonId, x.ScientificCommiteeId });
                    table.ForeignKey(
                        name: "FK_PersonScientificCommittee_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonScientificCommittee_ScientificCommittee_ScientificComm~",
                        column: x => x.ScientificCommiteeId,
                        principalTable: "ScientificCommittee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

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
                name: "IX_Article_SubjectId",
                table: "Article",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePerson_AuthorId",
                table: "ArticlePerson",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleReview_ScientificCommitteeId",
                table: "ArticleReview",
                column: "ScientificCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonScientificCommittee_ScientificCommiteeId",
                table: "PersonScientificCommittee",
                column: "ScientificCommiteeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSymposium_SymposiumId",
                table: "PersonSymposium",
                column: "SymposiumId");

            migrationBuilder.CreateIndex(
                name: "IX_ScientificCommittee_SubjectId",
                table: "ScientificCommittee",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Symposium_LocationAddressId",
                table: "Symposium",
                column: "LocationAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SymposiumWorkshopEnrollment_PersonId",
                table: "SymposiumWorkshopEnrollment",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_SymposiumWorkshopEnrollment_WorkshopId",
                table: "SymposiumWorkshopEnrollment",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_SubjectId",
                table: "Workshop",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkshopSymposium_WorkshopId",
                table: "WorkshopSymposium",
                column: "WorkshopId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticlePerson");

            migrationBuilder.DropTable(
                name: "ArticleReview");

            migrationBuilder.DropTable(
                name: "PersonScientificCommittee");

            migrationBuilder.DropTable(
                name: "PersonSymposium");

            migrationBuilder.DropTable(
                name: "SymposiumWorkshopEnrollment");

            migrationBuilder.DropTable(
                name: "WorkshopSymposium");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "ScientificCommittee");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Symposium");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Subject");
        }
    }
}
