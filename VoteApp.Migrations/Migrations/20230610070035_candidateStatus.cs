using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteApp.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class candidateStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Candidate",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Candidate");
        }
    }
}
