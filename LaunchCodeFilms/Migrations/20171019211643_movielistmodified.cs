using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LaunchCodeFilms.Migrations
{
    public partial class movielistmodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRating",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "HasReviewed",
                table: "MovieList");

            migrationBuilder.AddColumn<double>(
                name: "AverageUserRating",
                table: "Movies",
                type: "float8",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageUserRating",
                table: "Movies");

            migrationBuilder.AddColumn<double>(
                name: "UserRating",
                table: "Movies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "HasReviewed",
                table: "MovieList",
                nullable: false,
                defaultValue: false);
        }
    }
}
