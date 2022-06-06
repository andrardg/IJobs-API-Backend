using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class ApplicationDTO
    {
        public Guid Id { get; set; }
        [Required]
        public Guid JobId { get; set; }
        public Job Job { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
        [Required]
        public string CV { get; set; }
        [Required]
        public string Status { get; set; } //pending,  rejected, interview setup
        public ICollection<Interview> Interviews { get; set; }
    }
}
