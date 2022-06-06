using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.DomainRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services.DomainService
{
    public class DomainService : IDomainService
    {
        public IDomainRepository _domainRepository;
        public projectContext _context;
        private readonly IMapper _mapper;
        public DomainService(IDomainRepository domainRepository, IMapper mapper, projectContext context)
        {
            _domainRepository = domainRepository;
            _context = context;
            _mapper = mapper;
        }
        public IEnumerable<DomainDTO> GetAllDomains()
        {
            var results = _domainRepository.GetAll();
            var dtos = new List<DomainDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<DomainDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<DomainDTO> GetAllDomainsWithSubdomains()
        {
            var results = _domainRepository.GetAllWithSubdomains();
            var dtos = new List<DomainDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<DomainDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public DomainDTO GetById(Guid? id)
        {
            var result = _domainRepository.GetById(id);
            var domain = _mapper.Map<DomainDTO>(result);
            return domain;
        }
        public DomainDTO GetByIdWithSubdomain(Guid? id)
        {
            var result = _domainRepository.GetByIdWithSubdomain(id);
            var domain = _mapper.Map<DomainDTO>(result);
            return domain;
        }
        public void Create(DomainDTO model)
        {
            if (_context.Domains.Any(x => x.Name == model.Name))
                throw new Exception("Domain name '" + model.Name + "' already exists");
            var domain = _mapper.Map<Domain>(model);
            domain.Id = Guid.NewGuid();
            domain.DateCreated = DateTime.UtcNow;
            domain.DateModified = DateTime.UtcNow;
            _domainRepository.Create(domain);
            Save();
        }
        public void Delete(Guid? id)
        {
            var domain = _domainRepository.GetById(id);
            _domainRepository.Delete(domain);
        }
        public bool Save()
        {
            try
            {
                return _domainRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public void Update(DomainDTO model)
        {
            if (_context.Domains.Any(x => x.Name == model.Name && x.Id != model.Id))
                throw new Exception("Domain name '" + model.Name + "' already exists.");
            var domain = _mapper.Map<Domain>(model);
            domain.DateModified = DateTime.UtcNow;
            _domainRepository.Update(domain);
        }
    }
}
