using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class InviteDTO
    {
        public Guid Id { get; set; }
        [Required]
        public Guid JobId { get; set; }
        public Job Job { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
