using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Domain : Base.BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Subdomain> Subdomains { get; set; }
    }
}
