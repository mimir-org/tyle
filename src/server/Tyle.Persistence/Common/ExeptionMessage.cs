using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tyle.Persistence.Common
{
    public static class ExeptionMessage
    {
        public enum TypeOfMessage
        {
            Create, Add,Update,Remove
        }

        public static string CreateExeptionMessage( TypeOfMessage action,  string typeOfObject, string? id = null)
        {
            var sb = new StringBuilder();

            sb.Append($"Could not {action} {typeOfObject}");
            if (id != null)
                sb.Append($" with id {id}.");
            else sb.Append(".");

            sb.Append("Please check and try again later");

            return sb.ToString();
        }
    }

}