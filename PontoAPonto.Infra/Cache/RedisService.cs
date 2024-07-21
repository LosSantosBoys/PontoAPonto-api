using Newtonsoft.Json;
using PontoAPonto.Domain.Interfaces.Infra;
using PontoAPonto.Domain.Models.Configs;
using StackExchange.Redis;
using System.ComponentModel;

namespace PontoAPonto.Data.Cache
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly RedisConfig _redisConfig;

        public RedisService(RedisConfig redisConfig)
        {
            _redisConfig = redisConfig;
            _redis = ConnectionMultiplexer.Connect($"{_redisConfig.HostName}:{_redisConfig.PortNumber},password={_redisConfig.Password}");
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