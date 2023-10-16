using System.Text;

namespace Tyle.Persistence.Common;

public static class ExceptionMessage
{
    public enum TypeOfMessage
    {
        Create, Add, Update, Remove
    }

    public static string CreateExceptionMessage(TypeOfMessage action, string typeOfObject, string? id = null)
    {
        var sb = new StringBuilder();

        sb.Append($"Could not {action.ToString().ToLower()} {typeOfObject}");
        if (id != null)
            sb.Append($" with id {id}.");
        else sb.Append(".");

        sb.Append(" Please check and try again later.");

        return sb.ToString();
    }
}