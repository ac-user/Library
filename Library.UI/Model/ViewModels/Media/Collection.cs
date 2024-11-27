using Library.UI.Model.ViewModels.Media.Movie;

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
        public List<Book.Book> Books { get; set; }
        /// <summary>
        /// Music within the collection
        /// </summary>
        public List<Music.Music> Music { get; set; }
        /// <summary>
        /// Movies within the collection
        /// </summary>
        public List<Movies> Movies { get; set; }
        /// <summary>
        /// Subcollections within the collection
        /// </summary>
        public List<Collection> SubCollections { get; set; }
    }


}
