using ECommerce.Data.Model;
using ECommerce.SharedView.DTO;

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
                    productType = 0,
                    price = 4690
                },
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/UwKfc2vSQGNYIHP23DfTWcToEmsIaxjQsdx0DtIEbqCeZ5dnGBPS7d7WCVW9TOiIkfAh2ddgwDvnOR5U_jg=w500-rw",
                    productName = "CPU INTEL Core i5-10400",
                    description = "Socket: LGA 1200 , Intel Core thế hệ thứ 10",
                    productType = 0,
                    price = 4429
                },
                new ProductDTO {
                    productImg = "https://lh3.googleusercontent.com/3K84fNb4XFMvh7JyJ1-itImN6petr8lxpeLhNCIEpidnZGc0fOIjN5SQiHvWM3InvCFzJjwrpOpK3sY0P95o7ZA4VV-aB1JxiA=w500-rw",
                    productName = "CPU INTEL Core i5-11600K",
                    description = "Socket: 1200, Intel Core thế hệ thứ 11",
                    productType = 0,
                    price = 6099
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
                    productType = "CPU",
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
                new ECommerce.Data.Model.Product
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
                new ECommerce.Data.Model.Product
                {
                    Id = 3,
                    productImg = "https://lh3.googleusercontent.com/3K84fNb4XFMvh7JyJ1-itImN6petr8lxpeLhNCIEpidnZGc0fOIjN5SQiHvWM3InvCFzJjwrpOpK3sY0P95o7ZA4VV-aB1JxiA=w500-rw",
                    productName = "CPU INTEL Core i5-11600K",
                    description = "Socket: 1200, Intel Core thế hệ thứ 11",
                    productType = (ECommerce.Data.Enums.ProductType)1,
                    price = 6099,
                    quantity = 0,
                    inventoryNumber = 0,
                    rating = 1,
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
                        }
                    };
                case 1:
                    return new List<Product>
                    {
                        new ECommerce.Data.Model.Product
                        {
                            Id = 2,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)1,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 3,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)1,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)2,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 5,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)2,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)3,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 7,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)3,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)4,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 9,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)4,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)5,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 11,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)5,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)6,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 13,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)6,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)7,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
                            createdDate = DateTime.Parse("2022-10-26 14:21:58.1796606"),
                            updatedDate = DateTime.Parse("2022-10-26 14:21:58.1796638")
                        },
                        new ECommerce.Data.Model.Product
                        {
                            Id = 15,
                            productImg = "abc",
                            productName = "abc",
                            description = "abc",
                            productType = (ECommerce.Data.Enums.ProductType)7,
                            price = 4690,
                            quantity = 0,
                            inventoryNumber = 0,
                            rating = 1,
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
