#region Include Definition
using ProductService.Database.DBContext;
using ProductService.Database.Domain;
using ProductService.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
#endregion

namespace ProductService.Repository.EFRepository
{
    #region Public Class Definition
    public class EFBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
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
        public virtual TEntity Insert(TEntity entity)
        {
            // Add Client to dbContext
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public virtual TEntity Save(TEntity entity)
        {
            // Attach entity to dbContext && Set status as modified
            _context.Set<TEntity>().Attach(entity);
            SetEntityStateModified(entity, EntityState.Modified);
            return entity;
        }

        public virtual TEntity Delete(TEntity entity)
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
            context.Entry(entity).State = entityState;
        }
        #endregion
    }
    #endregion
}