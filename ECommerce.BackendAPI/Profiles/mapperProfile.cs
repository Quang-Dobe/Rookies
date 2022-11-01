using AutoMapper;
using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.Account;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.BackendAPI.Profiles
{
    public class mapperProfile : Profile
    {
        public mapperProfile()
        {
            // CustomerSite
            CreateMap<Product, ShowedProductDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(des => des.productType, act => act.MapFrom(src => src.ProductType));
            CreateMap<ProductDTO, Product>()
                .ForMember(des => des.ProductType, act => act.MapFrom(src => src.productType));
            CreateMap<Product, detailProductDTO>()
                .ForMember(dest => dest.productType, act => act.MapFrom(src => src.ProductType));

            CreateMap<RegisterRequestDTO, IdentityUser>();


            CreateMap<Cart, CartDTO>();
            CreateMap<CartDTO, Cart>();

            CreateMap<CartDetail, ShowedCartDetailDTO>()
                .ForMember(des => des.showedProductDTO, act => act.MapFrom(src => new ShowedProductDTO
                {
                    id = src.Product.Id,
                    productImg = src.Product.ProductImg,
                    productName = src.Product.ProductName,
                    price = src.Product.Price,
                    rating = src.Product.Rating,
                }));
            CreateMap<OrderDetail, ShowedOrderDetailDTO>()
                .ForMember(des => des.showedProductDTO, act => act.MapFrom(src => new ShowedProductDTO
                {
                    id = src.Product.Id,
                    productImg = src.Product.ProductImg,
                    productName = src.Product.ProductName,
                    price = src.Product.Price,
                    rating = src.Product.Rating,
                }))
                .ForMember(des => des.rating, act => act.MapFrom(src => (int)src.Rating));


            // AdminSite
            CreateMap<Product, AllProductDTO>()
                .ForMember(des => des.id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.ProductType, act => act.MapFrom(src => (int)src.ProductType));
            CreateMap<AllProductDTO, Product>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.id))
                .ForMember(des => des.ProductType, act => act.MapFrom(src => (ProductType)src.ProductType));
            CreateMap<IdentityUser, AllUserDTO>()
                .ForMember(des => des.id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.EmailConfirmed, act => act.MapFrom(src => src.EmailConfirmed ? 1 : 0))
                .ForMember(des => des.PhoneNumberConfirmedConfirmed, act => act.MapFrom(src => src.PhoneNumberConfirmed ? 1 : 0));
        }
    }
}
