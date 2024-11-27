using System;
using System.Collections.Generic;

namespace Library.Data.Entities;

public partial class Movie
{
    public int MovieId { get; set; }

    public int AccountId { get; set; }
    public string? Title { get; set; }

    public string? Series { get; set; }

    public string? Writer { get; set; }

    public DateTime? DateReleased { get; set; }

    public string? Summary { get; set; }
}
