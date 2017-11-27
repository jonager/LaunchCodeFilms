using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LaunchCodeFilms.Migrations
{
    public partial class MoviliestrenamedtoQueue_somepropetiresMovedaround : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieList");

            migrationBuilder.DropColumn(
                name: "IsFavorite",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Queue",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Favorite = table.Column<bool>(type: "bool", nullable: false),
                    MovieId = table.Column<int>(type: "int4", nullable: false),
                    NotifyTheater = table.Column<bool>(type: "bool", nullable: false),
                    UserId = table.Column<int>(type: "int4", nullable: false),
                    Watched = table.Column<bool>(type: "bool", nullable: false),
                    Watchlist = table.Column<bool>(type: "bool", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Queue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Queue_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Queue_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Queue_MovieId",
                table: "Queue",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Queue_UserId",
                table: "Queue",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Queue");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reviews");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavorite",
                table: "Reviews",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HasRated = table.Column<bool>(nullable: false),
                    HasWatched = table.Column<bool>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Watchlist = table.Column<bool>(nullable: false)
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
    }
}
