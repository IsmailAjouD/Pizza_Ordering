using Microsoft.AspNetCore.Mvc;
using Pizza_Ordaring.Infrastructure.Repositories;
using Pizza_Ordaring.Shared.Models;

namespace Pizza_OrderingAPI.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : Controller
    {
        ShoppingCartRepository _shoppingCartRepository = ShoppingCartRepository.Instance;



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("{userId}/GetItemsByUser")]
        public async Task<ActionResult<IEnumerable<CartItemModel>>> GetItemsByUser(int userId)
        {
            try
            {
                var cartItem = await _shoppingCartRepository.GetCartItemsbyUserId(userId);

            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}
