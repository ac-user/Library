namespace Library.Services.Models
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
        public string Description { get; set; }
    }
}
