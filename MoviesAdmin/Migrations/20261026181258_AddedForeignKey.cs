using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_AudienceRating_RatingId",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "RatingId",
                table: "Movie",
                newName: "AudienceRatingId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_RatingId",
                table: "Movie",
                newName: "IX_Movie_AudienceRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_AudienceRating_AudienceRatingId",
                table: "Movie",
                column: "AudienceRatingId",
                principalTable: "AudienceRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_AudienceRating_AudienceRatingId",
                table: "Movie");

            migrationBuilder.RenameColumn(
                name: "AudienceRatingId",
                table: "Movie",
                newName: "RatingId");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_AudienceRatingId",
                table: "Movie",
                newName: "IX_Movie_RatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_AudienceRating_RatingId",
                table: "Movie",
                column: "RatingId",
                principalTable: "AudienceRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
