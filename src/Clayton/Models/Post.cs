using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Createdate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }

        public ICollection<PostCategory> PostCategory { get; set; }
    }
}
