using IJobs.Data;
using IJobs.Models;
using IJobs.Repositories.GenericRepository;
using IJobs.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.UserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(projectContext context) : base(context)
        {

        }
        public List<User> GetByName(string Name)
        {
            return _table.Where(s => s.FirstName!.ToLower().Contains(Name.ToLower()) || s.LastName!.ToLower().Contains(Name.ToLower())).ToList();
        }
        public List<User> GetByFirstName(string FirstName)
        {
            return _table.Where(s => s.FirstName!.ToLower().Contains(FirstName.ToLower())).ToList();
        }
        public List<User> GetByLastName(string LastName)
        {
            return _table.Where(x => x.LastName!.ToLower().Contains(LastName.ToLower())).ToList();
        }
        public List<User> GetByEmail(string Email)
        {
            return _table.Where(x => x.Email!.ToLower().Contains(Email.ToLower())).ToList();
        }
        public User EmailExists(string Email)
        {
            return _table.FirstOrDefault(x => x.Email!.ToLower().Equals(Email.ToLower()));
        }
        public List<User> GetAllWithJoin()
        {
            var result = from user in _table
                         select new User
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 PasswordHash = user.PasswordHash,
                                 Occupation = user.Occupation,
                                 Residence = user.Residence,
                                 Studies = user.Studies,
                                 CV = user.CV,
                                 Photo = user.Photo,
                                 Role = user.Role,
                                 Applications = ((ICollection<Application>)(from a in _context.Applications
                                                where a.UserId == user.Id
                                                select a).ToList()),
                                 Invites = ((ICollection<Invite>)(from a in _context.Invites
                                                where a.UserId == user.Id
                                                select a).ToList()),
                                 Jobs = ((ICollection<Job>)(from a in _context.Jobs
                                                where a.UserId == user.Id
                                                select a).ToList()),
                         };            return result.ToList();
        }
        public User GetByIdJoin(Guid? id)
        {
            var result = _table.Where(x => x.Id == id).FirstOrDefault();
            result.Applications = ((ICollection<Application>)(from a in _context.Applications
                                                              where a.UserId == result.Id
                                                              select a).ToList());
            result.Invites = ((ICollection<Invite>)(from a in _context.Invites
                                                    where a.UserId == result.Id
                                                    select a).ToList());
            result.Jobs = ((ICollection<Job>)(from a in _context.Jobs
                                              where a.UserId == result.Id
                                              select a).ToList());
            return result;
        }
        public new bool Delete(User entity)
        {
            _context.Remove(entity);
            return true;
        }
        public new bool Update(User entity)
        {
            _context.Update(entity);
            _table.Update(entity);
            return true;
        }
    }
}
