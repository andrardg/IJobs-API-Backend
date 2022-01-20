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
        //ModelResultDTO GetDataMappedByTitle(string title);
        CompanyResponseDTO Authenticate(CompanyRequestDTO model);
        //get all
        IEnumerable<CompanyResponseDTO> GetAllCompanies();
        //get by name
        IEnumerable<CompanyResponseDTO> GetByTitle(string title);
        //get by name including jobs
        IEnumerable<CompanyResponseDTO> GetByTitleIncludingJobs(string title);
        void Create(CompanyRequestDTO company);
        CompanyResponseDTO FindById(Guid? id);
        Task<CompanyResponseDTO> FindByIdAsinc(Guid? id);
        void Update(CompanyRequestDTO company);
        Task<bool> SaveAsync();
        bool Save();
        void Delete(Guid? id);
    }
}
