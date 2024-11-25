namespace Library.Models.ViewModels.Media
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
        public List<NewCollectionContent> Books { get; set; }
        /// <summary>
        /// Music within the collection
        /// </summary>
        public List<NewCollectionContent> Music { get; set; }
        /// <summary>
        /// Movies within the collection
        /// </summary>
        public List<NewCollectionContent> Movies { get; set; }
        /// <summary>
        /// Subcollections within the collection
        /// </summary>
        public List<NewCollectionContent> SubCollections { get; set; }
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
        /// Is the element selected to be in the collection
        /// </summary>
        public string Selected { get; set; }
    }
}
