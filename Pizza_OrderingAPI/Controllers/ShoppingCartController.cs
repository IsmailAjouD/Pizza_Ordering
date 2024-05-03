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
    public class ShoppingCartController : ControllerBase
    {
        private IShoppingCartRepository _shoppingCartRepository;
        private IProductRepository _productRepository;


        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }

        //[HttpGet("{userId}/GetItems")]
        //public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        //{
        //    try
        //    {
        //        var cartItems = await _shoppingCartRepository.GetItemsByUser(userId);
        //        if (cartItems == null || !cartItems.Any())
        //        {
        //            return NoContent();
        //        }

        //        var products = await _productRepository.GetItems();
        //        if (products == null || !products.Any())
        //        {
        //            throw new Exception("No products exist in the system");
        //        }

        //        var productSizes = await _productRepository.GetSizes();
        //        if (productSizes == null || !productSizes.Any())
        //        {
        //            throw new Exception("No product sizes exist in the system");
        //        }

        //        var cartItemsDto = cartItems.ConvertToDto(products, productSizes);

        //        return Ok(cartItemsDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log the exception for debugging purposes

        //       Console.WriteLine($"Error in GetItems method: {ex}");

        //        return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        //    }
        //}

        [HttpGet("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDto>>> GetItems(int userId)
        {
            try
            {
                // Retrieve cart items for the user
                var cartItems = await _shoppingCartRepository.GetItemsByUser(userId);
                if (cartItems == null || !cartItems.Any())
                {
                    return NoContent();
                }

                var cartItemDtos = new List<CartItemDto>();
                foreach (var cartItem in cartItems)
                {
                    // Retrieve the corresponding product menu and product size
                    var productMenu = await _productRepository.GetItem(cartItem.ProductId);
                    var productSize = await _productRepository.GetItembySizeId(cartItem.SizeId);

                    if (cartItem.ExtraItemsId !=null && cartItem.ExtraItemsId.Length > 0 && cartItem.ExtraItemsId != " ")
                    {
                    var ExtraItem = await _productRepository.GetExtraItemsbyListid(cartItem.ExtraItemsId);
                        if (ExtraItem != null)
                        {
                            // Convert cart item to DTO
                            var cartItemDto = cartItem.ConvertToDto(productMenu, productSize, ExtraItem);
                            cartItemDtos.Add(cartItemDto);

                        }
                    }
                 
                    else
                    {
                        var cartItemDto = cartItem.ConvertToDto(productMenu, productSize);
                        cartItemDtos.Add(cartItemDto);
                    }

                }

                return Ok(cartItemDtos);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Error in GetItems method: {ex}");

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }



        [HttpGet("{id:int}/GetCartItem")]
        public async Task<ActionResult<CartItemDto>> GetCartItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.GetItemById(id);
                if (cartItem == null)
                {
                    return NotFound();
                }

                var product = await _productRepository.GetItem(cartItem.ProductId);
                if (product == null)
                {
                    return NotFound();
                }

                var productSizes = await _productRepository.GetItembySizeId(product.PizzaId);
                if (productSizes == null)
                {
                    return NotFound();
                }

                var cartItemDto = cartItem.ConvertToDto(product, productSizes);

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }




        [HttpPost("AddToCart")]
        public async Task<ActionResult<CartItemDto>> PostItem([FromBody] CartItemToAddDto cartItemToAddDto)
        {
            try
            {
                var newCartItem = await _shoppingCartRepository.AddItem(cartItemToAddDto); ;
                if (newCartItem == null)
                {
                    return NoContent();
                }
                var product = await _productRepository.GetItem(newCartItem.ProductId);
                if (product == null)
                {
                    throw new Exception($"Something went wrong when attempting to retrieve product (productId:({cartItemToAddDto.ProductId})");
                }
                var productSize = await _productRepository.GetItembySizeId(newCartItem.SizeId);
                if (productSize == null)
                {
                    return BadRequest();
                }
                var extraItems = await _productRepository.GetExtraItemsbyListid(cartItemToAddDto.ExtraItemIds);

                var newCartItemDto = newCartItem.ConvertToDto(product,productSize, extraItems);
                return CreatedAtAction("GetItems", new { userId = newCartItemDto.Id }, newCartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPatch("{id:int}")]
        public async Task<ActionResult<CartItemDto>> UpdateQty(int id, CartItemQtyUpdateDto quantityUpdateDto)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.UpdateQty(id, quantityUpdateDto);
                if (cartItem == null)
                {
                    return NotFound();
                }

               var cartItemDto = cartItem.ConvertToDtoUpdateQty();
                return Ok(cartItem);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPatch("{id:int}/DeletitemfromItemCart")]
        public async Task<ActionResult<CartIemDeletItemDto>> DeletitemfromItemCart(CartIemDeletItemDto cartIemDeletItemDto)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.DeletItemFromCartItem(cartIemDeletItemDto);
                if (cartItem == null)
                {
                    return NotFound(cartIemDeletItemDto);

                }
                var ItemFromCartItem = _productRepository.GetItem(cartItem.CartItemId);
                var cartItemDto = cartItem.ConvertToDto();
                return Ok(cartItemDto);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CartItemDto>> DeleteItem(int id)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.DeleteItem(id);
                if(cartItem == null)
                {
                    return NotFound();
                }
                var product = await _productRepository.GetItem(cartItem.ProductId);
                var cartItemDto = cartItem.ConvertToDto();

                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{id:int}/DeletExtraItemfromCartItem")]
        public async Task<ActionResult<CartItemForDeletExtraItemDto>> DeletExtraItemfromCartItem(CartItemForDeletExtraItemDto  cartItemForDeletExtraItemDto)
        {
            try
            {
                var carItem = await _shoppingCartRepository.DeletExstraItemFromCartItem(cartItemForDeletExtraItemDto.CartItemId, cartItemForDeletExtraItemDto.ExtraItemId);
                if (carItem == null)
                {
                    return NotFound();
                }
              var cartItemDto = carItem.ConvertToDto();
                return Ok(carItem);
            }
            catch (Exception)
            {

                throw;
            }
         
        }
    }
}
