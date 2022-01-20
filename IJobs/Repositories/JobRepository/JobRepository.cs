using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using IJobs.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.JobRepository
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(projectContext context) : base(context)
        {

        }
        public List<Job> GetByJobTitle(string JobTitle)
        {
            return _table.Where(s => s.JobTitle!.ToLower().Contains(JobTitle.ToLower())).ToList();
        }

        public List<Job> GetBySalary(int Salary)
        {
            return _table.Where(s => s.Salary!.Equals(Salary)).ToList();
        }

        public List<Job> GetByJobType(string JobType)
        {
            return _table.Where(s => s.JobType!.ToLower().Contains(JobType.ToLower())).ToList();
        }

        public List<Job> GetByExperience(string Experience)
        {
            return _table.Where(s => s.Experience!.ToLower().Contains(Experience.ToLower())).ToList();
        }
        public IGrouping<string, Job> GetAllGroupByLINQ()
        {
            var results = (from m1 in _table
                           group m1 by m1.JobTitle into result
                           orderby result.Key
                           select result).ToList();
            return (IGrouping<string, Job>)results;
        }
    }
}
