using System.ComponentModel.DataAnnotations.Schema;
using Tyle.Persistence.Common;

namespace Tyle.Persistence.Terminals;

[Table("Media")]
public class MediumDao : ReferenceDao
{
}
