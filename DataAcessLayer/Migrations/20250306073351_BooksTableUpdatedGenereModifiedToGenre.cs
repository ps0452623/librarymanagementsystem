using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BooksTableUpdatedGenereModifiedToGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Branches_BranchId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BranchId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Genere",
                table: "Books",
                newName: "Genre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "Genere");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BranchId",
                table: "Books",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Branches_BranchId",
                table: "Books",
                column: "BranchId",
                principalTable: "Branches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
