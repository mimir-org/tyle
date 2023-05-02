using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Contracts.Common;

public interface ILogable
{
    LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy);
}