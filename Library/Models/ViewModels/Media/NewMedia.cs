using System.ComponentModel.DataAnnotations;

namespace Library.Models.ViewModels.Media
{
    public class NewMedia
    {
        public MediaType MediaType { get; set; }
        public NewBook Book { get; set; } = new NewBook();
        public NewMusic Music { get; set; } = new NewMusic();
        public NewMovie Movie { get; set; } = new NewMovie();
    }

    public class NewBook : NewBase
    {
        public string? Description { get; set; }
        public DateTime? Published { get; set; }
        public string? Summary { get; set; }
        public string? Author { get; set; }
        public string? ISBN { get; set; }
    }

    public class NewMusic : NewBase
    {
        public string? Album { get; set; }

        public string? Writer { get; set; }

        public string? Singer { get; set; }

        public DateTime? DatePublished { get; set; }
    }

    public class NewMovie : NewBase
    {
        public string? Summary { get; set; }

        public string? Series { get; set; }

        public string? Writer { get; set; }

        public DateTime? DateReleased { get; set; }

    }

    public class NewBase
    {
        public string? Title { get; set; }

    }

    public enum MediaType
    {
        Book,
        Music,
        Movie
    }
}
