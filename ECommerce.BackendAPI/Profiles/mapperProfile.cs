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
                .ForMember(des => des.categoryId, act => act.MapFrom(src => src.CategoryId));
            CreateMap<ProductDTO, Product>()
                .ForMember(des => des.CategoryId, act => act.MapFrom(src => src.categoryId));
            CreateMap<Product, detailProductDTO>()
                .ForMember(dest => dest.categoryId, act => act.MapFrom(src => src.CategoryId));

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
                .ForMember(des => des.CategoryId, act => act.MapFrom(src => src.CategoryId));
            CreateMap<AllProductDTO, Product>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.id))
                .ForMember(des => des.CategoryId, act => act.MapFrom(src => src.CategoryId));
            CreateMap<IdentityUser, AllUserDTO>()
                .ForMember(des => des.id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.EmailConfirmed, act => act.MapFrom(src => src.EmailConfirmed ? 1 : 0))
                .ForMember(des => des.PhoneNumberConfirmedConfirmed, act => act.MapFrom(src => src.PhoneNumberConfirmed ? 1 : 0));
            CreateMap<Category, AllCategoryDTO>()
                .ForMember(des => des.id, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.name, act => act.MapFrom(src => src.Name))
                .ForMember(des => des.description, act => act.MapFrom(src => src.Description));
            CreateMap<AllCategoryDTO, Category>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.id))
                .ForMember(des => des.Description, act => act.MapFrom(src => src.description))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.name));
        }
    }
}
