using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.Areas.Client.Controllers;

[Area("Client")]
public class CartController(ICartService cartService) : Controller
{
   

    
    public async Task<IActionResult> Index()
    {
        if (!Request.Cookies.TryGetValue("SessionId", out string? sessionId))
        {
            return BadRequest("Session ID not found");
        }
        var cart = await cartService.SearchCartBySessionid(Request.Cookies["SessionId"]);

        if (cart == null)
        {
            var newCart = new Cart() { ProductCart = new List<ProductCart>() };
            return View(newCart);
        }

        TempData["sessionId"] = Request.Cookies["sessionId"];
        return View(cart);
    }
    
        public async Task<IActionResult> AddToCart(int productId)
        {
            if(!Request.Cookies.TryGetValue("sessionId", out var sessionId)) return BadRequest();
            TempData["AddToCart"] = true;
            cartService.AddProductToCart(sessionId,productId,1);
            var cart = await cartService.SearchCartBySessionid(sessionId);
            cart.LastActionTime=DateTime.UtcNow;
            return  RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        public async Task<IActionResult> UpdateCartQuantity(int productId, int quantity, int change = 0)
        {
            if (!Request.Cookies.TryGetValue("sessionId", out var sessionId))
            {
                return BadRequest("No se encontró sessionId en la cookie.");
            }

            var cart = await cartService.SearchCartBySessionid(sessionId);
            
            var productCart = cart.ProductCart.FirstOrDefault(cp => cp.ProductId == productId);
            if (productCart == null)
            {
                return BadRequest("Producto no encontrado en el carrito.");
            }

            // Si se usaron los botones, ajusto la cantidad
            if (change != 0)
            {
                quantity = productCart.Quantity + change;
            }
            
            productCart.Quantity = Math.Max(1, quantity);
            cart.LastActionTime=DateTime.UtcNow;
            cartService.Save();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> DeleteProductOnCart(int productId)
        {
            if (!Request.Cookies.TryGetValue("sessionId", out var sessionId)) return BadRequest("No se encontró sessionId en la cookie.");
            
            var cart = await cartService.SearchCartBySessionid(Request.Cookies["sessionId"]);
            var productCart = cart.ProductCart.FirstOrDefault(cp => cp.ProductId == productId);
            
            if (productCart == null) return BadRequest("No existe ese producto en el carrito");
            
            cartService.RemoveProductFromCart(productCart);
            cart.LastActionTime=DateTime.UtcNow;
            cartService.Update(cart);
            cartService.Save();
            return RedirectToAction("Index");
        }
}