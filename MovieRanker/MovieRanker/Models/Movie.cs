using System;
using System.Collections.Generic;

namespace MovieRanker.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int Duration { get; set; }

    public string Genre { get; set; } = null!;

    public int Year { get; set; }

    public string? Synopsis { get; set; }

    public double? Rating { get; set; }

    public int RatingCount { get; set; } = 0;

    public virtual ICollection<Person> Actors { get; set; } = new List<Person>();

    public virtual ICollection<Person> Directors { get; set; } = new List<Person>();
}
