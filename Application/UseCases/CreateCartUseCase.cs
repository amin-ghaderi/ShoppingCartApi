using ShoppingCartApi.Application.Interfaces;
using ShoppingCartApi.Domain.Entities;

namespace ShoppingCartApi.Application.UseCases;

public class CreateCartUseCase
{
    private readonly ICartRepository _cartRepository;

    public CreateCartUseCase(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public Cart Execute(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        var existing = _cartRepository.GetByUserId(userId);
        if (existing is not null)
        {
            return existing;
        }

        var cart = new Cart(userId);
        _cartRepository.Save(cart);
        return cart;
    }
}
