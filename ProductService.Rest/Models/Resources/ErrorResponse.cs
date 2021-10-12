#region Include Definition
using System.Collections.Generic;
#endregion

namespace ProductService.Rest.Models.Resources
{
    #region Public Class Definition
    public class ErrorResponse : BaseResponse
    {
        #region Public Property Definition
        public IList<string> Messages { get; protected set; }
        #endregion

        #region Public Constructor Definition
        public ErrorResponse(IList<string> messages) : base(false)
        {
            Messages = messages;
        }
        #endregion
    }
    #endregion
}