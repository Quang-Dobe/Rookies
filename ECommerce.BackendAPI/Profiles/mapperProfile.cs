using AutoMapper;
using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.Account;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.BackendAPI.Profiles
{
    public class mapperProfile : Profile
    {
        public mapperProfile()
        {
            CreateMap<Product, ShowedProductDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(des => des.productType, act => act.MapFrom(src => src.productType));
            CreateMap<ProductDTO, Product>()
                .ForMember(des => des.productType, act => act.MapFrom(src => src.productType));
            CreateMap<Product, detailProductDTO>()
                .ForMember(dest => dest.productType, act => act.MapFrom(src => src.productType));

            CreateMap<RegisterRequestDTO, IdentityUser>();
        }
    }
}
