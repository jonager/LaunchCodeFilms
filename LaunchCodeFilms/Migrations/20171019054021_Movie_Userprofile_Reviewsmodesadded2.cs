using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LaunchCodeFilms.Migrations
{
    public partial class Movie_Userprofile_Reviewsmodesadded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_MovieID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "MovieDBID",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "MovieID",
                table: "Reviews",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MovieID",
                table: "Reviews",
                newName: "IX_Reviews_MovieId");

            migrationBuilder.AlterColumn<int>(
                name: "MovieId",
                table: "Reviews",
                type: "int4",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_MovieId",
                table: "Reviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Movies_MovieId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Reviews",
                newName: "MovieID");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                newName: "IX_Reviews_MovieID");

            migrationBuilder.AlterColumn<int>(
                name: "MovieID",
                table: "Reviews",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int4");

            migrationBuilder.AddColumn<int>(
                name: "MovieDBID",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Movies_MovieID",
                table: "Reviews",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
