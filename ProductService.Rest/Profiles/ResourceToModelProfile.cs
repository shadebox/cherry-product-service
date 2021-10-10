#region Include Definition
using AutoMapper;
using ProductService.Database.Domain;
using ProductService.Rest.Models.Bindings;
#endregion

namespace ProductService.Rest.Profiles
{
    #region Public Class Definition
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveProductBinding, Product>();
        }
    }
    #endregion
}