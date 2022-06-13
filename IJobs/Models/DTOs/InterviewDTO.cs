using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class InterviewDTO
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ApplicationId { get; set; }
        public Application Application { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public bool IsOnline { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public bool ResponseUser { get; set; }
        [Required]
        public bool ResponseCompany { get; set; }
    }
}
