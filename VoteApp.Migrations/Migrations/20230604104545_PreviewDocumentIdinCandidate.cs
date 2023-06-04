using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class PreviewDocumentIdinCandidate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreviewDocumentId",
                table: "Candidate",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviewDocumentId",
                table: "Candidate");
        }
    }
}
