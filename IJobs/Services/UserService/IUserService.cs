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
        AccountDTO Authenticate(AccountDTO model);
        void Register(AccountDTO model);
        void Create(User user);
        void Update(Guid? id, UserRequestDTO model);
        IEnumerable<UserResponseDTO> GetAllUsers();
        UserResponseDTO GetById(Guid? id);
        Task<UserResponseDTO> GetByIdAsinc(Guid? id);
        IEnumerable<UserResponseDTO> GetByName(string Name);
        Task<bool> SaveAsync();
        bool Save();
        void Delete(Guid? id);
    }
}
