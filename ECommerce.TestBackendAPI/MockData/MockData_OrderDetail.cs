﻿using ECommerce.Data.Enums;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;

namespace ECommerce.TestBackendAPI.MockData
{
    public static class MockData_OrderDetail
    {
        public static List<Order> GetListOrder()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    userId = "38cd5450-4071-453d-b146-5940453bbe50",
                    user = new Microsoft.AspNetCore.Identity.IdentityUser
                    {
                        Id = "38cd5450-4071-453d-b146-5940453bbe50",
                        UserName = "tdquangcr",
                        NormalizedUserName = "TDQUANGCR",
                        Email = "tdquangcr@gmail.com",
                        NormalizedEmail = "TDQUANGCR@GMAIL.COM",
                        EmailConfirmed = false,
                        PasswordHash = "AQAAAAEAACcQAAAAELAUQGLVTJ9EB8v+Rh7HP+blpQ1PoD+0pH/FyM8xGbzt0cwyqq+YHwLDEXvJq7zxHg==",
                        SecurityStamp = "3SVMQ6O6WWISTPUEYTHKEFAUR74UJNBV",
                        ConcurrencyStamp = "d1fce8a9-c019-4c2e-b6ef-5ffa8994d80a",
                        PhoneNumber = "0356131798",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        LockoutEnd = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                    },
                    orderDetails = new List<OrderDetail> { },
                    dateOfPurchase = DateTime.Today,
                    total = 0
                }
            };
        }

        public static List<OrderDetail> GetListOrderDetail(Order order)
        {
            return new List<OrderDetail>
            {
                new OrderDetail
                {
                    Id = 1,
                    productId = 1,
                    product = new Product
                    {
                        Id = 1,
                        productImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                        productName = "CPU Intel Core I5-7600",
                        description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                        productType = (ECommerce.Data.Enums.ProductType)0,
                        price = 4690,
                        quantity = 0,
                        inventoryNumber = 0,
                        rating = 1,
                        createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                        updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                    },
                    orderId = order.Id,
                    order = order,
                    number = 1,
                    datePurchase = DateTime.Today,
                    comment = "This is just a test comment",
                    rating = (ProductRating)5
                },
                new OrderDetail
                {
                    Id = 2,
                    productId = 2,
                    product = new Product
                    {
                        Id = 2,
                        productImg = "https://lh3.googleusercontent.com/UwKfc2vSQGNYIHP23DfTWcToEmsIaxjQsdx0DtIEbqCeZ5dnGBPS7d7WCVW9TOiIkfAh2ddgwDvnOR5U_jg=w500-rw",
                        productName = "CPU INTEL Core i5-10400",
                        description = "Socket: LGA 1200 , Intel Core thế hệ thứ 10",
                        productType = (ECommerce.Data.Enums.ProductType)1,
                        price = 4429,
                        quantity = 0,
                        inventoryNumber = 0,
                        rating = 1,
                        createdDate = DateTime.Parse("2022-10-26 14:22:38.2973197"),
                        updatedDate = DateTime.Parse("2022-10-26 14:22:38.2973213")
                    },
                    orderId = order.Id,
                    order = order,
                    number = 2,
                    datePurchase = DateTime.Today,
                    comment = "This is just another test comment",
                    rating = (ProductRating)4
                }
            };
        }

        public static List<OrderDetailDTO> GetListOrderDetailDTO()
        {
            return new List<OrderDetailDTO>
            {
                new OrderDetailDTO
                {
                    productId = 1,
                    number = 1
                },
                new OrderDetailDTO
                {
                    productId = 2,
                    number = 2
                }
            };
        }
    }
}