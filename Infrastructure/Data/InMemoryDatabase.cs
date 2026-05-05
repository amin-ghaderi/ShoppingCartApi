using ShoppingCartApi.Domain.Entities;

namespace ShoppingCartApi.Infrastructure.Data;

public static class InMemoryDatabase
{
    public static readonly Dictionary<string, Cart> Carts = new(StringComparer.Ordinal);

    public static readonly List<Product> Products = SeedProducts();

    private static List<Product> SeedProducts()
    {
        return new List<Product>
        {
            new("p1", "BILLY Bookcase", 129.99m),
            new("p2", "MALM Bed Frame", 249.00m),
            new("p3", "KALLAX Shelf Unit (4×4)", 189.00m),
            new("p4", "EKTORP Sofa (3-seat)", 799.00m),
            new("p5", "POÄNG Armchair", 99.00m),
            new("p6", "LACK Coffee Table", 39.99m),
            new("p7", "HEMNES Nightstand", 79.99m),
            new("p8", "STEFAN Chair", 24.99m),
            new("p9", "BEKVÄM Step Stool", 19.99m),
            new("p10", "FÖRNUFT Flatware Set", 4.99m),
        };
    }
}
