using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class SubdomainDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid DomainId { get; set; }
        public Domain Domain { get; set; }
        public ICollection<Tutorial> Tutorials { get; set; }
    }
}
