using System;
using Mimirorg.Common.Contracts;
using Mimirorg.Common.Enums;

namespace TypeLibrary.Data.Models;

public class SymbolLibDm : IStatefulObject
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Iri { get; set; }
    public string TypeReference { get; set; }
    public State State { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public string Data { get; set; }
}