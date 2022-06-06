using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.TutorialRepository
{
    public interface ITutorialRepository : IGenericRepository<Tutorial>
    {
        List<Tutorial> GetAll();
        List<Tutorial> GetAllBySubdomainId(Guid? id);
        List<Tutorial> GetAllBySubdomainIdJoin(Guid? id);
        Tutorial GetByIdWithJoin(Guid? id);
    }
}
