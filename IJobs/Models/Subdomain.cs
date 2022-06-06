using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Subdomain: Base.BaseEntity
    {
        public string Name { get; set; }
        public Guid DomainId { get; set; }
        public Domain Domain { get; set; }
        public ICollection<Tutorial> Tutorials { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
