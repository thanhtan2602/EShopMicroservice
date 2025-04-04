using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        PopulateCategories(session);
        PopulateProducts(session);
    }

    private async void PopulateProducts(IDocumentSession session)
    {
        if (session.Query<Product>().Any())
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private async void PopulateCategories(IDocumentSession session)
    {
        if (session.Query<Category>().Any())
            return;

        session.Store<Category>(GetPreconfiguredCategories());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Category> GetPreconfiguredCategories() => new List<Category>()
    {
        new Category()
        {
            Id = new Guid(),
            Name = "Smart Phone",
            Description = "Smart Phone",
            ImageFile = "product-1.png"
        },
        new Category()
        {
            Id = new Guid(),
            Name = "White Appliances",
            Description = "White Appliances",
            ImageFile = "product-2.png"
        },
        new Category()
        {
            Id = new Guid(),
            Name = "Home Kitchen",
            Description = "Home Kitchen",
            ImageFile = "product-3.png"
        },
        new Category()
        {
            Id = new Guid(),
            Name = "Camera",
            Description = "Camera",
            ImageFile = "product-4.png"
        }
    };


    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = new Guid(),
            Name = "IPhone X",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-1.png",
            Price = 950.00M,
            CategoryId = new Guid()
        },
        new Product()
        {
            Id = new Guid(),
            Name = "Samsung 10",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-2.png",
            Price = 840.00M,
        },
        new Product()
        {
            Id = new Guid(),
            Name = "Huawei Plus",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-3.png",
            Price = 650.00M,
            CategoryId = new Guid()
        },
        new Product()
        {
            Id = new Guid(),
            Name = "Xiaomi Mi 9",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-4.png",
            Price = 470.00M,
            CategoryId = new Guid()
        },
        new Product()
        {
            Id = new Guid(),
            Name = "HTC U11+ Plus",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-5.png",
            Price = 380.00M,
            CategoryId = new Guid()
        },
        new Product()
        {
            Id = new Guid(),
            Name = "LG G7 ThinQ",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-6.png",
            Price = 240.00M,
            CategoryId = new Guid()
        },
        new Product()
        {
            Id = new Guid(),
            Name = "Panasonic Lumix",
            Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
            ImageFile = "product-6.png",
            Price = 240.00M,
            CategoryId = new Guid()
        }
    };

}