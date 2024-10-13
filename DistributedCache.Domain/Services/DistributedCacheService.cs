using System.Text.Json;
using System.Text.Json.Serialization;
using DistributedCache.Domain.Interfaces;

namespace DistributedCache.Domain.Services;

public class DistributedCacheService(IDistributedRepository distributedRepository) : IDistributedCacheService
{
    public async Task<bool> SetData(string key, string value)
    {
        return await distributedRepository.SetData(key, value);
    }

    public async Task<T?> GetData<T>(string key)
    {
        return await distributedRepository.GetData<T>(key);
    }

    public async Task<bool> RemoveData(string key)
    {
        return await distributedRepository.RemoveData(key);
    }
}