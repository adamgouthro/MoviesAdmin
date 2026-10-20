namespace MoviesAdmin.Models
{
    public class CriticReview
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty; // the review

        public int Rating { get; set; } // 1-5 star rating of movie

        public bool IsPublished { get; set; } // has it been published or not (is it a draft?)

        public string CreatedBy { get; set; } = string.Empty; // who the review was created by

        public DateTime CreatedDate { get; set; } = DateTime.Now; // timestamp of when it was created

    }
}
