using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.DomainService
{
    public interface IDomainService
    {
        IEnumerable<DomainDTO> GetAllDomains();
        IEnumerable<DomainDTO> GetAllDomainsWithSubdomains();
        DomainDTO GetById(Guid? id);
        DomainDTO GetByIdWithSubdomain(Guid? id);
        void Create(DomainDTO model);
        void Update(DomainDTO model);
        bool Save();
        void Delete(Guid? id);
    }
}
