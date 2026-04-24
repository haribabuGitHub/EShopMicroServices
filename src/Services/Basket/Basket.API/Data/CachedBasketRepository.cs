using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Collections.Generic;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository,IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasket(UserName, cancellationToken);
            await cache.RemoveAsync(UserName, cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var cachebasket = await cache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrEmpty(cachebasket))
            {
                var deserialized = JsonSerializer.Deserialize<ShoppingCart>(cachebasket);
                if (deserialized != null)
                    return deserialized;
                // If deserialization produced null, continue to fetch from repository.
            }

            var basket = await basketRepository.GetBasket(UserName, cancellationToken)
                         ?? new ShoppingCart { UserName = UserName, Items = new List<ShoppingCartItem>() };

            await cache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(basket, cancellationToken);
             await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }
    }
}
