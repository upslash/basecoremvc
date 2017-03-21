using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Clayton.Models
{
    public class EnhancementProgress
    {
        public int EnhancementProgressId { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public int EnhancementId { get; set; }
        public Enhancement Enhancement { get; set; }
    }
}
