using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class Enhancement
    {
        public int EnhancementId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ICollection<EnhancementProgress> EnhancementProgress { get; set; }
    }
}
