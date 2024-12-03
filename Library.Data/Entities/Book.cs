using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class Book
{
    public int BookId { get; set; }

    public int AccountId { get; set; }

    public string? Isbn { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public int? Pages { get; set; }

    public string? Language { get; set; }

    public string? Title { get; set; }

    public string? SubTitle { get; set; }

    public string? Series { get; set; }

    public string? Author { get; set; }

    public string? Artist { get; set; }

    public string? Publisher { get; set; }

    public DateTime? Published { get; set; }

    public int? TimesRead { get; set; }

    public bool Ongoing { get; set; }

    public bool IsActivelyReading { get; set; }

    public int? Stars { get; set; }

    public string Genre { get; set; }



    public ICollection<CollectionAssociation> CollectionAssociations { get; set; } = new List<CollectionAssociation>();
}
