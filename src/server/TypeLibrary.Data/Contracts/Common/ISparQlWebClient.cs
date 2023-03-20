using System.Collections.Generic;

namespace TypeLibrary.Data.Contracts.Common;

public interface ISparQlWebClient
{
    IEnumerable<T> Get<T>(string url, string query) where T : class, new();
}