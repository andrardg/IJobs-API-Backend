using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.ContactRepository
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        List<Contact> GetAll();
    }
}
