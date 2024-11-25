namespace Library.Models.ViewModels.Media.Music
{
    public class Music
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
        /// The album the song came from
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// Who wrote the song
        /// </summary>
        public string Writer { get; set; }
        /// <summary>
        /// Who sung the song
        /// </summary>
        public string Singer { get; set; }
        /// <summary>
        /// When was it released
        /// </summary>
        public DateTime? DatePublished { get; set; }
    }
}