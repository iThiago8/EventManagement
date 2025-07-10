using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apis.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyArticleReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticlePerson",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    AuthorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticlePerson", x => new { x.ArticlesId, x.AuthorsId });
                    table.ForeignKey(
                        name: "FK_ArticlePerson_Article_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Article",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticlePerson_Person_AuthorsId",
                        column: x => x.AuthorsId,
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
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    ScientificCommiteesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonScientificCommittee", x => new { x.PeopleId, x.ScientificCommiteesId });
                    table.ForeignKey(
                        name: "FK_PersonScientificCommittee_Person_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonScientificCommittee_ScientificCommittee_ScientificComm~",
                        column: x => x.ScientificCommiteesId,
                        principalTable: "ScientificCommittee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArticlePerson_AuthorsId",
                table: "ArticlePerson",
                column: "AuthorsId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleReview_ScientificCommitteeId",
                table: "ArticleReview",
                column: "ScientificCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonScientificCommittee_ScientificCommiteesId",
                table: "PersonScientificCommittee",
                column: "ScientificCommiteesId");
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
        }
    }
}
