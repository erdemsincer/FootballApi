﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class erdemece : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerId",
                table: "PlayerStatistics",
                column: "PlayerId");
        }
    }
}
