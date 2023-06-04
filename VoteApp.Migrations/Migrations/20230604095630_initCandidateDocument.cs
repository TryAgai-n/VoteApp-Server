using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class initCandidateDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Candidate_CandidateModelId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_CandidateModelId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CandidateModelId",
                table: "Document");

            migrationBuilder.CreateTable(
                name: "CandidateDocument",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "integer", nullable: false),
                    DocumentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateDocument", x => new { x.CandidateId, x.DocumentId });
                    table.ForeignKey(
                        name: "FK_CandidateDocument_Candidate_CandidateId",
                        column: x => x.CandidateId,
                        principalTable: "Candidate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateDocument_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDocument_DocumentId",
                table: "CandidateDocument",
                column: "DocumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateDocument");

            migrationBuilder.AddColumn<int>(
                name: "CandidateModelId",
                table: "Document",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_CandidateModelId",
                table: "Document",
                column: "CandidateModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Candidate_CandidateModelId",
                table: "Document",
                column: "CandidateModelId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }
    }
}
