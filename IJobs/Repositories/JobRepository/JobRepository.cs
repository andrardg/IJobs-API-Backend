﻿using IJobs.Data;
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
        public List<Job> GetAll()
        {
            return _table.ToList();
        }
        public List<Job> GetAllWithJoin()
        {
            var result = _table.Join(_context.Companies, j => j.CompanyId, c => c.Id,
               (j, c) => new {j, c }).Select(obj => obj.j);
            return result.ToList();
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