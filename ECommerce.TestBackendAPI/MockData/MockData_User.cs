using Microsoft.AspNetCore.Identity;


namespace ECommerce.TestBackendAPI.MockData
{
    public static class MockData_User
    {
        public static List<IdentityUser> GetUsers()
        {
            return new List<IdentityUser>{
                new IdentityUser
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
                }
            };
        }
    }
}
