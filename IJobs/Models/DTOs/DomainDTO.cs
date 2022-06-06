using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class DomainDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Subdomain> Subdomains { get; set; }
    }
}
