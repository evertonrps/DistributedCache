namespace DistributedCache.Domain.Interfaces;

public interface IDistributedCacheService
{
    Task<bool> SetData(string key, string value);
    Task<T?> GetData<T>(string key);
    Task<bool> RemoveData(string key);
}