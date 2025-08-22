namespace Library.Models.Media.Music
{
    public class MusicCreationRequest : CreationBase
    {
        /// <summary>
        /// The album the song came from
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// Who wrote the song
        /// </summary>
        public string Composer { get; set; }
        /// <summary>
        /// Who sung the song
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// When was it released
        /// </summary>
        public DateTime? DateReleased { get; set; }
        /// <summary>
        /// How many times did you listen to it
        /// </summary>
        public int TimesListenedTo { get; set; }
    }
}
