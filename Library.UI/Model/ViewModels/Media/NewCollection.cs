namespace Library.UI.Model.ViewModels.Media
{
    public class NewCollection
    {
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Books within the collection
        /// </summary>
        public List<NewCollectionContent> Books { get; set; } = new();
        /// <summary>
        /// Music within the collection
        /// </summary>
        public List<NewCollectionContent> Music { get; set; } = new();
        /// <summary>
        /// Movies within the collection
        /// </summary>
        public List<NewCollectionContent> Movies { get; set; } = new();
        /// <summary>
        /// Collections that can be subcollections within the collection
        /// </summary>
        public List<NewCollectionContent> Collections { get; set; } = new();
    }

    public class NewCollectionContent
    {
        /// <summary>
        /// Content/Collection identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Content/Collection Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Type of the content
        /// </summary>
        public MediaType Type { get; set; }
        /// <summary>
        /// Is the element selected to be in the collection
        /// </summary>
        public bool Selected { get; set; }
        /// <summary>
        /// Image for the media
        /// </summary>
        public byte[]? Image { get; set; }
    }
}
