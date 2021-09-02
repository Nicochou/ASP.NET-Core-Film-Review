using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovieClient.Models
{
    public partial class GenreSet
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenreMovie> GenreMovies { get; set; }
    }
}
