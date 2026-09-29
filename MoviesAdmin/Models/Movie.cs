namespace MoviesAdmin.Models
{
    public class Movie
    {
        public int Id { get; set; } // unique id for each movie

        public string Title { get; set; } = string.Empty; // movie title

        public string Synopsis {  get; set; } = string.Empty; // movie synopsis 

        public string Genre {  get; set; } = string.Empty; // movie genere

        public string Rating {  get; set; } = string.Empty; // rating PG-13

        public int Runtime { get; set; } // runtime in minutes

        public DateTime ReleaseDate { get; set; } // movie release date

        public DateTime CreatedDate { get; set; } = DateTime.Now; // time the movie is created

        public string Director { get; set; } = string.Empty; // director of movie

        public string MovieStudio { get; set; } = string.Empty; // movie studio filmed by

        public string Screenwriter {  get; set; } = string.Empty; // screenwriter of movie

        public string Distributor {  get; set; } = string.Empty; // the movies distributer

    }
}