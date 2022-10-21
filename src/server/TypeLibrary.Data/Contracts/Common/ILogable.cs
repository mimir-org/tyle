using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;

namespace TypeLibrary.Data.Contracts.Common
{
    public interface ILogable
    {
        LogLibAm CreateLog(LogType logType, string logTypeValue, string comment);
    }
}