using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Models.DTOs
{
    public class CompanyResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public Role Role { get; set; }
        public bool verifiedAccount { get; set; }
        public string Token { get; set; }
        public ICollection<Job> Jobs { get; set; } //one to many between job and company
        public CompanyResponseDTO() { }
        public CompanyResponseDTO(Company company, string token)
        {
            Id = company.Id;
            Name = company.Name;
            Email = company.Email;
            PasswordHash = company.PasswordHash;
            Address = company.Address;
            Description = company.Description;
            Photo = company.Photo;
            Role = company.Role;
            verifiedAccount = company.verifiedAccount;
            Token = token;
            Jobs = company.Jobs;
        }
    }
}
