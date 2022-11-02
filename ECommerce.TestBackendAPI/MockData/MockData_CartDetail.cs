using ECommerce.Data.Model;

namespace ECommerce.TestBackendAPI.MockData
{
    public static class MockData_CartDetail
    {
        public static List<Cart> GetListCart()
        {
            return new List<Cart>
            {
                new Cart
                {
                    Id = 1,
                    UserId = "05235465-f941-4e00-98bb-5306da1de482",
                    User = new Microsoft.AspNetCore.Identity.IdentityUser
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
                    CartDetails = new List<CartDetail> { },
                    Total = 0
                }
            };
        }

        public static List<CartDetail> GetListCartDetail(Cart cart)
        {
            return new List<CartDetail>
            {
                new CartDetail
                {
                    Id = 1,
                    ProductId = 1,
                    Product = new Product
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
                    CartId = cart.Id,
                    Cart = cart,
                    Number = 1
                },
                new CartDetail
                {
                    Id = 2,
                    ProductId = 2,
                    Product = new Product
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
                    CartId = cart.Id,
                    Cart = cart,
                    Number = 2
                }
            };
        }
    }
}
