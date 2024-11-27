namespace Library.UI.Model.ViewModels.Media.Movie
{
    public class Movies
    {
        /// <summary>
        /// Unique Identifier for the media content
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// What the mendia content is called
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Series the show is from, if any
        /// </summary>
        public string? Series { get; set; }
        /// <summary>
        /// Who wrote the show
        /// </summary>
        public string? Writer { get; set; }
        /// <summary>
        /// When was it released
        /// </summary>
        public DateTime? DateReleased { get; set; }
        /// <summary>
        /// Summary of the show
        /// </summary>
        public string? Summary { get; set; }
    }
}