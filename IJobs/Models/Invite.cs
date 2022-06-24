using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Invite : Base.BaseEntity
    {
        public Guid JobId { get; set; }
        public Job Job { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
