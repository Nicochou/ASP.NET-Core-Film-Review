using System;
using System.Collections.Generic;

#nullable disable

namespace RateMyMovie.Models
{
    public partial class UserSet
    {
        public UserSet()
        {
            CommentSets = new HashSet<CommentSet>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int Admin { get; set; }

        public virtual ICollection<CommentSet> CommentSets { get; set; }
    }
}
