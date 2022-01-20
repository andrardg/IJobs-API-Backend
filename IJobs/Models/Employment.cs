using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Employment : Base.BaseEntity
    {
        public string Status { get; set; }
        public User User { get; set; } //one to one between user and employment
        public Guid UserId { get; set; }
    }
}
