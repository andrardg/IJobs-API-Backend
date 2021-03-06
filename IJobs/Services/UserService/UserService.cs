using AutoMapper;
using IJobs.Data;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Repositories.UserRepository;
using IJobs.Utilities.JWTUtils;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace IJobs.Services
{
    public class UserService: IUserService
    {
        public projectContext _context;
        private IJWTUtils<User> _ijwtUtils;
        private readonly IMapper _mapper;
        public IUserRepository _userRepository;
        public UserService(projectContext context, IJWTUtils<User> ijwtUtils, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _ijwtUtils = ijwtUtils;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public AccountDTO Authenticate(AccountDTO model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                //throw new Exception("Email or password is incorrect");
                return new AccountDTO();
            }
            //auth successful
            var response = _mapper.Map<AccountDTO>(user);
            //JWT generation (JSON WEB TOKEN)
            response.Token = _ijwtUtils.GenerateJWTToken(user, user.Role);
            Console.Write(response.Token);
            return response;
        }
        public void Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.DateCreated = DateTime.UtcNow;
            user.DateModified = DateTime.UtcNow;
            _userRepository.Create(user);
            Save();
        }
        public void Register(AccountDTO model)
        {
            if (_context.Companies.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");

            var user = _mapper.Map<User>(model);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            user.Role = Role.User;
            Create(user);
            Save();
        }
        public void Update(Guid? id, UserRequestDTO model)
        {
            if (_context.Companies.Any(x => x.Email == model.Email && x.Id != id))
                throw new Exception("Email '" + model.Email + "' is already taken");
            //if( _context.Users.Where(x => x.Email == model.Email && x.Id != id).Count()>0)

            if (_context.Users.Any(x => x.Email == model.Email && x.Id != id))
                throw new Exception("Email '" + model.Email + "' is already taken");

            var user = _mapper.Map<User>(model); // the new one
            user.Id = (Guid)id;
            // hash password if it was entered
            if(model.Password != model.oldPasswordHash)//if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            else
                user.PasswordHash = model.oldPasswordHash;

            // copy model to user and save
            user.DateModified = DateTime.UtcNow;
            _userRepository.Update(user);

        }
        public IEnumerable<UserResponseDTO> GetAllUsers()
        {
            var results = _userRepository.GetAllWithJoin();
            var dtos = new List<UserResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<UserResponseDTO>(result);
                //var response = new UserResponseDTO(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public UserResponseDTO GetById(Guid? id)
        {
            var user = _userRepository.GetByIdJoin(id);
            if (user == null) 
                throw new KeyNotFoundException("User not found");
            var response = _mapper.Map<UserResponseDTO>(user);
            //var response = new UserResponseDTO(user);
            return response;
        }
        public async Task<UserResponseDTO> GetByIdAsinc(Guid? id)
        {
            var user = await _userRepository.GetByIdAsinc(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
            var response = _mapper.Map<UserResponseDTO>(user);
            return response;
        }
        public IEnumerable<UserResponseDTO> GetByName(string Name)
        {
            var results = _userRepository.GetByName(Name);
            var dtos = new List<UserResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<UserResponseDTO>(result);
                //var response = new UserResponseDTO(result);
                dtos.Add(response);
            }
            return dtos;
        }
        public bool Save()
        {
            try
            {
                return _userRepository.Save() != false;
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
                return await _userRepository.SaveAsync() != false;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public void Delete(Guid? id)
        {
            var user = _userRepository.GetById(id);
            _userRepository.Delete(user);
        }
    }
}
