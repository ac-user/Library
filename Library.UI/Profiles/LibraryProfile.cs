using AutoMapper;
using Library.Models.Media;
using Adapter = Library.Models;
using Views = Library.UI.Model.ViewModels;

namespace Library.UI.Profiles
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile() 
        {
            MediaContentMaps();
        }

        private void MediaContentMaps()
        {
            CreatingNewMedia();
            ModifyingMedia();

            CreateMap<Views.Media.MediaCollection, Adapter.Media.Book.Book>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ContentId))
                .ForMember(d => d.Series, o => o.MapFrom(s => s.ColumnTwo))
                .ForMember(d => d.Author, o => o.MapFrom(s => s.ColumnThree))
                .ForMember(d => d.Publisher, o => o.MapFrom(s => s.ColumnFour)).ReverseMap();
            CreateMap<Views.Media.MediaCollection, Adapter.Media.Music.Music>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ContentId))
                .ForMember(d => d.Album, o => o.MapFrom(s => s.ColumnTwo))
                .ForMember(d => d.Artist, o => o.MapFrom(s => s.ColumnThree))
                .ForMember(d => d.Composer, o => o.MapFrom(s => s.ColumnFour)).ReverseMap();
            CreateMap<Views.Media.MediaCollection, Adapter.Media.Movies.Movie>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.ContentId))
                .ForMember(d => d.Series, o => o.MapFrom(s => s.ColumnTwo))
                .ForMember(d => d.Writer, o => o.MapFrom(s => s.ColumnThree))
                .ForMember(d => d.DateReleased, o => o.MapFrom(s => s.ColumnFour)).ReverseMap();

            CreateMap<Views.Media.Media, Adapter.Media.Book.Book>().ReverseMap();
            CreateMap<Views.Media.Media, Adapter.Media.Music.Music>().ReverseMap();
            CreateMap<Views.Media.Media, Adapter.Media.Movies.Movie>().ReverseMap();
            CreateMap<Views.Media.MediaType, Adapter.Media.MediaContentType>().ReverseMap();

            CreateMap<Views.Media.Book.Book, Adapter.Media.Book.Book>()
                .ForMember(d => d.Identification, o => o.MapFrom(s => s.ISBN))
                 .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(',', s.Genre))).ReverseMap();
            CreateMap<Views.Media.Music.Music, Adapter.Media.Music.Music>()
                 .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(',', s.Genre))).ReverseMap();
            CreateMap<Views.Media.Movie.Movies, Adapter.Media.Movies.Movie>()
                 .ForMember(d => d.Genre, o => o.MapFrom(s => string.Join(',', s.Genre))).ReverseMap();
            CreateMap<Views.Media.Collection, Adapter.Media.Collection>()
                .ForMember(d => d.Books, o => o.MapFrom(s => s.MediaContent.Where(w => w.Type == Views.Media.MediaType.Book)))
                .ForMember(d => d.Music, o => o.MapFrom(s => s.MediaContent.Where(w => w.Type == Views.Media.MediaType.Music)))
                .ForMember(d => d.Movies, o => o.MapFrom(s => s.MediaContent.Where(w => w.Type == Views.Media.MediaType.Movie)));
            CreateMap<Adapter.Media.Collection, Views.Media.Collection>();
            CreateMap<Adapter.Media.Collection, Views.Media.SelectableCollection>()
                .ForMember(d => d.Title, o => o.MapFrom(s => s.Name));
            CreateMap<Views.CollectionCards, Adapter.Media.Collection>().ReverseMap();
        }

        private void CreatingNewMedia()
        {
            CreateMap<Views.Media.Book.EditableBook, Adapter.Media.Book.BookCreationRequest>()
                .ForMember(d => d.Identification, o => o.MapFrom(s => s.ISBN)).ReverseMap();
            CreateMap<Views.Media.Music.EditableMusic, Adapter.Media.Music.MusicCreationRequest>().ReverseMap();
            CreateMap<Views.Media.Movie.EditableMovie, Adapter.Media.Movies.MovieCreationRequest>().ReverseMap();

            CreateMap<Views.Media.NewCollectionContent, Adapter.Media.Book.Book>().ReverseMap();
            CreateMap<Views.Media.NewCollectionContent, Adapter.Media.Music.Music>().ReverseMap();
            CreateMap<Views.Media.NewCollectionContent, Adapter.Media.Movies.Movie>().ReverseMap();
            CreateMap<Views.Media.NewCollectionContent, Adapter.Media.Collection>()
                .ForMember(s => s.Name, o => o.MapFrom(d => d.Title)).ReverseMap();
            CreateMap<Views.Media.NewCollectionContent, Adapter.Media.NewCollectionContent>().ReverseMap();
        }

        private void ModifyingMedia()
        {
            CreateMap<Views.Media.Book.EditableBook, Views.Media.Book.Book>().ReverseMap();
            CreateMap<Views.Media.Music.EditableMusic, Views.Media.Music.Music>().ReverseMap();
            CreateMap<Views.Media.Movie.EditableMovie, Views.Media.Movie.Movies>().ReverseMap();

            CreateMap<Views.Media.Book.EditableBook, Adapter.Media.Book.BookModificationRequest>()
                .ForMember(d => d.Identification, o => o.MapFrom(s => s.ISBN)).ReverseMap();
            CreateMap<Views.Media.Music.EditableMusic, Adapter.Media.Music.MusicModificationRequest>().ReverseMap();
            CreateMap<Views.Media.Movie.EditableMovie, Adapter.Media.Movies.MovieModificationRequest>().ReverseMap();
        }
    }
}
