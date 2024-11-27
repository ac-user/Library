namespace Library.Models.Media.Movies
{
    public class Movie : Base
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
    }
}