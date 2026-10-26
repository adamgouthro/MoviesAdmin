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

        // Mostly one word genre's
        [ValidateNever]
        [Required]
        public Genre Genre { get; set; } = null!; // movie genere

        [ValidateNever]
        [Required] 
        public AudienceRating Rating {  get; set; } = null!; // rating PG-13

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

        [StringLength(100)]
        [Required]
        public string Director { get; set; } = string.Empty; // director of movie

        public string ImageUrl { get; set; } = string.Empty;
    }
}