using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;
using ECommerce.SharedView.DTO.AdminSiteDTO;

namespace ECommerce.TestBackendAPI.MockData
{
    public class MockData_Product
    {
        public static List<ProductDTO> CreateProductDTO()
        {
            return new List<ProductDTO>
            {
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                    productName = "CPU Intel Core I5-7600",
                    description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                    categoryId = 0,
                    price = 4690
                },
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/UwKfc2vSQGNYIHP23DfTWcToEmsIaxjQsdx0DtIEbqCeZ5dnGBPS7d7WCVW9TOiIkfAh2ddgwDvnOR5U_jg=w500-rw",
                    productName = "CPU INTEL Core i5-10400",
                    description = "Socket: LGA 1200 , Intel Core thế hệ thứ 10",
                    categoryId = 0,
                    price = 4429
                },
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/3K84fNb4XFMvh7JyJ1-itImN6petr8lxpeLhNCIEpidnZGc0fOIjN5SQiHvWM3InvCFzJjwrpOpK3sY0P95o7ZA4VV-aB1JxiA=w500-rw",
                    productName = "CPU INTEL Core i5-11600K",
                    description = "Socket: 1200, Intel Core thế hệ thứ 11",
                    categoryId = 0,
                    price = 6099
                }
            };
        }


        public static List<AllProductDTO> CreateAllProductDTO()
        {
            return new List<AllProductDTO>
            {
                new AllProductDTO
                {
                    id = 1,
                    ProductImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                    ProductName = "CPU Intel Core I5-7600",
                    Description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                    CategoryId = 0,
                    Price = 4690,
                    Quantity = 50,
                    InventoryNumber = 50,
                    Rating = 1,
                    createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                    updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                },
                new AllProductDTO
                {
                    id = 2,
                    ProductImg = "https://lh3.googleusercontent.com/UwKfc2vSQGNYIHP23DfTWcToEmsIaxjQsdx0DtIEbqCeZ5dnGBPS7d7WCVW9TOiIkfAh2ddgwDvnOR5U_jg=w500-rw",
                    ProductName = "CPU INTEL Core i5-10400",
                    Description = "Socket: LGA 1200 , Intel Core thế hệ thứ 10",
                    CategoryId = 0,
                    Price = 4429,
                    Quantity = 50,
                    InventoryNumber = 50,
                    Rating = 1,
                    createdDate = DateTime.Parse("2022-10-26 14:22:38.2973197"),
                    updatedDate = DateTime.Parse("2022-10-26 14:22:38.2973213")
                },
                new AllProductDTO
                {
                    id = 3,
                    ProductImg = "https://lh3.googleusercontent.com/3K84fNb4XFMvh7JyJ1-itImN6petr8lxpeLhNCIEpidnZGc0fOIjN5SQiHvWM3InvCFzJjwrpOpK3sY0P95o7ZA4VV-aB1JxiA=w500-rw",
                    ProductName = "CPU INTEL Core i5-11600K",
                    Description = "Socket: 1200, Intel Core thế hệ thứ 11",
                    CategoryId = 0,
                    Price = 6099,
                    Quantity = 50,
                    InventoryNumber = 50,
                    Rating = 1,
                    createdDate = DateTime.Parse("2022-10-26 14:23:24.8310386"),
                    updatedDate = DateTime.Parse("2022-10-26 14:23:24.8310397")
                }
            };
        }


        public static List<detailProductDTO> CreateDetailProductDTO()
        {
            return new List<detailProductDTO>
            {
                new detailProductDTO
                {
                    id = 1,
                    productImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                    productName = "CPU Intel Core I5-7600",
                    description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                    categoryId = 0,
                    price = 4690,
                    inventoryNumber = 0,
                    rating = 1,
                    reviewProductDTOs = new List<ReviewDTO>
                    {
                        new ReviewDTO
                        {
                            userName = "Admin",
                            comment = "Everything is Ok now",
                            rating = 4
                        }
                    }
                }
            };
        }


        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new ECommerce.Data.Model.Product
                {
                    Id = 1,
                    ProductImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                    ProductName = "CPU Intel Core I5-7600",
                    Description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                    CategoryId = 0,
                    Price = 4690,
                    Quantity = 50,
                    InventoryNumber = 50,
                    Rating = 1,
                    createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                    updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                },
                new ECommerce.Data.Model.Product
                {
                    Id = 2,
                    ProductImg = "https://lh3.googleusercontent.com/UwKfc2vSQGNYIHP23DfTWcToEmsIaxjQsdx0DtIEbqCeZ5dnGBPS7d7WCVW9TOiIkfAh2ddgwDvnOR5U_jg=w500-rw",
                    ProductName = "CPU INTEL Core i5-10400",
                    Description = "Socket: LGA 1200 , Intel Core thế hệ thứ 10",
                    CategoryId = 1,
                    Price = 4429,
                    Quantity = 50,
                    InventoryNumber = 50,
                    Rating = 1,
                    createdDate = DateTime.Parse("2022-10-26 14:22:38.2973197"),
                    updatedDate = DateTime.Parse("2022-10-26 14:22:38.2973213")
                },
                new ECommerce.Data.Model.Product
                {
                    Id = 3,
                    ProductImg = "https://lh3.googleusercontent.com/3K84fNb4XFMvh7JyJ1-itImN6petr8lxpeLhNCIEpidnZGc0fOIjN5SQiHvWM3InvCFzJjwrpOpK3sY0P95o7ZA4VV-aB1JxiA=w500-rw",
                    ProductName = "CPU INTEL Core i5-11600K",
                    Description = "Socket: 1200, Intel Core thế hệ thứ 11",
                    CategoryId = 1,
                    Price = 6099,
                    Quantity = 50,
                    InventoryNumber = 50,
                    Rating = 1,
                    createdDate = DateTime.Parse("2022-10-26 14:23:24.8310386"),
                    updatedDate = DateTime.Parse("2022-10-26 14:23:24.8310397")
                }
            };
        }


        public static List<Product> GetProductByType(int a)
        {
            switch (a)
            {
                case 0:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 1,
                            ProductImg = "https://lh3.googleusercontent.com/Omba4ZoTRs4tR_2u3eD7455PuuwCyoHXLF5rfn0vi9v6H2k_ji_RrYzyVWw9g2P8JmbKDQ16Q17q31IiFgC1=w500-rw",
                            ProductName = "CPU Intel Core I5-7600",
                            Description = "Socket: LGA 1151 , Intel Core thế hệ thứ 7",
                            CategoryId = 0,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 1:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 2,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 1,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 3,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 1,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 2:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 4,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 2,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 5,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 2,
                            Price = 4690,
                            Quantity = 0,
                            InventoryNumber = 0,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 3:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 6,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 3,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 7,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 3,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 4:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 8,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 4,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 9,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 4,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 5:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 10,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 5,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 11,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 5,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 6:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 12,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 6,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 13,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 6,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                case 7:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 14,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 7,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 15,
                            ProductImg = "abc",
                            ProductName = "abc",
                            Description = "abc",
                            CategoryId = 7,
                            Price = 4690,
                            Quantity = 50,
                            InventoryNumber = 50,
                            Rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        }
                    };
                default:
                    return null;
            }
        }
    }
}
