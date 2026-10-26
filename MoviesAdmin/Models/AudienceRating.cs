using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MoviesAdmin.Models
{
    public class AudienceRating
    {
        public int Id { get; set; }

        [ValidateNever]
        public List<AudienceRating> ratings { get; set; } = new List<AudienceRating>();

        [ValidateNever]
        public string Title { get; set; } = string.Empty;

    }
}
