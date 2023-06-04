using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VoteApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class initCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CandidateModelId",
                table: "Document",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidate_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Document_CandidateModelId",
                table: "Document",
                column: "CandidateModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_UserId",
                table: "Candidate",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Candidate_CandidateModelId",
                table: "Document",
                column: "CandidateModelId",
                principalTable: "Candidate",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Candidate_CandidateModelId",
                table: "Document");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropIndex(
                name: "IX_Document_CandidateModelId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "CandidateModelId",
                table: "Document");
        }
    }
}
