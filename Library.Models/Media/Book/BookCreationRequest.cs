namespace Library.Models.Media.Book
{
    /// <summary>
    /// Request details to create a new book entry
    /// </summary>
    public class BookCreationRequest : CreationBase
    {
        /// <summary>
        /// Secondary title
        /// </summary>
        public string? SubTitle { get; set; }
        /// <summary>
        /// Unique identifier for the media
        /// </summary>
        /// <example>Book ISBN: 123-123645-452</example>
        public string Identification { get; set; }
        /// <summary>
        /// The person who wrote the book
        /// </summary>
        public string? Series { get; set; }
        /// <summary>
        /// The person who wrote the book
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// Who created the art
        /// </summary>
        public string? Artist { get; set; }
        /// <summary>
        /// The company who printed the book
        /// </summary>
        public string? Publisher { get; set; }
        /// <summary>
        /// When was this release published
        /// </summary>
        public DateTime? Published { get; set; }
        /// <summary>
        /// A short summary about the book
        /// </summary>
        public string? Summary { get; set; }
        /// <summary>
        /// Number of pages within the book
        /// </summary>
        public int Pages { get; set; }
        /// <summary>
        /// How many times read
        /// </summary>
        public int? TimesRead { get; set; }
        /// <summary>
        /// Is the series ongoing
        /// </summary>
        public bool Ongoing { get; set; }
        /// <summary>
        /// Are you currently reading series
        /// </summary>
        public bool IsActivelyReading { get; set; }

    }
}