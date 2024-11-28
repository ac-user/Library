namespace Library.UI.Model.ViewModels.Media
{
    public class NewBase
    {
        /// <summary>
        /// What the mendia content is called
        /// </summary>
        public string? Title { get; set; }
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
