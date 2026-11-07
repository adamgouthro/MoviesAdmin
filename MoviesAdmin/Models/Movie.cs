using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; } // unique id for each movie


        [StringLength(100)]
        [Required]
        public string Title { get; set; } = string.Empty; // movie title

        [StringLength(750)]
        [Required] 
        public string Synopsis {  get; set; } = string.Empty; // movie synopsis 

        [Display(Name = "Genre")]
        public int GenreId { get; set; }

        // Mostly one word genre's
        [ValidateNever]
        public Genre Genre { get; set; } = null!; // movie genere

        [ValidateNever]
        public Director Director { get; set; } = null!;

        [Display(Name = "Director")]
        public int DirectorId { get; set; }


        [Display(Name = "Audience Rating")]
        public int AudienceRatingId { get; set; }

        [ValidateNever]
        public AudienceRating Rating {  get; set; } = null!; // rating PG-13

        [ValidateNever]
        [ScaffoldColumn(false)]        public List<CriticReview> CriticReviews { get; set; } = new List<CriticReview>();

        [Range(1, 240)]
        [Display(Name = "Runtime (Minutes)")]
        [Required]
        public int Runtime { get; set; } // runtime in minutes

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; } // movie release date

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "Created")]
        [Required] 
        public DateTime CreatedDate { get; set; } = DateTime.Now; // time the movie is created

        public string ImageUrl { get; set; } = string.Empty;
    }
}