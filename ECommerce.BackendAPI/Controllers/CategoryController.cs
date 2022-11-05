using AutoMapper;
using ECommerce.BackendAPI.Repository;
using ECommerce.Data.Model;
using ECommerce.SharedView.DTO.AdminSiteDTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this._categoryRepository = categoryRepository;
            this._mapper = mapper;
        }


        [HttpGet]
        [Route("{Id:int}")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult<AllCategoryDTO>> GetCategory([FromRoute] int Id)
        {
            Category category = await _categoryRepository.GetCategory(Id);
            AllCategoryDTO allCategoryDTO = _mapper.Map<AllCategoryDTO>(category);
            if (category == null)
            {
                return BadRequest("Invalid Category ID");
            }
            return Ok(allCategoryDTO);
        }


        [HttpGet]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult<List<AllCategoryDTO>>> GetAllCategory()
        {
            List<Category> categories = await _categoryRepository.GetCategories();
            List<AllCategoryDTO> allCategoryDTOs = _mapper.Map<List<AllCategoryDTO>>(categories);
            return Ok(allCategoryDTOs);
        }


        [HttpPost("Create")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult> CreateCategory([FromBody] AllCategoryDTO allCategoryDTO)
        {
            await _categoryRepository.CreateCategory(allCategoryDTO.name, allCategoryDTO.description);
            await _categoryRepository.Save();
            return Ok("Create Category Sucessfully!");
        }


        [HttpPost("Update")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult> UpdateCategory_([FromBody] AllCategoryDTO allCategoryDTO)
        {

            Category category = await _categoryRepository.GetCategory(allCategoryDTO.id);
            if (category == null)
            {
                return BadRequest("Invalid Category ID");
            }
            category.Name = allCategoryDTO.name;
            category.Description = allCategoryDTO.description;
            _categoryRepository.UpdateCategory(category);
            await _categoryRepository.Save();
            return Ok("Updated Category ID");
        }


        [HttpDelete]
        [Route("{id:int}")]
        [EnableCors("_myAdminSite")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            Category category = await _categoryRepository.GetCategory(id);
            if (category == null)
            {
                return BadRequest("Invalid Category ID");
            }
            _categoryRepository.DeleteCategory(category);
            await _categoryRepository.Save();
            return Ok("Deleted Catedgory ID");
        }
    }
}
