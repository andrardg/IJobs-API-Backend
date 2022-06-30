using IJobs.Models;
using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.ContactService
{
    public interface IContactService
    {
        void Create(ContactDTO contact);
        void Update(ContactDTO model);
        IEnumerable<ContactDTO> GetAll();
        bool Save();
        void Delete(Guid? id);
    }
}
