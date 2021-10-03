#region Include Definition
using ProductService.Repository.IService;
using ProductService.Database.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
#endregion

namespace ProductService.Repository.EFService
{
    #region Public Class Definition
    public class EFBaseService : IBaseService
    {        
        #region Private Field Definition
        private EFContext _context;
        #endregion

        #region Public Constructor Definition
        public EFBaseService(EFContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Method Definition
        public EFContext GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            int updatedEntities = 0;

            try
            {
                updatedEntities = await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return updatedEntities;
        }
        #endregion
    }
    #endregion
}