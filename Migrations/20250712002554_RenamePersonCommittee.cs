using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apis.Migrations
{
    /// <inheritdoc />
    public partial class RenamePersonCommittee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonScientificCommittee_Person_PersonId",
                table: "PersonScientificCommittee");

            migrationBuilder.RenameColumn(
                name: "ScientificCommiteesId",
                table: "PersonScientificCommittee",
                newName: "ScientificCommiteeId");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "PersonScientificCommittee",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonScientificCommittee_ScientificCommiteesId",
                table: "PersonScientificCommittee",
                newName: "IX_PersonScientificCommittee_ScientificCommiteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonScientificCommittee_Person_PersonId",
                table: "PersonScientificCommittee",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonScientificCommittee_Person_PeopleId",
                table: "PersonScientificCommittee");

            migrationBuilder.RenameColumn(
                name: "ScientificCommiteesId",
                table: "PersonScientificCommittee",
                newName: "ScientificCommiteeId");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "PersonScientificCommittee",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_PersonScientificCommittee_ScientificCommiteesId",
                table: "PersonScientificCommittee",
                newName: "IX_PersonScientificCommittee_ScientificCommiteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonScientificCommittee_Person_PersonId",
                table: "PersonScientificCommittee",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
