using AngleSharp.Text;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using TypeLibrary.Data.Contracts.Common;

namespace TypeLibrary.Data.Models;

public class AttributeGroupLibDm
{
    public string Id { get; set; }
    public string Name { get; set; }
    public ICollection<AttributeGroupAttributesLibDm> AttributeGroupAttributes { get; set; }
    public ICollection<AttributeLibDm> Attributes { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public string Description { get; set; }

}