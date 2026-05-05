using ShoppingCartApi.Application.Interfaces;
using ShoppingCartApi.Domain.Entities;

namespace ShoppingCartApi.Application.UseCases;

public class GetCartUseCase
{
    private readonly ICartRepository _cartRepository;

    public GetCartUseCase(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public Cart? Execute(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        return _cartRepository.GetByUserId(userId);
    }
}
