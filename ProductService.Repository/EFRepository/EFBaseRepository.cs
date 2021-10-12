#region Include Definition
using ProductService.Database.DBContext;
using ProductService.Database.Domain;
using ProductService.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;
using LinqKit;
using System.Threading.Tasks;
#endregion

namespace ProductService.Repository.EFRepository
{
    #region Public Class Definition
    public abstract class EFBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Protected Field
        protected EFContext _context;
        #endregion

        #region Public Constructor Definition
        public EFBaseRepository(EFContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Method Definition
        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            try
            {
                IQueryable<TEntity> entity;

                entity = _context.Set<TEntity>().AsNoTracking();

                if (whereCondition != null)
                    return await entity.AsExpandable().Where(whereCondition).SingleOrDefaultAsync();
                else
                    return await entity.SingleOrDefaultAsync();
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch(InvalidOperationException)
            {
                throw;
            }
        }

        public async Task<IList<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> whereCondition, int page = 1, int pageSize = 1)
        {
            try
            {
                int pageStart = (page - 1) * pageSize;
                IQueryable<TEntity> entity;

                entity = _context.Set<TEntity>().AsNoTracking();

                if (whereCondition != null)
                    entity = entity.AsExpandable().Where(whereCondition).OrderBy(p => p.ID);
                else
                    entity = entity.OrderBy(p => p.ID);
                
                if (page == 0 && pageSize == 0)
                    return await entity.ToListAsync();
                else
                    return await entity.Skip(pageStart).Take(pageSize).ToListAsync();

            }
            catch(ArgumentNullException)
            {
                throw;
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            // Add Client to dbContext
            await _context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public TEntity Save(TEntity entity)
        {
            // Attach entity to dbContext && Set status as modified
            _context.Set<TEntity>().Attach(entity);
            SetEntityStateModified(entity, EntityState.Modified);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            // Attach entity to dbcontext && Set status as modified, Never delete entity
            _context.Set<TEntity>().Attach(entity);
            SetEntityStateModified(entity, EntityState.Modified);// EntityState.Deleted; 
            return entity;
        }
        #endregion

        #region Prviate Method Definition
        private void SetEntityStateModified(TEntity entity, EntityState entityState)
        {
            _context.Entry(entity).State = entityState;
        }
        #endregion
    }
    #endregion
}