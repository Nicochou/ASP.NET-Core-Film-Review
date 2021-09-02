using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovieClient.Models
{
    public partial class CommentSet
    {
        public int Id { get; set; }
        public short Value { get; set; }
        public long Like { get; set; }
        public long Dislike { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }

        public virtual MovieSet Movie { get; set; }
        public virtual UserSet User { get; set; }
    }
}
