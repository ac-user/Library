namespace Library.UI.Model.ViewModels.Media
{
    /// <summary>
    /// Content collections
    /// </summary>
    public class Collection
    {
        /// <summary>
        /// Unique identifier for the collection
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the collection
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Books within the collection
        /// </summary>
        public List<Media> MediaContent { get; set; }
        /// <summary>
        /// Subcollections within the collection
        /// </summary>
        public List<Collection> SubCollections { get; set; }
    }

    public class Media
    {
        /// <summary>
        /// Media content identifier
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the content
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Type of media
        /// </summary>
        public MediaType Type { get; set; }
    }
}
