using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Tutorial: Base.BaseEntity
    {
        public string Link { get; set; }
        public Guid SubdomainId { get; set; }
        public Subdomain Subdomain { get; set; }
    }
}
