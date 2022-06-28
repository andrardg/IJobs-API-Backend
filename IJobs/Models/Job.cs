using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Job : Base.BaseEntity
    {
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int Salary { get; set; }
        public string JobType { get; set; }
        public string Experience { get; set; }
        public string Address { get; set; }
        public bool Open { get; set; }
        public bool WorkType { get; set; } // 0=job 1=service
        public Company Company { get; set; }
        public Guid? CompanyId { get; set; }
        public User User { get; set; }
        public Guid? UserId { get; set; }
        public Guid SubdomainId { get; set; }
        public Subdomain Subdomain { get; set; }
        public ICollection<Application> Applications { get; set; }


        public ICollection<Invite> Invites { get; set; }
    }
}
