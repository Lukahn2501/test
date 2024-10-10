using System;
using System.Collections.Generic;

namespace MovieRanker.Models;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

    public virtual ICollection<Movie> MoviesNavigation { get; set; } = new List<Movie>();
}
