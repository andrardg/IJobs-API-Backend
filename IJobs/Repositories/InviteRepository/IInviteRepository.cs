using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.InviteRepository
{
    public interface IInviteRepository : IGenericRepository<Invite>
    {
        List<Invite> GetAll();
        List<Invite> GetAllJoin();
        Invite GetById(Guid? id);
        Invite GetByIdJoin(Guid? id);
    }
}
