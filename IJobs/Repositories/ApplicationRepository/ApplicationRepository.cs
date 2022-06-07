using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.ApplicationRepository
{
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(projectContext context) : base(context)
        {

        }
        public List<Application> GetAll()
        {
            return _table.ToList();
        }

        public List<Application> GetAllJoin()
        {
            var result = from app in _table
                         join user in _context.Users on app.UserId equals user.Id
                         join job in _context.Jobs on app.JobId equals job.Id
                         select new Application
                         {
                             Id = app.Id,
                             JobId = app.JobId,
                             Job = job,
                             UserId = app.UserId,
                             User = user,
                             CV = app.CV,
                             Status = app.Status,
                             Interviews = ((ICollection<Interview>)(from interview in _context.Interviews where app.Id == interview.ApplicationId select interview))
                         };
            return result.ToList();
        }
        public Application GetById(Guid? id)
        {
            return _table.Where(x => x.Id == id).FirstOrDefault(); ;
        }
        public Application GetByIdJoin(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).FirstOrDefault();
            result.User = (User)_context.Users.Where(user => user.Id == result.UserId).FirstOrDefault();
            result.Job = (Job)_context.Jobs.Where(job => job.Id == result.JobId).FirstOrDefault();
            result.Interviews = _context.Interviews.Where(interview => interview.ApplicationId == result.Id).ToList();
            return result;
        }
        public new bool Delete(Application entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Application entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
