using ShoppingCartApi.Application.Interfaces;
using ShoppingCartApi.Domain.Entities;
using ShoppingCartApi.Infrastructure.Data;

namespace ShoppingCartApi.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    public Cart? GetByUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return null;
        }

        return InMemoryDatabase.Carts.TryGetValue(userId, out var cart) ? cart : null;
    }

    public void Save(Cart cart)
    {
        ArgumentNullException.ThrowIfNull(cart);

        InMemoryDatabase.Carts[cart.UserId] = cart;
    }

    public void Delete(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            return;
        }

        InMemoryDatabase.Carts.Remove(userId);
    }
}
