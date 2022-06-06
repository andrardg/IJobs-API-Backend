using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IJobs.Models
{
    public class User : Base.BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [StringLength(200)]
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Residence { get; set; }
        public string Occupation { get; set; }
        public string Studies { get; set; }
        public string CV { get; set; }
        public string Photo { get; set; }
        public Role Role { get; set; }
        public ICollection<Application> Applications { get; set; }
    }
}
