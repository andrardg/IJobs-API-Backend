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
        public AccountDTO Authenticate(AccountDTO model)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Email == model.Email);
            if (company == null || !BCrypt.Net.BCrypt.Verify(model.Password, company.PasswordHash))
            {
                //throw new Exception("Email or password is incorrect");
                return new AccountDTO();
            }
            //auth successful
            var response = _mapper.Map<AccountDTO>(company);
            //JWT generation (JSON WEB TOKEN)
            response.Token = _ijwtUtils.GenerateJWTToken(company, company.Role);
            return response;
        }
        public void Create(Company company)
        {
            company.Id = Guid.NewGuid();
            company.DateCreated = DateTime.UtcNow;
            company.DateModified = DateTime.UtcNow;
            _companyRepository.Create(company);
            Save();
        }
        public void Register(AccountDTO model)
        {
            if (_context.Companies.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");

            var company = _mapper.Map<Company>(model);
            company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            company.Role = Role.User;
            Create(company);
            Save();
        }
        public void Update(Guid? id, CompanyRequestDTO model)
        {
            if (_context.Companies.Any(x => x.Email == model.Email && x.Id != id))
                throw new Exception("Email '" + model.Email + "' is already taken");
            if (_context.Users.Any(x => x.Email == model.Email && x.Id != id))
                throw new Exception("Email '" + model.Email + "' is already taken");
            var company = _mapper.Map<Company>(model); // the new one
            company.Id = (Guid)id;

            // if the new object has a saved password other than the old hash
            if (!model.Password.Equals(model.oldPasswordHash))
                company.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            else
                company.PasswordHash = model.oldPasswordHash;

            // copy model to company and save
            _companyRepository.Update(company);
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
        public IEnumerable<CompanyResponseDTO> GetByEmail(string email)
        {
            var results = _companyRepository.GetByEmail(email);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<CompanyResponseDTO> GetByName(string Name)
        {
            var results = _companyRepository.GetByName(Name);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public IEnumerable<CompanyResponseDTO> GetByNameIncludingJobs(string Name)
        {
            var results = _companyRepository.GetByNameIncludingJobs(Name);
            var dtos = new List<CompanyResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<CompanyResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public CompanyResponseDTO GetById(Guid? id)
        {
            //var result =  _companyRepository.GetById(id); 
            var result = _companyRepository.GetByIdWithJobs(id);
            if (result == null)
                throw new KeyNotFoundException("Company not found");
            var company = _mapper.Map<CompanyResponseDTO>(result);
            return company;
        }

        public async Task<CompanyResponseDTO> GetByIdAsinc(Guid? id)
        {
            var result =  await _companyRepository.GetByIdAsinc(id);
            if (result == null)
                throw new KeyNotFoundException("Company not found");
            var company = _mapper.Map<CompanyResponseDTO>(result);
            return company;
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
            var company = _companyRepository.GetById(id);
            _companyRepository.Delete(company);
        }

    }
}
