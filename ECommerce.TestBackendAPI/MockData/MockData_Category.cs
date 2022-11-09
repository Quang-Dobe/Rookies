using ECommerce.Data.Model;
using ECommerce.SharedView.DTO.AdminSiteDTO;

namespace ECommerce.TestBackendAPI.MockData
{
    public static class MockData_Category
    {
        public static List<Category> GetAllCategory()
        {
            return new List<Category>
                {
                    new Category
                    {
                        Id = 0,
                        Name = "CPU",
                        Description = "CPU"
                    },
                    new Category {
                        Id = 1,
                        Name = "Mainboard",
                        Description = "Mainboard"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "VGA",
                        Description = "VGA"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "PSU",
                        Description = "PSU"
                    },
                    new Category {
                        Id = 4,
                        Name = "RAM",
                        Description = "RAM"
                    },
                    new Category
                    {
                        Id = 5,
                        Name = "Case",
                        Description = "Case"
                    },
                    new Category
                    {
                        Id = 6,
                        Name = "Fan",
                        Description = "Fan"
                    },
                    new Category
                    {
                        Id = 7,
                        Name = "Others",
                        Description = "Others"
                    }
                };
        }


        public static List<AllCategoryDTO> GetAllCategoryDTO()
        {
            return new List<AllCategoryDTO>
                {
                    new AllCategoryDTO
                    {
                        id = 0,
                        name = "CPU",
                        description = "CPU"
                    },
                    new AllCategoryDTO {
                        id = 1,
                        name = "Mainboard",
                        description = "Mainboard"
                    },
                    new AllCategoryDTO
                    {
                        id = 2,
                        name = "VGA",
                        description = "VGA"
                    },
                    new AllCategoryDTO
                    {
                        id = 3,
                        name = "PSU",
                        description = "PSU"
                    },
                    new AllCategoryDTO {
                        id = 4,
                        name = "RAM",
                        description = "RAM"
                    },
                    new AllCategoryDTO
                    {
                        id = 5,
                        name = "Case",
                        description = "Case"
                    },
                    new AllCategoryDTO
                    {
                        id = 6,
                        name = "Fan",
                        description = "Fan"
                    },
                    new AllCategoryDTO
                    {
                        id = 7,
                        name = "Others",
                        description = "Others"
                    }
                };
        }
    }
}
