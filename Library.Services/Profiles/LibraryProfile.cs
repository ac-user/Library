using AutoMapper;
using Library.Models.Media.Book;
using Library.Models.Media.Movies;
using Library.Models.Media.Music;
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
            CreateMap<Book, Entity.Book>()
                .ForMember(d=> d.BookId, o =>o.MapFrom(s => s.Id)).ReverseMap();
            CreateMap<Book, BookCreationRequest>().ReverseMap();
            CreateMap<Book, BookModificationRequest>().ReverseMap();

            CreateMap<Movie, Entity.Movie>()
                .ForMember(d => d.MovieId, o => o.MapFrom(s => s.Id)).ReverseMap();
            CreateMap<Movie, MovieCreationRequest>().ReverseMap();
            CreateMap<Movie, MovieModificationRequest>().ReverseMap();
            
            CreateMap<Music, Entity.Music>()
                .ForMember(d => d.MusicId, o => o.MapFrom(s => s.Id)).ReverseMap();
            CreateMap<Music, MusicCreationRequest>().ReverseMap();
            CreateMap<Music, MusicModificationRequest>().ReverseMap();
        }

    }
}
