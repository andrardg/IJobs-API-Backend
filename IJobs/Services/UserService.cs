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
        public UserResponseDTO Authenticate(UserRequestDTO model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == model.Email);
            //validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            {
                throw new Exception("Username or password is incorrect");
                //return null;
            }
            //auth successful
            var response = _mapper.Map<UserResponseDTO>(user);
            //JWT generation (JSON WEB TOKEN)
            response.Token = _ijwtUtils.GenerateJWTToken(user);
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
        public void Register(UserRequestDTO model)
        {
            // validate
            if (_context.Users.Any(x => x.Email == model.Email))
                throw new Exception("Email '" + model.Email + "' is already taken");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            Create(user);
        }
        public void ValidateUpdate(Guid? id, string email)
        {
            var emailBefore = GetById(id).Email;
            var newEmailExists = _userRepository.EmailExists(email);

            // validate
            if (emailBefore != email && newEmailExists != null)
                throw new Exception("Email '" + email + "' is already taken");

        }
        public void Update(Guid? id, UserRequestDTO model)
        {
            
            //ValidateUpdate(id, model.Email);

            var user = _mapper.Map<User>(model); // the new one
            user.Id = (Guid)id;

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _userRepository.Update(user);
        }
        public IEnumerable<UserResponseDTO> GetAllUsers()
        {
            var results = _userRepository.GetAllWithEmploymentInclude();
            //return (IEnumerable<User>)results;
            
            var dtos = new List<UserResponseDTO>();
            foreach (var result in results)
            {
                var response = _mapper.Map<UserResponseDTO>(result);
                dtos.Add(response);
            }
            return dtos;
        }

        

        public UserResponseDTO GetById(Guid? id)
        {
            var user = _userRepository.FindById(id);
            if (user == null) 
                throw new KeyNotFoundException("User not found");
            var response = _mapper.Map<UserResponseDTO>(user);
            return response;
        }

        public async Task<UserResponseDTO> FindByIdAsinc(Guid? id)
        {
            var user = await _userRepository.FindByIdAsinc(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");

            var response = _mapper.Map<UserResponseDTO>(user);
            return response;
            //return await _table.FirstOrDefaultAsync(x => x.Id.Equals(id));
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
            var user = _userRepository.FindById(id);
            _userRepository.Delete(user);
        }
    }
}
