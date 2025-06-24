using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartController(ICartService cartService) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartAsync(string id)
        {
            ShoppingCart? cart = await cartService.GetCartAsync(id);

            return Ok(cart ?? new ShoppingCart { Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        {
            ShoppingCart? updatedCart = await cartService.SetCartAsync(cart);

            if(updatedCart == null)
            {
                return BadRequest("Failed to update the cart.");
            }

            return updatedCart;
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCart(string id)
        {
            bool result = await cartService.DeleteCartAsync(id);

            if (!result)
            {
                return NotFound("Problem deleting cart.");
            }

            return Ok();
        }
    }
}
