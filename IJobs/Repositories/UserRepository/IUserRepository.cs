using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetByName(string Name);
        List<User> GetByFirstName(string FirstName);
        List<User> GetByLastName(string LastName);
        List<User> GetByEmail(string Email);
        User EmailExists(string Email);
        List<User> GetAllWithApplications();
    }
}
