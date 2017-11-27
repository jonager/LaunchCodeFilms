using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LaunchCodeFilms.Migrations
{
    public partial class QueuetoQueues_MovieDBIdtoMovieIDAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queue_Movies_MovieId",
                table: "Queue");

            migrationBuilder.DropForeignKey(
                name: "FK_Queue_users_UserId",
                table: "Queue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Queue",
                table: "Queue");

            migrationBuilder.DropColumn(
                name: "MovieDBID",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Queue",
                newName: "Queues");

            migrationBuilder.RenameIndex(
                name: "IX_Queue_UserId",
                table: "Queues",
                newName: "IX_Queues_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Queue_MovieId",
                table: "Queues",
                newName: "IX_Queues_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "MovieIDAPI",
                table: "Movies",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Queues",
                table: "Queues",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_Movies_MovieId",
                table: "Queues",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queues_users_UserId",
                table: "Queues",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Queues_Movies_MovieId",
                table: "Queues");

            migrationBuilder.DropForeignKey(
                name: "FK_Queues_users_UserId",
                table: "Queues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Queues",
                table: "Queues");

            migrationBuilder.DropColumn(
                name: "MovieIDAPI",
                table: "Movies");

            migrationBuilder.RenameTable(
                name: "Queues",
                newName: "Queue");

            migrationBuilder.RenameIndex(
                name: "IX_Queues_UserId",
                table: "Queue",
                newName: "IX_Queue_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Queues_MovieId",
                table: "Queue",
                newName: "IX_Queue_MovieId");

            migrationBuilder.AddColumn<int>(
                name: "MovieDBID",
                table: "Movies",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Queue",
                table: "Queue",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Queue_Movies_MovieId",
                table: "Queue",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Queue_users_UserId",
                table: "Queue",
                column: "UserId",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
