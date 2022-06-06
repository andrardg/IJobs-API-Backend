using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.SubdomainRepository
{
    public interface ISubdomainRepository : IGenericRepository<Subdomain>
    {
        List<Subdomain> GetAll();
        List<Subdomain> GetAllByDomainId(Guid? id);
        List<Subdomain> GetAllByDomainIdJoin(Guid? id);
        Subdomain GetByIdWithJoin(Guid? id);
    }
}
