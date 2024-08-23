using System;
using System.Collections.Generic;

namespace MovieService.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? Name { get; set; }
<<<<<<< HEAD
=======
    public string? Status { get; set; }
>>>>>>> 960a83c (commit)

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
