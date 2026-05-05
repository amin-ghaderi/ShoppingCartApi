using ShoppingCartApi.Domain.Entities;

namespace ShoppingCartApi.Application.Interfaces;

public interface ICartRepository
{
    Cart? GetByUserId(string userId);

    void Save(Cart cart);

    void Delete(string userId);
}
