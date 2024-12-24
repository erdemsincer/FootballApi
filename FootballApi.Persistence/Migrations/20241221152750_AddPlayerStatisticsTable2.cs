using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPlayerStatisticsTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.AlterColumn<double>(
                name: "PassAccuracy",
                table: "PlayerStatistics",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "PlayerName",
                table: "PlayerStatistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerName",
                table: "PlayerStatistics");

            migrationBuilder.AlterColumn<decimal>(
                name: "PassAccuracy",
                table: "PlayerStatistics",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "PlayerStatistics",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
