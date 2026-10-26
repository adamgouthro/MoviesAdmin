using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class Relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Movie");

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingId",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AudienceRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudienceRatingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudienceRating_AudienceRating_AudienceRatingId",
                        column: x => x.AudienceRatingId,
                        principalTable: "AudienceRating",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_GenreId",
                table: "Movie",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Movie_RatingId",
                table: "Movie",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceRating_AudienceRatingId",
                table: "AudienceRating",
                column: "AudienceRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GenreId",
                table: "Genre",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_AudienceRating_RatingId",
                table: "Movie",
                column: "RatingId",
                principalTable: "AudienceRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_GenreId",
                table: "Movie",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_AudienceRating_RatingId",
                table: "Movie");

            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Genre_GenreId",
                table: "Movie");

            migrationBuilder.DropTable(
                name: "AudienceRating");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Movie_GenreId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_RatingId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "RatingId",
                table: "Movie");

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Movie",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Movie",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
