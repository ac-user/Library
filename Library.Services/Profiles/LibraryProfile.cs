using AutoMapper;
using Library.Models.Media;
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
            CreateMap<string, List<string>>()
                .ConstructUsing(s => s.Split(',', StringSplitOptions.None).ToList());

            CreateMap<Book, Entity.Book>()
                .ForMember(d=> d.BookId, o =>o.MapFrom(s => s.Id))
                .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(',', s.Genre)))
                .ForMember(d => d.Isbn, o => o.MapFrom(s => s.Identification)).ReverseMap();
            CreateMap<Book, BookCreationRequest>().ReverseMap();
            CreateMap<Book, BookModificationRequest>().ReverseMap();

            CreateMap<Movie, Entity.Movie>()
                .ForMember(d => d.MovieId, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(',', s.Genre))).ReverseMap();
            CreateMap<Movie, MovieCreationRequest>().ReverseMap();
            CreateMap<Movie, MovieModificationRequest>().ReverseMap();
            
            CreateMap<Music, Entity.Music>()
                .ForMember(d => d.MusicId, o => o.MapFrom(s => s.Id))
                 .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(',', s.Genre))).ReverseMap();
            CreateMap<Music, MusicCreationRequest>().ReverseMap();
            CreateMap<Music, MusicModificationRequest>().ReverseMap();


            CreateMap<NewCollectionContent, Book>();
            CreateMap<NewCollectionContent, Music>();
            CreateMap<NewCollectionContent, Movie>();
            CreateMap<CollectionCreationRequest, Collection>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.Books, o => o.MapFrom(s => s.NewCollectionContents.Where(w => w.MediaType == MediaContentType.Book)))
                .ForMember(d => d.Music, o => o.MapFrom(s => s.NewCollectionContents.Where(w => w.MediaType == MediaContentType.Music)))
                .ForMember(d => d.Movies, o => o.MapFrom(s => s.NewCollectionContents.Where(w => w.MediaType == MediaContentType.Movie)))
                .ForMember(d => d.SubCollections, o => o.MapFrom(s => s.NewCollectionContents.Where(w => w.MediaType == null))).ReverseMap();
        }

    }
}
