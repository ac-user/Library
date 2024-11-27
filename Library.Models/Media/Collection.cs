using MediaModel = Library.Models.Media;

namespace Library.Models.Media
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
        public List<MediaModel.Book.Book> Books { get; set; }
        /// <summary>
        /// Music within the collection
        /// </summary>
        public List<MediaModel.Music.Music> Music { get; set; }
        /// <summary>
        /// Movies within the collection
        /// </summary>
        public List<MediaModel.Movies.Movie> Movies { get; set; }
        /// <summary>
        /// Subcollections within the collection
        /// </summary>
        public List<Collection> SubCollections { get; set; }
    }
}
