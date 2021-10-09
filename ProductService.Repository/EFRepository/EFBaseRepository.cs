#region Include Definition
using ProductService.Database.DBContext;
using ProductService.Database.Domain;
using ProductService.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Linq;
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
                    return await entity.AsExpandable().Where(whereCondition).FirstOrDefaultAsync();
                else
                    return await entity.FirstOrDefaultAsync();
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

        public TEntity Insert(TEntity entity)
        {
            // Add Client to dbContext
            _context.Set<TEntity>().Add(entity);
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