#region Include Definition
using AutoMapper;
using ProductService.BusinessLogic.Dtos;
using ProductService.Database.Domain;
#endregion

namespace ProductService.Rest.Profiles
{
    #region Public Class Definition
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(d => d.ID, s => s.MapFrom(x => x.ProductID));
        }
    }
    #endregion
}