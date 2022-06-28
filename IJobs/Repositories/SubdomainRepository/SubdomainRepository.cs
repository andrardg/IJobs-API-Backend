using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.SubdomainRepository
{
    public class SubdomainRepository : GenericRepository<Subdomain>, ISubdomainRepository
    {
        public SubdomainRepository(projectContext context) : base(context)
        {
        }
        public List<Subdomain> GetAll()
        {
            return _table.ToList();
        }
        public List<Subdomain> GetAllByDomainId(Guid? id)
        {
            return _table.Where(x => x.DomainId == id).OrderBy(x => x.Name).ToList();
        }

        public List<Subdomain> GetAllByDomainIdJoin(Guid? id)
        {
            var result = from subd in _table
                         join tut in _context.Tutorials on subd.Id equals tut.SubdomainId
                         where subd.DomainId == id
                         select new Subdomain
                         {
                             Name = subd.Name,
                             DomainId = subd.DomainId,
                             Domain = ((Domain)(from dom in _context.Domains where dom.Id == subd.DomainId select dom)),
                             Tutorials = ((ICollection<Tutorial>)(from tut in _context.Tutorials where tut.SubdomainId == subd.Id select tut)),
                             Jobs = ((ICollection<Job>)(from job in _context.Jobs where job.SubdomainId == subd.Id select job))
                         };
            return result.ToList();
        }

        public Subdomain GetByIdWithJoin(Guid? id)
        {
            var subd = _table.Where(x => x.Id == id).FirstOrDefault();
            subd.Tutorials = (ICollection<Tutorial>)(from tut in _context.Tutorials
                             where tut.SubdomainId == id
                             select tut);
            subd.Jobs = (ICollection<Job>)(from job in _context.Jobs
                                                where job.SubdomainId == id
                                                select job);
            return subd;
        }
        public new bool Delete(Subdomain entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Subdomain entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
