using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models.Media
{
    public class CollectionCreationRequest
    {
        /// <summary>
        /// Collection title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Collection contents
        /// </summary>
        public List<NewCollectionContent> NewCollectionContents { get; set; }
    }

    public class NewCollectionContent
    {
        /// <summary>
        /// Unique identifier for the content
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// What kind of content is it? Null if collection
        /// </summary>
        public MediaContentType? MediaType { get; set; }
    }
}
