using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypeLibrary.Data.Contracts.Common;

public interface ISparQlWebClient
{
    Task<List<T>> Get<T>(string url, string query) where T : class, new();
}