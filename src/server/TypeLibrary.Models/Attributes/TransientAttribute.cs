﻿using System;

namespace TypeLibrary.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class TransientAttribute : Attribute
    {
    }
}
