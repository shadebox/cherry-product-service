#region Include Definition
using AutoMapper;
using ProductService.Database.Domain;
using ProductService.Rest.Models.Resources;
#endregion

namespace ProductService.Rest.Profiles
{
    #region Public Class Definition
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Product, ProductResource>()
                .ForMember(d => d.ProductID, s => s.MapFrom(x => x.ID));
        }
    }
    #endregion
}