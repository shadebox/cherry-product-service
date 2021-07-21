#region
using System;
#endregion

namespace ProductService.Database.Domain
{
    #region Public Class Definition
    public abstract class ExtendedEntity : BaseEntity
    {
        #region Public Property Definition
        public DateTime? ModifiedDate { get; set; }
        #endregion
    }
    #endregion
}