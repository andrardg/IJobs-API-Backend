using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.JobRepository
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Job GetByIdWithJoin(Guid? id);
        List<Job> GetAll();
        List<Job> GetAllJobsWithJoin();
        List<Job> GetAllWorkWithJoin();
        List<Job> GetByJobTitle(string JobTitle);
        List<Job> GetByJobTitleWithCompany(string JobTitle);
        List<Job> GetBySalary(int Salary);
        List<Job> GetByJobType(string JobType);
        List<Job> GetByExperience(string Experience);
        IGrouping<string, Job> GetAllGroupByLINQ();

    }
}
