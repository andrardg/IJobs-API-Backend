using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.InterviewRepository
{
    public interface IInterviewRepository : IGenericRepository<Interview>
    {
        List<Interview> GetAll();
        List<Interview> GetAllJoin();
        Interview GetByIdJoin(Guid? id);
    }
}
