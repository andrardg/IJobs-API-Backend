using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.JobRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public class JobService : IJobService
    {
        public IJobRepository _jobRepository;
        public projectContext _context;
        private readonly IMapper _mapper;

        public JobService(IJobRepository jobRepository, IMapper mapper, projectContext context)
        {
            _jobRepository = jobRepository;
            _context = context;
            _mapper = mapper;
        }
        public void Create(JobDTO model)
        {
            var job = _mapper.Map<Job>(model);
            job.Id = Guid.NewGuid();
            job.DateCreated = DateTime.UtcNow;
            job.DateModified = DateTime.UtcNow;
            _jobRepository.Create(job);
            Save();
        }

        public void Delete(Guid? id)
        {
            var job = _jobRepository.GetById(id);
            _jobRepository.Delete(job);
        }

        public JobDTO GetById(Guid? id)
        {
            var result = _jobRepository.GetById(id);
            var job = _mapper.Map<JobDTO>(result);
            return job;
        }

        public IEnumerable<JobDTO> GetAllJobs()
        {
            var results = _jobRepository.GetAll();
            var dtos = new List<JobDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<JobDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
            public IEnumerable<JobDTO> GetAllJobsWithCompany()
        {
            var results = _jobRepository.GetAllWithJoin();
            var dtos = new List<JobDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<JobDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<JobDTO> GetByJobTitle(string title)
        {
            var results = _jobRepository.GetByJobTitle(title);
            var dtos = new List<JobDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<JobDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public bool Save()
        {
            try
            {
                return _jobRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public void Update(JobDTO model)
        {
            var job = _mapper.Map<Job>(model);
            _jobRepository.Update(job);
        }
    }
}
