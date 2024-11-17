using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class Collection
{
    public int CollectionId { get; set; }

    public string Title { get; set; } = null!;
}
