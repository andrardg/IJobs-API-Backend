using IJobs.Models.Base;
using IJobs.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly projectContext _context;
        protected readonly DbSet<TEntity> _table;
        public GenericRepository(projectContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        //get all
        public async Task<List<TEntity>> GetAll()
        {
            return await _table.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _table.AsNoTracking();
            //select * from entity
            //var entityList = _table.ToList();
            //var entityListFiltered = entityList.Where(x => x.Id.ToString() != "");
            //sau (versiunea mai buna) select * from entity where id is not null
            //var entityListFiltered2 = _table.Where(x => x.Id.ToString() != "");
        }


        //create
        public void Create(TEntity entity)
        {
            _table.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _table.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _table.AddRangeAsync(entities);
        }


        //update
        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }


        //delete
        public void Delete(TEntity entity)
        {
            _table.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _table.RemoveRange(entities);
        }


        //find
        public TEntity FindById(object id)
        {
            return _table.Find(id);
            //return _table.FirstOrDefault(x => x.Id.Equals(id));
        }

        public async Task<TEntity> FindByIdAsinc(object id)
        {
            return await _table.FindAsync(id);
            //return await _table.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }


        //save
        public bool Save()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch(SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e);
            }
            return false;
        }
    }
}
