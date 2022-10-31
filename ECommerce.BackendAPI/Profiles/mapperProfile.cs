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
        }
    }
}
