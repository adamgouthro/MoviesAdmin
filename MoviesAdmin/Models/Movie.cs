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

        [StringLength(20)] // Mostly one word genre's
        [Required] 
        public string Genre {  get; set; } = string.Empty; // movie genere

        [StringLength(10)] // It should just be an abbriviation like PG-13, R, etc...
        [Required] 
        public string Rating {  get; set; } = string.Empty; // rating PG-13

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

        [StringLength(100)]
        [Display(Name = "Movie Studio")] 
        [Required] 
        public string MovieStudio { get; set; } = string.Empty; // movie studio filmed by

        [StringLength(100)]
        [Required] 
        public string Screenwriter {  get; set; } = string.Empty; // screenwriter of movie

        [StringLength(100)]
        [Required] 
        public string Distributor {  get; set; } = string.Empty; // the movies distributer

    }
}