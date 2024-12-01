namespace Library.UI.Model.ViewModels.Media.Movie
{
    public class Movies : MediaContentBase
    {
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
        /// <summary>
        /// How many times watched
        /// </summary>
        public int? TimesWatched { get; set; }
        /// <summary>
        /// Is the series ongoing
        /// </summary>
        public bool Ongoing { get; set; }
        /// <summary>
        /// Are you currently watching series
        /// </summary>
        public bool IsActivelyWatching { get; set; }

    }
}