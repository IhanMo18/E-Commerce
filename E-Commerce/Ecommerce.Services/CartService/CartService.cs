
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Repositories;

namespace Ecommerce.Services.CartService;

public class CartService(ICartRepository repository) : Service<Cart>(repository), ICartService
{
    public async Task<Cart?> SearchCartBySessionid(string sessionId)
    {
       return await repository.SearchCartBySessionid(sessionId);
    }

    public void AddProductToCart(string sessionId, int productId, int quantity)
    {
        repository.AddProductToCart(sessionId,productId,quantity);
    }
    
    public void RemoveProductFromCart(ProductCart productCart)
    {
        repository.RemoveProductFromCart(productCart);
    }
    
    
}