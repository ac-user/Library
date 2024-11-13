namespace Library.Services.Models
{
    public class CreationBase
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
    }
}
