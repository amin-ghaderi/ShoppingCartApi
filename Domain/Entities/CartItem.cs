namespace ShoppingCartApi.Domain.Entities;

public class CartItem
{
    public string ProductId { get; }

    public int Quantity { get; }

    public CartItem(string productId, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            throw new ArgumentException("Product id is required.", nameof(productId));
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(quantity),
                quantity,
                "Quantity must be greater than zero.");
        }

        ProductId = productId;
        Quantity = quantity;
    }
}
