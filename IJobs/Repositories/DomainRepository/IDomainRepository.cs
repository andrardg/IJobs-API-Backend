using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.DomainRepository
{
    public interface IDomainRepository : IGenericRepository<Domain>
    {
        List<Domain> GetAll();
        List<Domain> GetAllWithSubdomains();
        Domain GetByIdWithSubdomain(Guid? id);
    }
}
