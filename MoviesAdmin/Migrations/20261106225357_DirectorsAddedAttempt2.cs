using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class DirectorsAddedAttempt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Director_Director_DirectorId",
                table: "Director");

            migrationBuilder.DropIndex(
                name: "IX_Director_DirectorId",
                table: "Director");

            migrationBuilder.DropColumn(
                name: "DirectorId",
                table: "Director");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DirectorId",
                table: "Director",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Director_DirectorId",
                table: "Director",
                column: "DirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Director_Director_DirectorId",
                table: "Director",
                column: "DirectorId",
                principalTable: "Director",
                principalColumn: "Id");
        }
    }
}
