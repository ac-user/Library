using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class SubCollectionAssociation
{
    public int SubCollectionAssociationId { get; set; }

    public int CollectionId { get; set; }

    public int SubCollectionId { get; set; }

    public Collection Collection { get; set; }

}
