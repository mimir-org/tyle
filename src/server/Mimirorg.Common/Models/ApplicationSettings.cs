using System.Text;

namespace Mimirorg.Common.Models
{
    public class ApplicationSettings
    {
        public string ApplicationSemanticUrl { get; set; }
        public string ApplicationUrl { get; set; }
        public string System => "System";

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine("###################### Application settings #################################");
            sb.AppendLine($"{nameof(ApplicationSemanticUrl)}:   {ApplicationSemanticUrl}");
            sb.AppendLine($"{nameof(ApplicationUrl)}:           {ApplicationUrl}");
            sb.AppendLine("#############################################################################");
            return sb.ToString();
        }
    }
}