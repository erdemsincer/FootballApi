using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerStatistics3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "PlayerStatistics");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerStatistics_Players_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerStatistics_Players_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "PlayerStatistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
