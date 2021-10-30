#region Include Definition
using AutoMapper;
using ProductService.BusinessLogic.Dtos;
using ProductService.Database.Domain;
using ProductService.Rest.Models.Resources;
#endregion

namespace ProductService.Rest.Profiles
{
    #region Public Class Definition
    public class ModelToDtoProfile : Profile
    {
        public ModelToDtoProfile()
        {
            CreateMap<ProductDto, ProductResource>();

            CreateMap<ProductResource, ProductDto>();

            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductID, s => s.MapFrom(x => x.ID));

            // CreateMap<ProductDto, Product>()
            //     .ForMember(d => d.ID, s => s.MapFrom(x => x.ProductID));
        }
    }
    #endregion
}