using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; }

        public ICollection<PostCategory> PostCategory { get; set; }
    }
}
