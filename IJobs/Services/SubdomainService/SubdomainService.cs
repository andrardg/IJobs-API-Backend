using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.SubdomainRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.SubdomainService
{
    public class SubdomainService : ISubdomainService
    {
        public ISubdomainRepository _subdomainRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public SubdomainService(ISubdomainRepository subdomainRepository, IMapper mapper, projectContext context)
        {
            _subdomainRepository = subdomainRepository;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<SubdomainDTO> GetAllSubdomains()
        {
            var results = _subdomainRepository.GetAll();
            var dtos = new List<SubdomainDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<SubdomainDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<SubdomainDTO> GetAllSubdomainsByDomainId(Guid? id)
        {
            var results = _subdomainRepository.GetAllByDomainId(id);
            var dtos = new List<SubdomainDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<SubdomainDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<SubdomainDTO> GetAllSubdomainsByDomainIdJoin(Guid? id)
        {
            var results = _subdomainRepository.GetAllByDomainIdJoin(id);
            var dtos = new List<SubdomainDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<SubdomainDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public SubdomainDTO GetById(Guid? id)
        {
            var result = _subdomainRepository.GetById(id);
            var subdomain = _mapper.Map<SubdomainDTO>(result);
            return subdomain;
        }
        public SubdomainDTO GetByIdWithJoin(Guid? id)
        {
            var result = _subdomainRepository.GetByIdWithJoin(id);
            var subdomain = _mapper.Map<SubdomainDTO>(result);
            return subdomain;
        }
        public void Create(SubdomainDTO model)
        {
            if (_context.Subdomains.Any(x => x.Name == model.Name && x.DomainId == model.DomainId))
                throw new Exception("Subdomain name '" + model.Name + "' already exists inside this domain.");
            var subdomain = _mapper.Map<Subdomain>(model);
            subdomain.Id = Guid.NewGuid();
            subdomain.DateCreated = DateTime.UtcNow;
            subdomain.DateModified = DateTime.UtcNow;
            _subdomainRepository.Create(subdomain);
            Save();
        }
        public void Delete(Guid? id)
        {
            var subdomain = _subdomainRepository.GetById(id);
            _subdomainRepository.Delete(subdomain);
        }
        public bool Save()
        {
            try
            {
                return _subdomainRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public void Update(SubdomainDTO model)
        {
            if (_context.Subdomains.Any(x => x.Name == model.Name && x.DomainId == model.DomainId && x.Id != model.Id))
                throw new Exception("Subdomain name '" + model.Name + "' already exists inside this domain.");
            var subdomain = _mapper.Map<Subdomain>(model);
            subdomain.DateModified = DateTime.UtcNow;
            _subdomainRepository.Update(subdomain);
        }
    }
}
