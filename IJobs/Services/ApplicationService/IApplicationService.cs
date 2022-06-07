using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.ApplicationService
{
    public interface IApplicationService
    {
        IEnumerable<ApplicationDTO> GetAllApplications();
        IEnumerable<ApplicationDTO> GetAllApplicationsJoin();
        ApplicationDTO GetById(Guid? id);
        ApplicationDTO GetByIdWithJoin(Guid? id);
        void Create(ApplicationDTO model);
        void Update(ApplicationDTO model);
        bool Save();
        void Delete(Guid? id);
    }
}
