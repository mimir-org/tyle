using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Mimirorg.Common.Abstract;

public abstract class ProcRepository<TContext> : IProcRepository<TContext> where TContext : DbContext
{
    /// <summary>
    /// Entity framework database context
    /// </summary>
    public TContext Context { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="dbContext">Entity framework database context</param>
    protected ProcRepository(TContext dbContext)
    {
        Context = dbContext;
    }

    /// <summary>
    /// Execute procedure from database using it's name and params that is protected from the SQL injection attacks.
    /// </summary>
    /// <typeparam name="T">The type you want to cast from return value in stored proc</typeparam>
    /// <param name="storedProcName">Name of the procedure that should be executed.</param>
    /// <param name="procParams">Dictionary of params that procedure takes as parameters.</param>
    /// <returns>List of objects that are mapped in T type, returned by procedure.</returns>
    public async Task<List<T>> ExecuteStoredProc<T>(string storedProcName, Dictionary<string, object> procParams) where T : class
    {
        if (!Context.Database.IsRelational())
            return new List<T>();

        var conn = Context.Database.GetDbConnection();
        try
        {
            if (conn.State != ConnectionState.Open)
                await conn.OpenAsync();

            await using var command = conn.CreateCommand();
            command.CommandText = storedProcName;
            command.CommandType = CommandType.StoredProcedure;

            foreach (var procParam in procParams)
            {
                var param = command.CreateParameter();
                param.ParameterName = procParam.Key;
                param.Value = procParam.Value;
                command.Parameters.Add(param);
            }

            var reader = await command.ExecuteReaderAsync();
            var objList = new List<T>();
            var props = typeof(T).GetRuntimeProperties().ToList();
            var colMapping = reader.GetColumnSchema()
                .Where(x => props.Any(y =>
                    string.Equals(y.Name, x.ColumnName, StringComparison.CurrentCultureIgnoreCase)))
                .ToDictionary(key => key.ColumnName.ToLower());

            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
                {
                    var obj = Activator.CreateInstance<T>();
                    foreach (var prop in props)
                    {
                        var columnOrdinal = colMapping[prop.Name.ToLower()].ColumnOrdinal;
                        if (columnOrdinal == null) continue;
                        var val = reader.GetValue(columnOrdinal.Value);
                        prop.SetValue(obj, val == DBNull.Value ? null : val);
                    }

                    objList.Add(obj);
                }
            }

            await reader.DisposeAsync();

            return objList;
        }
        finally
        {
            await conn.CloseAsync();
        }
    }
}