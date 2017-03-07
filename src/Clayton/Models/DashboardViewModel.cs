﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
