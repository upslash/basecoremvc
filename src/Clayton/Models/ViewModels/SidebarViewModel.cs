using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models.ViewModels
{
    public class SidebarViewModel
    {
        public IEnumerable<Post> RecentPosts { get; set; }
        public IEnumerable<Category> ActiveCategories { get; set; }
    }
}
