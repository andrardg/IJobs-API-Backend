using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.CompanyRepository
{
    public interface ICompanyRepository : IGenericRepository<Company>
    {
        Company GetByIdWithJobs(Guid? id);
        List<Company> GetByName(string Name);
        List<Company> GetByEmail(string email);
        List<Company> GetByNameIncludingJobs(string Name);
        List<Company> GetAllWithInclude();
        List<Company> GetAllWithJoin();
        Company EmailExists(string Email);
    }
}
