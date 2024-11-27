namespace Library.Models.Media.Music
{
    public class Music : Base
    {
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