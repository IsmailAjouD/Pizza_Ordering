using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pizza_OrderingAPI.Entities;
using Pizza_OrderingAPI.Extentions;
using Pizza_OrderingAPI.Repositories;
using Pizza_OrderingAPI.Repositories.Contracts;
using PizzaOrdering.Models.Dtos;

namespace Pizza_OrderingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMenuDto>>> GetItems()
        { 
            try
            {
                var products = await this.productRepository.GetItems();


                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto();

                    return Ok(productDtos);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]

        public async Task<ActionResult<ProductMenuDto>> GetItem(int id)
        {
            try
            {
             var product = await this.productRepository.GetItem(id);
                if (product == null)
                {
                    return BadRequest();
                }
                else 
                {
                    var productDto = product.ConvertToDto();
                    return Ok(productDto);
                }

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                          "Error retrieving data from the database");
            }

        }

        [HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await productRepository.GetItemCategories();

                var productCategoryDtos = productCategories.ConvertToDto();

                return Ok(productCategoryDtos);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

        }

        [HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductMenuDto>>> GetItemsByCategory (int categoryId)
        {
            try
            {
                var Menus=await productRepository.GetItemsByCategory(categoryId);
                var MenusDto = Menus.ConvertToDto();
                return Ok(MenusDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error retrieving data from the database");
            }
        }

        [HttpGet]
        [Route(nameof(GetExtraItems))]
        public async Task<ActionResult<IEnumerable<ExtraItemDto>>> GetExtraItems()
        {
            try
            {
                var extraItems = await productRepository.GetExtraItems();
                return Ok(extraItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet]
        [Route("{id}/GetExtraItembyListId")]
        public async Task<ActionResult<List<ExtraItemDto>>> GetExtraItembyListId(string id)
        {
            try
            {
                var extraItems = await productRepository.GetExtraItemsbyListid(id);
                var extraItemDtos = extraItems.Select(extraItem => new ExtraItemDto
                {
                    ExtraItemId = extraItem.ExtraItemId,
                    Name = extraItem.Name,
                    Price= extraItem.Price,
                    // Map other properties as needed
                }).ToList(); return Ok(extraItemDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id:int}/GetExtraitembyid")]
        public async Task<ActionResult<ExtraItemDto>> GetExtraitembyid(int id)
        {
            try
            {
                var ExtraItem = await productRepository.GetExtraItem(id);
                if(ExtraItem == null)
                {
                    return BadRequest();
                }
                else
                {
                    var ExtraDto = ExtraItem.ConvertToDto();
                    return ExtraDto;
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error retrieving data from the database");
            }

        }
        [HttpGet("{id:int}/GetCartItembysizeId")]
        public async Task<ActionResult<ProductSize>> GetCartItembysizeId(int id)
        {
            try
            {
                var size = await productRepository.GetItembySizeId(id);
                if (size == null)
                {
                    return NotFound();
                }
                var sizeItemDto = size.ConvertToDto();
                return Ok(sizeItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
