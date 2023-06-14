using Microsoft.EntityFrameworkCore;

namespace Mimirorg.Common.Abstract;

public interface IProcRepository<TContext> where TContext : DbContext
{
    /// <summary>
    /// Entity framework database context
    /// </summary>
    TContext Context { get; set; }

    /// <summary>
    /// Execute procedure from database using it's name and params that is protected from the SQL injection attacks.
    /// </summary>
    /// <typeparam name="T">The type you want to cast from return value in stored proc</typeparam>
    /// <param name="storedProcName">Name of the procedure that should be executed.</param>
    /// <param name="procParams">Dictionary of params that procedure takes as parameters.</param>
    /// <returns>List of objects that are mapped in T type, returned by procedure.</returns>
    Task<List<T>> ExecuteStoredProc<T>(string storedProcName, Dictionary<string, object> procParams) where T : class;
}