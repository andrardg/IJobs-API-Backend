using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.DomainRepository
{
    public class DomainRepository : GenericRepository<Domain>, IDomainRepository
    {
        public DomainRepository(projectContext context) : base(context)
        {

        }
        List<Domain> IDomainRepository.GetAll()
        {
            return _table.ToList();
        }
        public List<Domain> GetAllWithSubdomains()
        {
            return _table.Include(x => x.Subdomains).ToList();
        }
        public Domain GetByIdWithSubdomain(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).Include(x => x.Subdomains).ToList();
            return result.FirstOrDefault();
        }
        public new bool Delete(Domain entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Domain entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
