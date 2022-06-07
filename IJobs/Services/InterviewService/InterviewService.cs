using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.InterviewRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.InterviewService
{
    public class InterviewService :IInterviewService
    {
        public IInterviewRepository _interviewRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public InterviewService(IInterviewRepository interviewRepository, IMapper mapper, projectContext context)
        {
            _interviewRepository = interviewRepository;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<InterviewDTO> GetAllInterviews()
        {
            var results = _interviewRepository.GetAll();
            var dtos = new List<InterviewDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<InterviewDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public IEnumerable<InterviewDTO> GetAllInterviewsJoin()
        {
            var results = _interviewRepository.GetAllJoin();
            var dtos = new List<InterviewDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<InterviewDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public InterviewDTO GetById(Guid? id)
        {
            var result = _interviewRepository.GetById(id);
            var interview = _mapper.Map<InterviewDTO>(result);
            return interview;
        }

        public InterviewDTO GetByIdWithJoin(Guid? id)
        {
            var result = _interviewRepository.GetByIdJoin(id);
            var interview = _mapper.Map<InterviewDTO>(result);
            return interview;
        }

        public void Create(InterviewDTO model)
        {
            var interview = _mapper.Map<Interview>(model);
            interview.Id = Guid.NewGuid();
            interview.DateCreated = DateTime.UtcNow;
            interview.DateModified = DateTime.UtcNow;
            _interviewRepository.Create(interview);
            Save();
        }

        public void Update(InterviewDTO model)
        {
            var interview = _mapper.Map<Interview>(model);
            interview.DateModified = DateTime.UtcNow;
            _interviewRepository.Update(interview);
        }

        public bool Save()
        {
            try
            {
                return _interviewRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public void Delete(Guid? id)
        {
            var interview = _interviewRepository.GetById(id);
            _interviewRepository.Delete(interview);
        }
    }
}
