using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LaunchCodeFilms.Migrations
{
    public partial class MoviListModeladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HasRated = table.Column<bool>(type: "bool", nullable: false),
                    HasReviewed = table.Column<bool>(type: "bool", nullable: false),
                    HasWatched = table.Column<bool>(type: "bool", nullable: false),
                    IsFavorite = table.Column<bool>(type: "bool", nullable: false),
                    MovieId = table.Column<int>(type: "int4", nullable: false),
                    UserId = table.Column<int>(type: "int4", nullable: false),
                    Watchlist = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieList", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MovieList_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieList_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_MovieId",
                table: "MovieList",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_UserId",
                table: "MovieList",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieList");
        }
    }
}
