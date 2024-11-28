namespace Library.Models
{
    public class Base
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
        /// A description of the media content
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// How would it be categorized
        /// </summary>
        public List<string> Genre { get; set; }
        /// <summary>
        /// Rating
        /// </summary>
        public int? Stars { get; set; }
    }
}
