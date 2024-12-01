using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class Music
{
    public int MusicId { get; set; }

    public int AccountId { get; set; }
    public string? Title { get; set; }

    public string? Album { get; set; }
    
    public string? Language { get; set; }

    public string? Writer { get; set; }

    public string? Singer { get; set; }

    public DateTime? DatePublished { get; set; }
    
    public int TimesListenedTo { get; set; }

    public int? Stars { get; set; }

    public string Genre { get; set; }



    public CollectionAssociation CollectionAssociation { get; set; }

}
