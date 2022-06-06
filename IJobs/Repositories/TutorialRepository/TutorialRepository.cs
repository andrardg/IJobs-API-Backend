using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.TutorialRepository
{
    public class TutorialRepository : GenericRepository<Tutorial>, ITutorialRepository
    {
        public TutorialRepository(projectContext context) : base(context)
        {

        }
        public List<Tutorial> GetAll()
        {
            return _table.ToList();
        }
        public List<Tutorial> GetAllBySubdomainId(Guid? id)
        {
            return _table.Where(x => x.SubdomainId == id).ToList();
        }

        public List<Tutorial> GetAllBySubdomainIdJoin(Guid? id)
        {
            var result = from tut in _table
                            join subd in _context.Subdomains on tut.SubdomainId equals subd.Id
                            where tut.SubdomainId == id
                            select new Tutorial
                            {
                                Id = tut.Id,
                                Link = tut.Link,
                                SubdomainId = tut.SubdomainId,
                                Subdomain = ((Subdomain)(from sub in _context.Subdomains where sub.Id == tut.SubdomainId select sub))
                            };
            return result.ToList();
        }

        public Tutorial GetByIdWithJoin(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).FirstOrDefault();
            result.Subdomain = (Subdomain)(from subd in _context.Subdomains
                               where subd.Id == result.SubdomainId
                               select subd);
            return result;
        }
        public new bool Delete(Tutorial entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Tutorial entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
