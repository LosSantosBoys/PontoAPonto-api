using Newtonsoft.Json;
using PontoAPonto.Domain.Interfaces.Infra;
using StackExchange.Redis;
using System.ComponentModel;

namespace PontoAPonto.Data.Cache
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisService()
        {
            var PASSWORD = "0"; // TODO - CHANGE KEY
            var PORT_NUMBER = "0";
            var HOST_NAME = "0";
            _redis = ConnectionMultiplexer.Connect($"{HOST_NAME}:{PORT_NUMBER},password={PASSWORD}");
            _database = _redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            await _database.StringSetAsync(key, serializedValue, expiration);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }
    }
}