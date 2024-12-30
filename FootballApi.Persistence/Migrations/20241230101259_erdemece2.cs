using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class erdemece2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerStatisticId",
                table: "Players",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerStatisticId",
                table: "Players");
        }
    }
}
