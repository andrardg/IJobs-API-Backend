using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public interface IJobService
    {
        //get all
        IEnumerable<JobDTO> GetAllJobs();
        IEnumerable<JobDTO> GetAllJobsWithCompany();
        //get by name
        IEnumerable<JobDTO> GetByJobTitle(string title);
        JobDTO FindById(Guid? id);
        void Create(JobDTO job);
        void Update(JobDTO job);
        bool Save();
        void Delete(Guid? id);

    }
}
