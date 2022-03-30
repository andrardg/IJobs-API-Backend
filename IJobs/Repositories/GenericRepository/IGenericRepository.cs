using IJobs.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Repositories.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //get all data
        Task<List<TEntity>> GetAll();
        IQueryable<TEntity> GetAllAsQueryable();

        //create
        void Create(TEntity entity);
        Task CreateAsync(TEntity entity);
        void CreateRange(IEnumerable<TEntity> entities);
        Task CreateRangeAsync(IEnumerable<TEntity> entities);

        //update
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);


        // Delete
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);


        // Find
        TEntity GetById(object id);
        Task<TEntity> GetByIdAsinc(object id);


        // Save
        bool Save();
        Task<bool> SaveAsync();
    }
}
