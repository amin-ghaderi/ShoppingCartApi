using ShoppingCartApi.Application.Interfaces;

namespace ShoppingCartApi.Application.UseCases;

public class DeleteCartUseCase
{
    private readonly ICartRepository _cartRepository;

    public DeleteCartUseCase(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public void Execute(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        _cartRepository.Delete(userId);
    }
}
