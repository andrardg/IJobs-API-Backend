using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class CompanyRequestDTO
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool verifiedAccount { get; set; }
        public Guid Id { get; set; }
        public ICollection<Job> Jobs { get; set; } //one to many between job and company
    }
}
