using System;
using System.Collections.Generic;

namespace MovieService.Models;

public partial class Producer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public byte[]? Img { get; set; }
<<<<<<< HEAD
=======
    public string? Status { get; set; }
>>>>>>> 960a83c (commit)

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
