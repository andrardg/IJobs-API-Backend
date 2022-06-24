using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.InviteService
{
    public interface IInviteService
    {
        IEnumerable<InviteDTO> GetAllInvites();
        IEnumerable<InviteDTO> GetAllInvitesJoin();
        InviteDTO GetById(Guid? id);
        InviteDTO GetByIdWithJoin(Guid? id);
        void Create(InviteDTO model);
        bool Save();
        void Delete(Guid? id);
    }
}
