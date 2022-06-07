using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.ApplicationRepository
{
    public interface IApplicationRepository : IGenericRepository<Application>
    {
        List<Application> GetAll();
        List<Application> GetAllJoin();
        Application GetById(Guid? id);
        Application GetByIdJoin(Guid? id);
    }
}
