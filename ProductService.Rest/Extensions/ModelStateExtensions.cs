#region Include Definition
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
#endregion

namespace ProductService.Rest.Extensions
{
    #region Public Class Definition
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary dictionary)
        {
            return dictionary.SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToList();
        }
    }
    #endregion
}