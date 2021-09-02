using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovie.Models
{
    public partial class MovieSet
    {
        public MovieSet()
        {
            CommentSets = new HashSet<CommentSet>();
            GenreMovies = new HashSet<GenreMovie>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Image { get; set; }

        public virtual ICollection<CommentSet> CommentSets { get; set; }
        public virtual ICollection<GenreMovie> GenreMovies { get; set; }
    }

    public partial class MovieCreate
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string Image { get; set; }

        public virtual ICollection<CommentSet> CommentSets { get; set; }
        public virtual ICollection<int> GenreIds { get; set; }

        internal MovieSet MapMovieSet()
        {
            return new MovieSet { Id = this.Id, Title = this.Title, Synopsis = this.Synopsis, Image = this.Image };
        }
    }
}