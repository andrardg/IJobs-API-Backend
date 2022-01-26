using IJobs.Models.DTOs;
using IJobs.Models;
using IJobs.Repositories.CompanyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IJobs.Services;
using IJobs.Utilities.JWTUtils;
using IJobs.Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace IJobs.Services
{
    public class CompanyService : ICompanyService
    {
        public ICompanyRepository _companyRepository;
        private IJWTUtils<Company> _ijwtUtils; 
        public projectContext _context;
        private readonly IMapper _mapper;
        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, IJWTUtils<Company> ijwtUtils, projectContext context)
        {
            _companyRepository = companyRepository;
            _context = context;
            _ijwtUtils = ijwtUtils;
            _mapper = mapper;
        }

        public CompanyResponseDTO Authenticate(CompanyRequestDTO model)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Email == model.Email);
            if (company == null || BCrypt.Net.BCrypt.Verify(model.PasswordHash, company.PasswordHash))
            {
                return null;
            }
            //JWT generation (JSON WEB TOKEN)
            var jwtToken = _ijwtUtils.GenerateJWTToken(company);
            return new CompanyResponseDTO(company, jwtToken);
        }

        public IEnumerable<CompanyResponseDTO> GetAllCompanies()
        {
            var results = _companyRepository.GetAllWithInclude();
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        public IEnumerable<CompanyResponseDTO> GetByTitle(string title)
        {
            var results = _companyRepository.GetByTitle(title);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<CompanyResponseDTO> GetByTitleIncludingJobs(string title)
        {
            var results = _companyRepository.GetByTitleIncludingJobs(title);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public void Create(CompanyRequestDTO model)
        {
            // validate
            if (_context.Companies.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");

            var company = _mapper.Map<Company>(model);
            company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);
            company.Id = Guid.NewGuid();
            company.verifiedAccount = false;
            company.DateCreated = DateTime.UtcNow;
            company.DateModified = DateTime.UtcNow;

            _companyRepository.Create(company);
            Save();
        }
        public CompanyResponseDTO FindById(Guid? id)
        {
            var result =  _companyRepository.FindById(id);
            var company = _mapper.Map<CompanyResponseDTO>(result);
            return company;
        }

        public async Task<CompanyResponseDTO> FindByIdAsinc(Guid? id)
        {
            var result =  await _companyRepository.FindByIdAsinc(id);
            var company = _mapper.Map<CompanyResponseDTO>(result);
            return company;
        }
        public void Update(CompanyRequestDTO company)
        {
            if (_context.Companies.Any(x => x.Email == company.Email && x.Id != company.Id))
                throw new Exception("Email '" + company.Email + "' is already taken");

            var company2 = _mapper.Map <Company> (company);

            if (_context.Companies.Any(x => x.Id == company2.Id && x.PasswordHash != company2.PasswordHash))
                company2.PasswordHash = BCrypt.Net.BCrypt.HashPassword(company2.PasswordHash);
            _companyRepository.Update(company2);
        }
        public bool Save()
        {
            try
            {
                return _companyRepository.Save() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _companyRepository.SaveAsync() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public void Delete(Guid? id)
        {
            var company = _companyRepository.FindById(id);
            _companyRepository.Delete(company);
        }

    }
}
