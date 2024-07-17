namespace PontoAPonto.Domain.Interfaces.Infra
{
    public interface IRedisService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
        Task<bool> ExistsAsync(string key);
    }
}
