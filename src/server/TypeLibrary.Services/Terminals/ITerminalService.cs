using Tyle.Application.Common;
using Tyle.Application.Terminals.Requests;
using Tyle.Core.Terminals;

namespace Tyle.Application.Terminals;

public interface ITerminalService : ITypeService<TerminalType, TerminalTypeRequest>
{
}