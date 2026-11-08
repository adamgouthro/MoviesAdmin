using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        public int Id { get; set; }

        //foreign key 
        [Display(Name = "Movie")]
        [Required]
        public int MovieId { get; set; }

        [ValidateNever]
        public Movie Movie { get; set; } = null!;
        
        [Required]
        [StringLength(1000)]
        [Display(Name = "Review Text")]
        public string Description { get; set; } = string.Empty; // the review

        [Required]
        [Range(0, 5)]
        [Display(Name = "Rating (0 - 5)")]
        public int Rating { get; set; } // 1-5 star rating of movie

        [Required]
        [Display(Name = "Publish Now")]
        public bool IsPublished { get; set; } = false; // has it been published or not (is it a draft?)

        public string CreatedBy { get; set; } = string.Empty; // who the review was created by

        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp of when it was created

    }
}
