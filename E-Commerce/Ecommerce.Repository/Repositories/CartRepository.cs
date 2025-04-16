using Microsoft.EntityFrameworkCore;
using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Models;

namespace Ecommerce.Repository.Repositories;

public class CartRepository(ApplicationDbContext dbContext) : Repository<Cart>(dbContext),ICartRepository
{


    public Task<Cart?> SearchCartBySessionid(string? sessionId)
    {
        return _dbContext.Cart.Include(c => c.ProductCart)
            .ThenInclude(cp => cp.Product).FirstOrDefaultAsync(c => c.SessionId == sessionId);
    }

    public void AddProductToCart(string sessionId, int productId, int quantity)
    {
        var cart = _dbContext.Cart.FirstOrDefault(c=>c.SessionId==sessionId);

        if (cart == null)
        {
            cart = new Cart()
            {
                LastActionTime = DateTime.UtcNow,
                SessionId = sessionId
            };
            _dbContext.Add(cart);
            _dbContext.SaveChanges(); 
        }
        
        var productCart = _dbContext.ProductCarts
            .FirstOrDefault(cp => cp.CartId == cart.CartId && cp.ProductId == productId);
        
        if (productCart != null)
        {
            productCart.Quantity += quantity;
        }
        else
        {
            _dbContext.ProductCarts.Add(new ProductCart()
            {
                CartId = cart.CartId,
                ProductId = productId,
                Quantity = quantity
            });
            cart.LastActionTime = DateTime.UtcNow;
            
        }
        cart.LastActionTime = DateTime.UtcNow;
        _dbContext.Update(cart);
        _dbContext.SaveChanges();
    }

    public void RemoveProductFromCart(ProductCart productCart)
    {
        _dbContext.ProductCarts.Remove(productCart);
    }
}