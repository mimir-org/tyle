using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypeLibrary.Data.Contracts.Common
{
    public interface ICacheRepository
    {
        /// <summary>
        /// Refresh queue
        /// The id and iri of project that should refresh cache
        /// </summary>
        Queue<(string, string)> RefreshList { get; set; }

        /// <summary>
        /// Delete from cache based on key
        /// </summary>
        /// <param name="key">The cache key to delete</param>
        /// <returns>Completed Task</returns>
        Task DeleteCacheAsync(string key);

        /// <summary>
        /// Get or create cache
        /// </summary>
        /// <typeparam name="T">Generic return value of function param</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="item">Function param that create the cache</param>
        /// <param name="seconds">Override lifetime cache</param>
        /// <returns>T value</returns>
        Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> item, int? seconds = null);
    }
}