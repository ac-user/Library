namespace Library.Models.Adapter.Media.Book
{
    public class Book
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
        /// The person who wrote the book
        /// </summary>
        public string Series { get; set; }
        /// <summary>
        /// The person who wrote the book
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// The company who printed the book
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// When was this release published
        /// </summary>
        public DateTime Published { get; set; }
        /// <summary>
        /// The short description about the book
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// A short summary about the book
        /// </summary>
        public string Summary { get; set; }
    }
}
