using AutoMapper;
using Adapter = Library.Models.Adapter;
using Views = Library.Models.ViewModels;

namespace Library.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile() 
        {
            MediaContentMaps();
        }

        private void MediaContentMaps()
        {
            CreateMap<Views.Media.NewBook, Adapter.Media.Book.BookCreationRequest>().ReverseMap();
            CreateMap<Views.Media.NewMusic, Adapter.Media.Music.MusicCreationRequest>().ReverseMap();
            CreateMap<Views.Media.NewMovie, Adapter.Media.Movies.MovieModificationRequest>().ReverseMap();
            
            CreateMap<Views.Media.Book.Book, Adapter.Media.Book.Book>().ReverseMap();
            CreateMap<Views.Media.Book.Book, Adapter.Media.Book.BookModificationRequest>().ReverseMap();
            
            CreateMap<Views.Media.Music.Music, Adapter.Media.Music.Music>().ReverseMap();
            CreateMap<Views.Media.Music.Music, Adapter.Media.Music.MusicModificationRequest>().ReverseMap();
            
            CreateMap<Views.Media.Movie.Movies, Adapter.Media.Movies.Movie>().ReverseMap();
            CreateMap<Views.Media.Movie.Movies, Adapter.Media.Movies.MovieModificationRequest>().ReverseMap();

        }
    }
}
