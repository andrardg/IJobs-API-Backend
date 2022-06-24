using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class JobDTO
    {
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string JobType { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Address { get; set; }
        public bool Open { get; set; }
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        [Required]
        public Guid SubdomainId { get; set; }
        public Subdomain Subdomain { get; set; }
        public ICollection<Application> Applications { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
