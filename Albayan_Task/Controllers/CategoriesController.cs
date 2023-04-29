using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using Albayan_Task.Service.Iservices.Icategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Albayan_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoriesService _categoriesService { get; set; }

        public CategoriesController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET: api/<CategoriesController>/lang
        [HttpGet()]
        [Route("{lang}")]
        [Route("")]
        public async Task<ActionResult<List<DTOCategory>>> Get(string? lang="en")
        {
            var response = await _categoriesService.GetCategories(lang);
            return response;
        }

        // GET api/<CategoriesController>/5
        [HttpGet]
        [Route("{categoryId}/products/{lang}")]
        [Route("{categoryId}/products")]
        public async Task<ActionResult<List<DTOProducts>>> GetCategoryProducts(int categoryId, string? lang = "en")
        {
            var response = await _categoriesService.GetCategoryProduct(categoryId,lang);
            return response;
        }


        [Authorize]
        [HttpDelete("{categoryId}")]
        public async Task <ActionResult> Delete(int categoryId)
        {
            var response = await _categoriesService.DeleteCategory(categoryId);
            return Ok("Category Deleted Successfuly");
        }
    }
}
