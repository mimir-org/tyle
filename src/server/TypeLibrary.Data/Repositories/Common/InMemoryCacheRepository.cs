using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Repositories.Common;

public class InMemoryCacheRepository : ICacheRepository
{
    /// <inheritdoc />
    public Queue<(string, string)> RefreshList { get; set; }

    private const int Seconds = 86400;
    private readonly IMemoryCache _memoryCache;
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _locks;

    public InMemoryCacheRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
        _locks = new ConcurrentDictionary<string, SemaphoreSlim>();
        RefreshList = new Queue<(string, string)>();
    }

    /// <inheritdoc />
    public Task DeleteCacheAsync(string key)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> item, int? seconds)
    {
        if (_memoryCache.TryGetValue(key, out T cacheEntry))
            return cacheEntry;

        int sec;

        switch (seconds)
        {
            case null:
                sec = Seconds;
                break;
            case <= 0:
                cacheEntry = await item();
                return cacheEntry;
            default:
                sec = seconds.Value;
                break;
        }

        var cacheLock = _locks.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        await cacheLock.WaitAsync();

        try
        {
            if (!_memoryCache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = await item();
                _memoryCache.Set(key, cacheEntry, DateTimeOffset.Now.AddSeconds(sec));
            }
        }
        finally
        {
            cacheLock.Release();
        }

        return cacheEntry;
    }
}