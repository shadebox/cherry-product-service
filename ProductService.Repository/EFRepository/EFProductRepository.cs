#region Include Definition
using ProductService.Repository.IRepository;
using ProductService.Database.Domain;
using ProductService.Database.DBContext;
#endregion

namespace ProductService.Repository.EFRepository
{
    #region Public Class Definition
    public class EFProductRepository : EFBaseRepository<Product>, IProductRepository
    {
        #region Public Constructor Definition
        public EFProductRepository(EFContext context) : base(context) { }
        #endregion
    }
    #endregion
}