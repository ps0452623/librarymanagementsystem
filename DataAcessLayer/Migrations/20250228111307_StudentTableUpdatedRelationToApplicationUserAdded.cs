using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class StudentTableUpdatedRelationToApplicationUserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Faculties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Faculties_UserId1",
                table: "Faculties",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculties_AspNetUsers_UserId1",
                table: "Faculties",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculties_AspNetUsers_UserId1",
                table: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Faculties_UserId1",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Faculties");
        }
    }
}
