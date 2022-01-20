using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IJobs.Models
{
    public class Company : Base.BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool verifiedAccount { get; set; }
        public ICollection<Job> Jobs { get; set; } //one to many between job and company
    }
}
