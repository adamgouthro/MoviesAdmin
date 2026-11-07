using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Director
    {

        public int Id { get; set; }

        [ValidateNever]
        public List<Movie> Movies { get; set; } = new List<Movie>();

        [Required]
        public string Name { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:MMM d, yyyy}")]
        [Display(Name = "Release Date")]
        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Birthplace { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
