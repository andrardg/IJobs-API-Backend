using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.InterviewRepository
{
    public class InterviewRepository : GenericRepository<Interview>, IInterviewRepository
    {
        public InterviewRepository(projectContext context) : base(context)
        {
        }
        public List<Interview> GetAll()
        {
            return _table.ToList();
        }

        public List<Interview> GetAllJoin()
        {
            var result = from interview in _table
                         join app in _context.Applications on interview.ApplicationId equals app.Id
                         join job in _context.Jobs on app.JobId equals job.Id
                         join user in _context.Users on app.UserId equals user.Id
                         select new Interview
                         {
                             Id = interview.Id,
                             ApplicationId = interview.ApplicationId,
                             Application = new Application
                             {
                                 Id = app.Id,
                                 JobId = app.JobId,
                                 Job = job,
                                 UserId = app.UserId,
                                 User = user,
                                 CV = app.CV,
                                 Status = app.Status
                             },
                             Date = interview.Date,
                             IsOnline = interview.IsOnline,
                             Location = interview.Location,
                             ResponseUser = interview.ResponseUser,
                             ResponseCompany = interview.ResponseCompany
                         };
            return result.ToList();
        }

        public Interview GetByIdJoin(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).FirstOrDefault();
            result.Application = _context.Applications.Where(x => x.Id == result.ApplicationId).FirstOrDefault();
            return result;
        }
        public new bool Delete(Interview entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Interview entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
