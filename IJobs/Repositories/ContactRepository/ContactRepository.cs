using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.ContactRepository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(projectContext context) : base(context)
        {

        }
        public List<Contact> GetAll()
        {
            return _table.ToList();
        }
        public new bool Delete(Contact entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Contact entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
