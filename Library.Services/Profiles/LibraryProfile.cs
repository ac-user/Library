using AutoMapper;
using Library.Services.Models.Media.Book;
using Library.Services.Models.Media.Movies;
using Library.Services.Models.Media.Music;
using Entity = Library.Data.Entities;

namespace Library.Services.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile() 
        {
            MediaContentMaps();
        }

        private void MediaContentMaps()
        {
            CreateMap<Book, Entity.Book>().ReverseMap();
            CreateMap<Book, BookCreationRequest>().ReverseMap();
            CreateMap<Book, BookModificationRequest>().ReverseMap();

            CreateMap<Movie, Entity.Movie>().ReverseMap();
            CreateMap<Movie, MovieCreationRequest>().ReverseMap();
            CreateMap<Movie, MovieModificationRequest>().ReverseMap();
            
            CreateMap<Music, Entity.Music>().ReverseMap();
            CreateMap<Music, MusicCreationRequest>().ReverseMap();
            CreateMap<Music, MusicModificationRequest>().ReverseMap();
        }

    }
}
