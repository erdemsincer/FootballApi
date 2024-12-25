using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class emre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "PlayerStatistics",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "PlayerStatistics");
        }
    }
}
