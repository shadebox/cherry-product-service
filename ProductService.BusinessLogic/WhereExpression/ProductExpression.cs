#region Include Definition
using LinqKit;
using System;
using System.Linq.Expressions;
using ProductService.Database.Domain;
#endregion

namespace ProductService.BusinessLogic.WhereExpression
{
    #region Public Class Definition
    public class ProductExpression
    {
        // Where (ProductID == 1)
        public Expression<Func<Product, bool>> GetProduct(Product product)
        {
            ExpressionStarter<Product> resultExpression = PredicateBuilder.New<Product>(true);

            // ProductID == 1
            long tempProductID = product.ID;
            resultExpression = resultExpression.And(x => x.ID == tempProductID);

            return resultExpression;
        }
    }
    #endregion
}