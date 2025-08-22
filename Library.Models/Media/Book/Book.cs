namespace Library.Models.Media.Book
{
    /// <summary>
    /// Book entity details
    /// </summary>
    public class Book : BookCreationRequest
    {
        /// <summary>
        /// Unique Identifier for the media content
        /// </summary>
        public int Id { get; set; }
    }
}
