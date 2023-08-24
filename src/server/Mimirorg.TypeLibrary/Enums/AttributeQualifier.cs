namespace Mimirorg.TypeLibrary.Enums;

public enum AttributeQualifier
{
    // Provenance Qualifiers
    CalculatedQualifier = 0,
    MeasuredQualifier = 1,
    SpecifiedQualifier = 2,

    // Range Qualifiers
    AverageQualifier = 10,
    MaximumQualifier = 11,
    MinimumQualifier = 12,
    NominalQualifier = 13,
    NormalQualifier = 14,

    // Regularity Qualifiers
    AbsoluteQualifier = 20,
    ContinuousQualifier = 21,

    // Scope Qualifiers
    DesignQualifier = 30,
    OperatingQualifier = 31
}
