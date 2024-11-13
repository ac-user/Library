using AutoMapper;
using Library.Services.Models.Media.Book;
using Library.Services.Models.Media.Movies;
using Library.Services.Models.Media.Music;

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
            CreateMap<Book, BookCreationRequest>().ReverseMap();
            CreateMap<Book, BookModificationRequest>().ReverseMap();

            CreateMap<Movie, MovieCreationRequest>().ReverseMap();
            CreateMap<Movie, MovieModificationRequest>().ReverseMap();
            
            CreateMap<Music, MusicCreationRequest>().ReverseMap();
            CreateMap<Music, MusicModificationRequest>().ReverseMap();
        }

    }
}
