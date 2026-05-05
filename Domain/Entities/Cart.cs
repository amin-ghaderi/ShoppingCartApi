namespace ShoppingCartApi.Domain.Entities;

public class Cart
{
    private readonly List<CartItem> _items = new();

    public string Id { get; }

    public string UserId { get; }

    public IReadOnlyList<CartItem> Items => _items;

    public Cart(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new ArgumentException("User id is required.", nameof(userId));
        }

        Id = Guid.NewGuid().ToString("N");
        UserId = userId;
    }

    public void SetItems(List<CartItem> items)
    {
        ArgumentNullException.ThrowIfNull(items);

        ValidateItemQuantities(items);
        ValidateNoDuplicateProductIds(items);

        _items.Clear();
        _items.AddRange(items);
    }

    public void AddOrUpdateItem(string productId, int quantity)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            throw new ArgumentException("Product id is required.", nameof(productId));
        }

        if (quantity <= 0)
        {
            RemoveItem(productId);
            return;
        }

        var index = _items.FindIndex(i =>
            string.Equals(i.ProductId, productId, StringComparison.Ordinal));

        if (index >= 0)
        {
            _items[index] = new CartItem(productId, quantity);
        }
        else
        {
            _items.Add(new CartItem(productId, quantity));
        }
    }

    public void RemoveItem(string productId)
    {
        if (string.IsNullOrWhiteSpace(productId))
        {
            throw new ArgumentException("Product id is required.", nameof(productId));
        }

        _items.RemoveAll(i =>
            string.Equals(i.ProductId, productId, StringComparison.Ordinal));
    }

    public void Clear() => _items.Clear();

    private static void ValidateItemQuantities(IReadOnlyCollection<CartItem> items)
    {
        foreach (var item in items)
        {
            if (item.Quantity <= 0)
            {
                throw new ArgumentException(
                    "Cannot set cart items containing a quantity of zero or less.",
                    nameof(items));
            }
        }
    }

    private static void ValidateNoDuplicateProductIds(IReadOnlyCollection<CartItem> items)
    {
        var seen = new HashSet<string>(StringComparer.Ordinal);

        foreach (var item in items)
        {
            if (!seen.Add(item.ProductId))
            {
                throw new ArgumentException(
                    $"Duplicate product id '{item.ProductId}' is not allowed.",
                    nameof(items));
            }
        }
    }
}
