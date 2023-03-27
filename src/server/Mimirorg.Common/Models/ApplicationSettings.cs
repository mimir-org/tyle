using System.Text;

namespace Mimirorg.Common.Models;

public class ApplicationSettings
{
    public string ApplicationSemanticUrl { get; set; }
    public string ApplicationUrl { get; set; }
    public string PcaSyncTime { get; set; }
    public string System => "System";

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.AppendLine(@"
        TTTTTTTTTTTTTTTTTTTTTTYYYYYYY       YYYYYYLLLLLLLLLLL            EEEEEEEEEEEEEEEEEEEEEE
        T:::::::::::::::::::::Y:::::Y       Y:::::L:::::::::L            E::::::::::::::::::::E
        T:::::::::::::::::::::Y:::::Y       Y:::::L:::::::::L            E::::::::::::::::::::E
        T:::::TT:::::::TT:::::Y::::::Y     Y::::::LL:::::::LL            EE::::::EEEEEEEEE::::E
        TTTTTT  T:::::T  TTTTTYYY:::::Y   Y:::::YYY L:::::L                E:::::E       EEEEEE
 ::::::         T:::::T          Y:::::Y Y:::::Y    L:::::L                E:::::E
 ::::::         T:::::T           Y:::::Y:::::Y     L:::::L                E::::::EEEEEEEEEE   
 ::::::         T:::::T            Y:::::::::Y      L:::::L                E:::::::::::::::E
                T:::::T             Y:::::::Y       L:::::L                E:::::::::::::::E
                T:::::T              Y:::::Y        L:::::L                E::::::EEEEEEEEEE
                T:::::T              Y:::::Y        L:::::L                E:::::E
 ::::::         T:::::T              Y:::::Y        L:::::L         LLLLLL E:::::E       EEEEEE
 ::::::       TT:::::::TT            Y:::::Y      LL:::::::LLLLLLLLL:::::EE::::::EEEEEEEE:::::E
 ::::::       T:::::::::T         YYYY:::::YYYY   L::::::::::::::::::::::E::::::::::::::::::::E
              T:::::::::T         Y:::::::::::Y   L::::::::::::::::::::::E::::::::::::::::::::E
              TTTTTTTTTTT         YYYYYYYYYYYYY   LLLLLLLLLLLLLLLLLLLLLLLEEEEEEEEEEEEEEEEEEEEEE
");
        sb.AppendLine("############################ Tyle Application settings #######################################");
        sb.AppendLine($"{nameof(ApplicationSemanticUrl)}:   {ApplicationSemanticUrl}");
        sb.AppendLine($"{nameof(ApplicationUrl)}:           {ApplicationUrl}");
        sb.AppendLine($"{nameof(PcaSyncTime)}:              {PcaSyncTime}");
        sb.AppendLine("############################ Tyle Application settings #######################################");
        return sb.ToString();
    }
}