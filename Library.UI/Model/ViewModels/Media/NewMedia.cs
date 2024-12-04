using Library.UI.Model.ViewModels.Media.Book;
using Library.UI.Model.ViewModels.Media.Movie;
using Library.UI.Model.ViewModels.Media.Music;

namespace Library.UI.Model.ViewModels.Media
{
    public class NewMedia
    {
        public MediaType MediaType { get; set; }
        public EditableBook Book { get; set; } = new EditableBook();
        public EditableMusic Music { get; set; } = new EditableMusic();
        public EditableMovie Movie { get; set; } = new EditableMovie();
    }
        
    public enum MediaType
    {
        Book,
        Movie,
        Music
    }
}
