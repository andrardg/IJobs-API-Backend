using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
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
        public Job GetByIdWithJoin(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).FirstOrDefault();
            result.Subdomain = (from subd in _context.Subdomains where subd.Id == result.SubdomainId
                                select new Subdomain
                                {
                                    Id = subd.Id,
                                    Name = subd.Name,
                                    DomainId = subd.DomainId,
                                    Domain = (Domain)(from domain in _context.Domains where domain.Id == subd.DomainId select domain).FirstOrDefault()
                                }).FirstOrDefault();
            result.Company = (from company in _context.Companies where company.Id == result.CompanyId
                              select new Company
                              {
                                  Name = company.Name,
                                  Email = company.Email,
                                  PasswordHash = company.PasswordHash,
                                  Address = company.Address,
                                  Description = company.Description,
                                  Role = company.Role,
                                  Photo = company.Photo,
                                  verifiedAccount = company.verifiedAccount,
                                  Id = company.Id
                              }).FirstOrDefault();
            result.User = (from user in _context.Users
                              where user.Id == result.UserId
                              select user).FirstOrDefault();
            return result;
        }
        public List<Job> GetAll()
        {
            return _table.ToList();
        }
        public List<Job> GetAllJobsWithJoin()
        {
            var result = from job in _table
                          join company in _context.Companies on job.CompanyId equals company.Id
                          join subdomain  in _context.Subdomains on job.SubdomainId equals subdomain.Id
                          where job.WorkType == false
                          select new Job
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
                              Company = company,
                              SubdomainId = job.SubdomainId,
                              Subdomain = subdomain
                          };
            return result.ToList();
        }
        public List<Job> GetAllWorkWithJoin()
        {
            var result = from job in _table
                         join subdomain in _context.Subdomains on job.SubdomainId equals subdomain.Id
                         where job.WorkType == true
                         select new Job
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
                             Company = (Company)(from company in _context.Companies where job.CompanyId == company.Id select company).FirstOrDefault(),
                             UserId = job.UserId,
                             User = (User)(from user in _context.Users where job.UserId == user.Id select user).FirstOrDefault(),
                             SubdomainId = job.SubdomainId,
                             Subdomain = subdomain
                         };

            return result.ToList();
        }
        public List<Job> GetByJobTitle(string JobTitle)
        {
            return _table.Where(s => s.JobTitle!.ToLower().Contains(JobTitle.ToLower())).ToList();

        }
        public List<Job> GetByJobTitleWithCompany(string JobTitle){
            var result = from job in _table
                         join subdomain in _context.Subdomains on job.SubdomainId equals subdomain.Id
                         where job.JobTitle.ToLower().Contains(JobTitle.ToLower())
                          select new Job
                          {
                              Id = job.Id,
                              JobTitle = job.JobTitle,
                              Description = job.Description,
                              Salary = job.Salary,
                              JobType = job.JobType,
                              Experience = job.Experience,
                              Open = job.Open,
                              WorkType = job.WorkType,
                              CompanyId = job.CompanyId,
                              Company = (Company)(from company in _context.Companies where job.CompanyId == company.Id select company).FirstOrDefault(),
                              SubdomainId = job.SubdomainId,
                              Subdomain = subdomain,
                              UserId = job.UserId,
                              User = (User)(from user in _context.Users where job.UserId == user.Id select user).FirstOrDefault(),
                          };
            return result.ToList();
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
        public new bool Delete(Job entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(Job entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
