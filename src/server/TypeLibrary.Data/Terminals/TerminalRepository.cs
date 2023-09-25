using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Core.Terminals;
using TypeLibrary.Services.Terminals;
using TypeLibrary.Services.Terminals.Requests;

namespace TypeLibrary.Data.Terminals;

public class TerminalRepository : ITerminalRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<TerminalType> _dbSet;
    private readonly IMapper _mapper;

    public TerminalRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        //_dbSet = context.Terminals;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TerminalType>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<TerminalType?> Get(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<TerminalType> Create(TerminalTypeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<TerminalType?> Update(Guid id, TerminalTypeRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}
