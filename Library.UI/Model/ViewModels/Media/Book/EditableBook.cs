namespace Library.UI.Model.ViewModels.Media.Book
{
    public class EditableBook : NewBase
    {
        /// <summary>
        /// Unique identifier for the book
        /// </summary>
        public string? ISBN { get; set; }
        /// <summary>
        /// The person who wrote the book
        /// </summary>
        public string? Series { get; set; }
        /// <summary>
        /// The person who wrote the book
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// The company who printed the book
        /// </summary>
        public string? Publisher { get; set; }
        /// <summary>
        /// When was this release published
        /// </summary>
        public DateTime? Published { get; set; }
        /// <summary>
        /// A short summary about the book
        /// </summary>
        public string? Summary { get; set; }
        /// <summary>
        /// The short description about the book
        /// </summary>
        public string? Description { get; set; }
    }
}
