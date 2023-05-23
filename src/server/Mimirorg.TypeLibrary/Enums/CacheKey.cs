using System.ComponentModel.DataAnnotations;

namespace Mimirorg.TypeLibrary.Enums;

public enum CacheKey
{
    [Display(Name = "Aspect Object")]
    AspectObject = 0,

    [Display(Name = "Attribute Qualifier")]
    AttributeQualifier = 1,

    [Display(Name = "Attribute Condition")]
    AttributeCondition = 2,

    [Display(Name = "Attribute Source")]
    AttributeSource = 3,

    [Display(Name = "Attribute Format")]
    AttributeFormat = 4,

    [Display(Name = "Attribute")]
    Attribute = 5,

    [Display(Name = "Purpose")]
    Purpose = 6,

    [Display(Name = "Attribute Aspect")]
    AttributeAspect = 7,

    [Display(Name = "Unit")]
    Unit = 8,

    [Display(Name = "Rds")]
    Rds = 9,

    [Display(Name = "Terminal")]
    Terminal = 10,

    [Display(Name = "Simple Type")]
    SimpleType = 11,

    [Display(Name = "Attribute Predefined")]
    AttributePredefined = 12,

    [Display(Name = "Symbol")]
    Symbol = 13,

    [Display(Name = "Quantity Datum")]
    QuantityDatum = 14
}