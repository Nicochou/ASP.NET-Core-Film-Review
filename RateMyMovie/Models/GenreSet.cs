using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovie.Models
{
    public partial class GenreSet
    {
        public GenreSet()
        {
            GenreMovies = new HashSet<GenreMovie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<GenreMovie> GenreMovies { get; set; }
    }
}
