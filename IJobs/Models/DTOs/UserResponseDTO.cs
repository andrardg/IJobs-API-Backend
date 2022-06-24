using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Residence { get; set; }
        public string Occupation { get; set; }
        public string Studies { get; set; }
        public string CV { get; set; }
        public string Photo { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<Invite> Invites { get; set; }
        public ICollection<Job> Jobs { get; set; }
    }
}
