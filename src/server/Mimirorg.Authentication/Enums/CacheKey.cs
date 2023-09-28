using System.ComponentModel.DataAnnotations;

namespace Mimirorg.Authentication.Enums;

public enum CacheKey
{
    [Display(Name = "All")]
    All = 0,

    [Display(Name = "Block")]
    Block = 1,

    [Display(Name = "Attribute")]
    Attribute = 2,

    [Display(Name = "Attribute Predefined")]
    AttributePredefined = 3,

    [Display(Name = "Purpose")]
    Purpose = 4,

    [Display(Name = "Quantity Datum")]
    QuantityDatum = 5,

    [Display(Name = "Rds")]
    Rds = 6,

    [Display(Name = "Unit")]
    Unit = 7,

    [Display(Name = "Symbol")]
    Symbol = 8,

    [Display(Name = "Terminal")]
    Terminal = 9
}