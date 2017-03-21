using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class CloseViewModel
    {
        public bool ShouldClose { get; set; }
        public bool FetchData { get; set; }
        public bool ReloadPage { get; set; }
        public string Destination { get; set; }
        public string Target { get; set; }
        public string OnSuccess { get; set; }
    }
}
