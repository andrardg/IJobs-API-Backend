using IJobs.Models;
using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public interface ICompanyService
    {
        CompanyResponseDTO Authenticate(CompanyRequestDTO model);
        void Register(CompanyRequestDTO model);
        void Create(Company company);
        void Update(Guid? id, CompanyRequestDTO company);
        IEnumerable<CompanyResponseDTO> GetAllCompanies(); 
        IEnumerable<CompanyResponseDTO> GetByEmail(string email);
        IEnumerable<CompanyResponseDTO> GetByName(string Name);
        IEnumerable<CompanyResponseDTO> GetByNameIncludingJobs(string Name);
        CompanyResponseDTO GetById(Guid? id);
        Task<CompanyResponseDTO> GetByIdAsinc(Guid? id);
        Task<bool> SaveAsync();
        bool Save();
        void Delete(Guid? id);
    }
}
