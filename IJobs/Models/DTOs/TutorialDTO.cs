using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class TutorialDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public Guid SubdomainId { get; set; }
        public Subdomain Subdomain { get; set; }
    }
}
