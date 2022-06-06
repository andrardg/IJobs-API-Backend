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
        public List<User> GetAllWithApplications()
        {
            /*var result = from user in _table
                         from app in _context.Applications
                         where app.UserId == user.Id
                         select *;

            var applications = from 
            var result = _table.Select(user => new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Residence = user.Residence,
                Studies = user.Studies,
                CV = user.CV,
                Photo = user.Photo,
                Role = user.Role,
                Applications =  from a in _context.Applications
                                where a.UserId equals user.Id
        }).ToList();*/
            var result = from user in _table
                         join app in _context.Applications on user.Id equals app.UserId
                         select new User
                             {
                                 Id = user.Id,
                                 FirstName = user.FirstName,
                                 LastName = user.LastName,
                                 Email = user.Email,
                                 PasswordHash = user.PasswordHash,
                                 Residence = user.Residence,
                                 Studies = user.Studies,
                                 CV = user.CV,
                                 Photo = user.Photo,
                                 Role = user.Role,
                                 Applications = ((ICollection<Application>)(from a in _context.Applications
                                                where a.UserId == app.UserId
                                                select a)),
                             };            return result.ToList();
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
