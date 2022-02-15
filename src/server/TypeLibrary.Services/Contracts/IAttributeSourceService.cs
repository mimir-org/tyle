﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;

namespace TypeLibrary.Services.Contracts
{
    public interface IAttributeSourceService
    {
        Task<IEnumerable<AttributeSourceLibCm>> GetAttributeSources();
        Task<AttributeSourceLibCm> UpdateAttributeSource(AttributeSourceLibAm dataAm);
        Task<AttributeSourceLibCm> CreateAttributeSource(AttributeSourceLibAm dataAm);
        Task CreateAttributeSources(List<AttributeSourceLibAm> dataAm);
    }
}