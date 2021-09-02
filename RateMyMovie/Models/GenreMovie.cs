using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovie.Models
{
    public partial class GenreMovie
    {
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public virtual GenreSet Genre { get; set; }
        public virtual MovieSet Movie { get; set; }
    }
}
