using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.SubdomainService
{
    public interface ISubdomainService
    {
        IEnumerable<SubdomainDTO> GetAllSubdomains();
        IEnumerable<SubdomainDTO> GetAllSubdomainsByDomainId(Guid? id);
        IEnumerable<SubdomainDTO> GetAllSubdomainsByDomainIdJoin(Guid? id);
        SubdomainDTO GetById(Guid? id);
        SubdomainDTO GetByIdWithJoin(Guid? id);
        void Create(SubdomainDTO model);
        void Update(SubdomainDTO model);
        bool Save();
        void Delete(Guid? id);
    }
}
