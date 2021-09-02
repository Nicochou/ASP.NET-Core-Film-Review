using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovieClient.Models
{
    public partial class UserSet
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Admin { get; set; }

        public virtual ICollection<CommentSet> CommentSets { get; set; }
    }
}
