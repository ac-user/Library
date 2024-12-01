using Library.Models.Media;

namespace Library.Services.Models
{
    public class CollectionContentAssociation
    {
        public MediaContentType MediaType {get;set;}

        public int MediaId { get; set; }

    }
}
