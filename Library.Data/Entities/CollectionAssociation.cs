using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class CollectionAssociation
{
    public int CollectionAssociationId { get; set; }

    public int CollectionId { get; set; }

    public string MediaType { get; set; } = null!;

    public int MediaId { get; set; }


    public Collection Collection { get; set; } 
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Music> Music { get; set; } = new List<Music>();
    public ICollection<Movie> Movies { get; set; } = new List<Movie>();

}
