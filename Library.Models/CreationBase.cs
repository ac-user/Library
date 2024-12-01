namespace Library.Models
{
    public class CreationBase
    {
        /// <summary>
        /// What the mendia content is called
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// How would it be categorized
        /// </summary>
        public List<string> Genre { get; set; }
        /// <summary>
        /// Rating
        /// </summary>
        public int? Stars { get; set; }
        /// <summary>
        /// Language it is in
        /// </summary>
        public string? Language { get; set; }
        /// <summary>
        /// A short description of the content
        /// </summary>
        public string? Description { get; set; }
    }
}
