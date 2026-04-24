using Marten.Schema;

namespace Catelog.API.Data
{
    public class CatelogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Name = "IPhone X",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 999.00M,
                ImageFile = "product-1.png",
                Catelog = "Smart Phone",
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                Name = "Samsung 10",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 899.00M,
                ImageFile = "product-2.png",
                Catelog = "Smart Phone",
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
            new Product
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Huawei Plus",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Price = 799.00M,
                ImageFile = "product-3.png",
                Catelog = "Smart Phone",
                Category = new List<string> { "Smart Phone", "Electronics" }
            },
             new Product
             {
                 Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                 Name = "Xiaomi Mi 9",
                 Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                 Price = 699.00M,
                 ImageFile = "product-4.png",
                 Catelog = "Smart Phone",
                 Category = new List<string> { "Smart Phone", "Electronics" }
             },
              new Product
              {
                  Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                  Name = "HTC U11+ Plus",
                  Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                  Price = 599.00M,
                  ImageFile = "product-5.png",
                  Catelog = "Smart Phone",
                  Category = new List<string> { "Smart Phone", "Electronics" }
              },
               new Product
               {
                   Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                   Name = "LG G7 ThinQ",
                   Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                   Price = 499.00M,
                   ImageFile = "product-6.png",
                   Catelog = "Smart Phone",
                   Category = new List<string> { "Smart Phone", "Electronics" }
               }    
        };
    }
}

