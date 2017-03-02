using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        [Required]
        public string[] SelectedCategories { get; set; }
        public List<SelectListItem> CategoryList { get; set; }
    }
}
