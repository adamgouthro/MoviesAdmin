using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class Genre
    {
        public int Id { get; set; }


        [ValidateNever]
        public List<Genre> genres { get; set; } = new List<Genre>();

        [Required]
        public string Title { get; set; } = string.Empty;

    }
}
