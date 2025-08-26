using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace WiventoryAPI.Services
{
    /// <summary>
    /// Service for interacting with Redis cache.
    /// </summary>
    public class CacheService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _db;

        public CacheService()
        {
            var host = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "127.0.0.1";
            var port = Environment.GetEnvironmentVariable("REDIS_PORT") ?? "6379";
            var password = Environment.GetEnvironmentVariable("REDIS_PASSWORD");

            var options = ConfigurationOptions.Parse($"{host}:{port}");
            if (!string.IsNullOrWhiteSpace(password))
            {
                options.Password = password;
            }

            _redis = ConnectionMultiplexer.Connect(options);
            _db = _redis.GetDatabase();

            _redis.ConnectionFailed += (sender, e) =>
            {
                Console.WriteLine($"Redis connection failed: {e.Exception}");
            };
        }

        /// <summary>
        /// Set a value in Redis with optional TTL.
        /// </summary>
        public async Task SetAsync(string key, string value, TimeSpan? ttl = null)
        {
            if (ttl.HasValue)
            {
                await _db.StringSetAsync(key, value, ttl);
            }
            else
            {
                await _db.StringSetAsync(key, value);
            }
        }

        /// <summary>
        /// Get a value from Redis by key.
        /// </summary>
        public async Task<string?> GetAsync(string key)
        {
            return await _db.StringGetAsync(key);
        }

        /// <summary>
        /// Checks and sets resend cooldown for a given key.
        /// </summary>
        public async Task<(bool allowed, int? secondsLeft)> CheckCooldownAsync(string key, int ttlSeconds)
        {
            var redisKey = $"cooldown:{key.ToLower()}";
            var ttl = await _db.KeyTimeToLiveAsync(redisKey);

            if (ttl.HasValue && ttl.Value.TotalSeconds > 0)
            {
                return (false, (int)ttl.Value.TotalSeconds);
            }
            else
            {
                await _db.StringSetAsync(redisKey, "1", TimeSpan.FromSeconds(ttlSeconds));
                return (true, null);
            }
        }

        /// <summary>
        /// Delete a key from Redis.
        /// </summary>
        public async Task DeleteAsync(string key)
        {
            await _db.KeyDeleteAsync(key);
        }
    }
}
