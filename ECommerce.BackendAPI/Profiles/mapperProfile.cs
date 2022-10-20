using AutoMapper;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;

namespace ECommerce.BackendAPI.Profiles
{
    public class mapperProfile : Profile
    {
        public mapperProfile()
        {
            CreateMap<Product, ShowedProductDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Order, ReviewProductDTO>();
            CreateMap<OrderDetail, ReviewProductDTO>();
        }
    }
}
