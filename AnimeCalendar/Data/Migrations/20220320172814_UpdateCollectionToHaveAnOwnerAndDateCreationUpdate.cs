using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeCalendar.Data.Migrations
{
    public partial class UpdateCollectionToHaveAnOwnerAndDateCreationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InCollection_Animes_AnimeId",
                table: "InCollection");

            migrationBuilder.DropForeignKey(
                name: "FK_InCollection_Collection_CollectionId",
                table: "InCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InCollection",
                table: "InCollection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.RenameTable(
                name: "InCollection",
                newName: "InCollections");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collections");

            migrationBuilder.RenameIndex(
                name: "IX_InCollection_CollectionId",
                table: "InCollections",
                newName: "IX_InCollections_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_InCollection_AnimeId",
                table: "InCollections",
                newName: "IX_InCollections_AnimeId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Collections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InCollections",
                table: "InCollections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InCollections_Animes_AnimeId",
                table: "InCollections",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InCollections_Collections_CollectionId",
                table: "InCollections",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_InCollections_Animes_AnimeId",
                table: "InCollections");

            migrationBuilder.DropForeignKey(
                name: "FK_InCollections_Collections_CollectionId",
                table: "InCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InCollections",
                table: "InCollections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_UserId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "InCollections",
                newName: "InCollection");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Collection");

            migrationBuilder.RenameIndex(
                name: "IX_InCollections_CollectionId",
                table: "InCollection",
                newName: "IX_InCollection_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_InCollections_AnimeId",
                table: "InCollection",
                newName: "IX_InCollection_AnimeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InCollection",
                table: "InCollection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InCollection_Animes_AnimeId",
                table: "InCollection",
                column: "AnimeId",
                principalTable: "Animes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InCollection_Collection_CollectionId",
                table: "InCollection",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
