using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Application : Base.BaseEntity
    {
        public Guid JobId { get; set; }
        public Job Job { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string CV { get; set; }
        public string Status { get; set; } //pending,  rejected, interview setup
        public ICollection<Interview> Interviews { get; set; }
    }
}
