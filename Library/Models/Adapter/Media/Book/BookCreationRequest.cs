namespace Library.Models.Adapter.Media.Book
{
    /// <summary>
    /// Request details to create a new book entry
    /// </summary>
    public class BookCreationRequest
    {
        /// <summary>
        /// What the mendia content is called
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Unique identifier for the media
        /// </summary>
        /// <example>Book ISBN: 123-123645-452</example>
        public string Identification { get; set; }
        /// <summary>
        /// The person who wrote the book
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// The company who printed the book
        /// </summary>
        public string? Publisher { get; set; }
        /// <summary>
        /// The short description about the book
        /// </summary>
        public string? Description { get; set; }

    }
}