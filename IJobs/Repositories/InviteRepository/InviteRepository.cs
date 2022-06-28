using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.InviteRepository
{
    public class InviteRepository : GenericRepository<Invite>, IInviteRepository
    {
        public InviteRepository(projectContext context) : base(context)
        {

        }
        public List<Invite> GetAll()
        {
            return _table.ToList();
        }
        public List<Invite> GetAllJoin()
        {
            var result = from inv in _table
                         join user in _context.Users on inv.UserId equals user.Id
                         join job in _context.Jobs on inv.JobId equals job.Id
                         select new Invite
                         {
                             Id = inv.Id,
                             JobId = inv.JobId,
                             Job = new Job
                             {
                                 Id = job.Id,
                                 JobTitle = job.JobTitle,
                                 Description = job.Description,
                                 Salary = job.Salary,
                                 JobType = job.JobType,
                                 Experience = job.Experience,
                                 Address = job.Address,
                                 Open = job.Open,
                                 WorkType = job.WorkType,
                                 CompanyId = job.CompanyId,
                                 Company = ((Company)(from comp in _context.Companies where job.CompanyId == comp.Id select comp).FirstOrDefault()),
                                 SubdomainId = job.SubdomainId,
                                 Subdomain = ((Subdomain)(from subd in _context.Subdomains where job.SubdomainId == subd.Id select subd).FirstOrDefault()),
                                 UserId = job.UserId,
                                 User = ((User)(from user in _context.Users where job.UserId == user.Id select user).FirstOrDefault()),
                             },
                             UserId = inv.UserId,
                             User = user
                         };
            return result.ToList();
        }
        public Invite GetById(Guid? id)
        {
            return _table.Where(x => x.Id == id).FirstOrDefault();
        }
        public Invite GetByIdJoin(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).FirstOrDefault();
            result.User = (User)_context.Users.Where(user => user.Id == result.UserId).FirstOrDefault();
            result.Job = (Job)_context.Jobs.Where(job => job.Id == result.JobId).FirstOrDefault();
            return result;
        }
        public new bool Delete(Invite entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Invite entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
