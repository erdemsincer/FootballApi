using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class emre2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_TeamId",
                table: "PlayerStatistics",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Teams_TeamId",
                table: "PlayerStatistics",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Teams_TeamId",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_TeamId",
                table: "PlayerStatistics");
        }
    }
}
