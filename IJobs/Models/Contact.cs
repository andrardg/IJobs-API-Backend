using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Contact : Base.BaseEntity
    {
        public string Email { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Resolved { get; set; }

    }
}
