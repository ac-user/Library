using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class Collection
{
    public int CollectionId { get; set; }

    public int AccountId { get; set; }
    public string Title { get; set; } = null!;

    public ICollection<SubCollectionAssociation> SubCollectionAssociations { get; set; } = new List<SubCollectionAssociation>();
    public ICollection<CollectionAssociation> CollectionAssociations { get; set; } = new List<CollectionAssociation>();
}
