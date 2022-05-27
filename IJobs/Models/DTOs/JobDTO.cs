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
        public bool Open { get; set; }
        public Guid Id { get; set; }
        [Required]
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
