using ShoppingCartApi.Application.Interfaces;
using ShoppingCartApi.Domain.Entities;

namespace ShoppingCartApi.Application.UseCases;

public class UpdateCartUseCase
{
    private readonly ICartRepository _cartRepository;

    public UpdateCartUseCase(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public Cart Execute(string userId, List<(string productId, int quantity)> items)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        ArgumentNullException.ThrowIfNull(items);

        var cart = _cartRepository.GetByUserId(userId);
        if (cart is null)
        {
            cart = new Cart(userId);
        }

        var cartItems = items
            .Select(i => new CartItem(i.productId, i.quantity))
            .ToList();

        cart.SetItems(cartItems);
        _cartRepository.Save(cart);
        return cart;
    }
}
