using System;

namespace TypeLibrary.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public sealed class SingletonAttribute : Attribute
    {
    }
}
