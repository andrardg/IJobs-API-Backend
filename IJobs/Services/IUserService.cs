using IJobs.Models;
using IJobs.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Services
{
    public interface IUserService
    {
        //autentificare
        UserResponseDTO Authenticate(UserRequestDTO model);
        void Register(UserRequestDTO model);
        void Update(Guid? id, UserRequestDTO model);
        //get all
        IEnumerable<UserResponseDTO> GetAllUsers();
        //get by id
        UserResponseDTO GetById(Guid? id);

        void Create(User user);
        Task<UserResponseDTO> FindByIdAsinc(Guid? id);
        Task<bool> SaveAsync();
        bool Save();
        void Delete(Guid? id);
    }
}
