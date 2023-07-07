using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileSystem.Migrations
{
    /// <inheritdoc />
    public partial class FolderSecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "FolderId",
                table: "Folders",
                newName: "ParentFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_FolderId",
                table: "Folders",
                newName: "IX_Folders_ParentFolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentFolderId",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "ParentFolderId",
                table: "Folders",
                newName: "FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_ParentFolderId",
                table: "Folders",
                newName: "IX_Folders_FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_FolderId",
                table: "Folders",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}
