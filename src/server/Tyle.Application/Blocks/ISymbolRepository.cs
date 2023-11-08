using Tyle.Core.Blocks;

namespace Tyle.Application.Blocks;

public interface ISymbolRepository
{
    /// <summary>
    /// Gets all symbols.
    /// </summary>
    /// <returns>An IEnumerable of all symbols.</returns>
    Task<IEnumerable<Symbol>> GetAll();

    /// <summary>
    /// Gets the symbol with the given id.
    /// </summary>
    /// <param name="id">The id of the symbol.</param>
    /// <returns>The symbol, or null if no symbol was found.</returns>
    Task<Symbol?> Get(int id);
}