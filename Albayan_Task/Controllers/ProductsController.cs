using Albayan_Task.Domain.Entities.Products;
using Albayan_Task.DTO.Categories;
using Albayan_Task.DTO.Products;
using Albayan_Task.Errors;
using Albayan_Task.Service.Iservices.Icategories;
using Albayan_Task.Service.Iservices.Iproducts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Albayan_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        ICategoriesService _categoriesService { get; set; }
        IProductsService _productsService  { get; set; }

        public ProductsController(ICategoriesService categoriesService, IProductsService productsService)
        {
            _categoriesService = categoriesService;
            _productsService = productsService;
        }

        /// <summary>
        /// add product 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<long>> Add(DtoAddProduct product)
        {
            // make sure category is there
            if (product.CategoryID== 0)
            {
                return BadRequest(new APIValidationErrorResponce
                {
                    Errors = new
                    [] { "Category is mandatory to insert new product" }
                });
            }
            product.CreationDate = DateTime.Now;
            var response = await _productsService.AddProduct(product);
            return response;
        }

        /// <summary>
        /// get all product that are in activation duration and must shown
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("{lang}")]
        public async Task<ActionResult<List<DTOProducts>>> Get(string? lang="en")
        {
            var response = await _productsService.GetProducts(lang);
            return response;
        }

        /// <summary>
        /// update product
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("{productId}")]
        public async Task< ActionResult<bool>> Update(int productId,DtoAddProduct model)
        {
            model.Id = productId;
            // make sure product is there
            if (_productsService.GetProductById(productId) == null)
                return NotFound(new APIValidationErrorResponce
                {
                    Errors = new
                        [] { "product not found" }
                });
            // make sure category is there
            else if (!await _categoriesService.isAvilable(productId))
                return NotFound(new APIValidationErrorResponce
                {
                    Errors = new
                        [] { "Category not found" }
                });
            var response = await _productsService.EditProduct(model);
            return response;
        }

        // delete product 
        [Authorize]
        [HttpDelete("{productId}")]
        public async Task <ActionResult> Delete(int productId)
        {
            var response = await _productsService.DeleteProduct(productId);
            if (response)
                return Ok("Category Deleted Successfuly");
            else
                return  BadRequest(new APIValidationErrorResponce
                {
                    Errors = new
                    [] { "Can't delete" }
                }); 
        }
    }
}
