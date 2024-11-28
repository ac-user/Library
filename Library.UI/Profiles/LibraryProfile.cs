using AutoMapper;
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

            CreateMap<Views.Media.MediaCollection, Adapter.Media.Book.Book>().ReverseMap();
            CreateMap<Views.Media.MediaCollection, Adapter.Media.Music.Music>().ReverseMap();
            CreateMap<Views.Media.MediaCollection, Adapter.Media.Movies.Movie>().ReverseMap();


            CreateMap<Views.Media.Book.Book, Adapter.Media.Book.Book>().ReverseMap();
            CreateMap<Views.Media.Music.Music, Adapter.Media.Music.Music>().ReverseMap();
            CreateMap<Views.Media.Movie.Movies, Adapter.Media.Movies.Movie>().ReverseMap();

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

        }

        private void ModifyingMedia()
        {
            CreateMap<Views.Media.Book.Book, Adapter.Media.Book.BookModificationRequest>().ReverseMap();
            CreateMap<Views.Media.Music.Music, Adapter.Media.Music.MusicModificationRequest>().ReverseMap();
            CreateMap<Views.Media.Movie.Movies, Adapter.Media.Movies.MovieModificationRequest>().ReverseMap();

        }
    }
}
