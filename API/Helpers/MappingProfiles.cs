using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDTO>()
            .ForMember( d => d.ProductBand,o => o.MapFrom(s => s.ProductBand.Name))
            .ForMember( d => d.ProductType,o => o.MapFrom(s => s.ProductType.Name))
            .ForMember( d => d.PictureUrl ,o => o.MapFrom<ProductUrlResolver>());

        }
    }
}