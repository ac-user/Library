namespace Library.UI.Model.ViewModels.Media
{
    public class MediaContentBase
    {
        /// <summary>
        /// Unique media identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// What the media content is called
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// How would it be categorized
        /// </summary>
        public List<string> Genre { get; set; } = new List<string>();
        /// <summary>
        /// Rating
        /// </summary>
        public int Stars { get; set; }
        /// <summary>
        /// Language it is in
        /// </summary>
        public string? Language { get; set; }
        /// <summary>
        /// Png imagge in byte form
        /// </summary>
        public byte[]? Image { get; set; }
    }
}
