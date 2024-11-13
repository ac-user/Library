namespace Library.Services.Models.Media.Book
{
    /// <summary>
    /// Request details to create a new book entry
    /// </summary>
    public class BookCreationRequest : CreationBase
    {
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