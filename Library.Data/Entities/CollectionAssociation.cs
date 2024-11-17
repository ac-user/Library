using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class CollectionAssociation
{
    public int CollectionAssociationId { get; set; }

    public int CollectionId { get; set; }

    public string MediaType { get; set; } = null!;

    public int MediaId { get; set; }
}
