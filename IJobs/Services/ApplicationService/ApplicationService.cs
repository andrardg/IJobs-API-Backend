using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.ApplicationRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.ApplicationService
{
    public class ApplicationService : IApplicationService
    {
        public IApplicationRepository _applicationRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public ApplicationService(IApplicationRepository applicationRepository, IMapper mapper, projectContext context)
        {
            _applicationRepository = applicationRepository;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ApplicationDTO> GetAllApplications()
        {
            var results = _applicationRepository.GetAll();
            var dtos = new List<ApplicationDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<ApplicationDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public IEnumerable<ApplicationDTO> GetAllApplicationsJoin()
        {
            var results = _applicationRepository.GetAllJoin();
            var dtos = new List<ApplicationDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<ApplicationDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public ApplicationDTO GetById(Guid? id)
        {
            var result = _applicationRepository.GetById(id);
            var app = _mapper.Map<ApplicationDTO>(result);
            return app;
        }

        public ApplicationDTO GetByIdWithJoin(Guid? id)
        {
            var result = _applicationRepository.GetByIdJoin(id);
            var app = _mapper.Map<ApplicationDTO>(result);
            return app;
        }

        public void Create(ApplicationDTO model)
        {
            var app = _mapper.Map<Application>(model);
            app.Id = Guid.NewGuid();
            app.DateCreated = DateTime.UtcNow;
            app.DateModified = DateTime.UtcNow;
            _applicationRepository.Create(app);
            Save();
        }

        public void Update(ApplicationDTO model)
        {
            var app = _mapper.Map<Application>(model);
            app.DateModified = DateTime.UtcNow;
            _applicationRepository.Update(app);
        }

        public bool Save()
        {
            try
            {
                return _applicationRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public void Delete(Guid? id)
        {
            var app = _applicationRepository.GetById(id);
            _applicationRepository.Delete(app);
        }
    }
}
