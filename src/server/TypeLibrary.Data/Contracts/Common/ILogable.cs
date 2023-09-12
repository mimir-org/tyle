using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Domain;

namespace TypeLibrary.Data.Contracts.Common;

public interface ILogable
{
    LogLibDm CreateLog(LogType logType, string logTypeValue, string createdBy);
}