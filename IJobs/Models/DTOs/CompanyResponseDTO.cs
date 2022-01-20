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
        public string Address { get; set; }
        public string Description { get; set; }
        public bool verifiedAccount { get; set; }
        public string Token { get; set; }
        public CompanyResponseDTO() { }
        public CompanyResponseDTO(Company company, string token)
        {
            Id = company.Id;
            Name = company.Name;
            Email = company.Email;
            Address = company.Address;
            Description = company.Description;
            verifiedAccount = company.verifiedAccount;
            Token = token;
        }
    }
}
