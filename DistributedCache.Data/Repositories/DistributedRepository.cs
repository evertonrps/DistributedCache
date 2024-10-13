using System.Text;
using System.Text.Json;
using DistributedCache.Domain;
using DistributedCache.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace DistributedCache.Data.Repositories;

public class DistributedRepository(IDistributedCache distributedCache) : IDistributedRepository
{
    private readonly DistributedCacheEntryOptions _options = new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
        SlidingExpiration = TimeSpan.FromSeconds(1200)
    };

    public async Task<bool> SetData(string key, string value)
    {
        try
        {
            var product = new Product { Id = key, Description = value };
            await distributedCache.SetStringAsync(key, JsonSerializer.Serialize(product));
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<T?> GetData<T>(string key)
    {
        try
        {
            var jsonData = await distributedCache.GetStringAsync(key);

            return jsonData is null ? default(T) : JsonSerializer.Deserialize<T>(jsonData);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> RemoveData(string key)
    {
        try
        {
            await distributedCache.RemoveAsync(key);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}