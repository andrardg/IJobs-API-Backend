using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.CompanyRepository
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(projectContext context) : base(context)
        {
        }
        public List<Company> GetByTitle(string title)
        {
            return _table.Where(s => s.Name!.ToLower().Contains(title.ToLower())).ToList();
        }
        public List<Company> GetByTitleIncludingJobs(string title)
        {
            var result = _table.Where(s => s.Name!.ToLower().Contains(title.ToLower()));
            return result.Include(x => x.Jobs).ToList();
        }
        public List<Company> GetAllWithInclude()
        {
            return _table.Include(x => x.Jobs).ToList();
        }
        public List<Company> GetAllWithJoin()
        {
            var result = _table.Join(_context.Jobs, c => c.Id, j => j.CompanyId,
                (c, j) => new { c, j }).Select(obj => obj.c);
            return result.ToList();
        }
        public new bool Delete(Company entity)
        {
            //var entity2 = _context.Companies.FirstOrDefault(u => u.Id == entity.Id);
            _context.Remove(entity);
            //_context.SaveChanges();
            return true;
            //_table.Remove(entity);
            //SaveAsync();
        }
        public new bool Update(Company entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
