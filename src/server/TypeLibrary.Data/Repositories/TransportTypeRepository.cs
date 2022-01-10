﻿using TypeLibrary.Models.Abstract;
using TypeLibrary.Models.Configurations;
using TypeLibrary.Models.Data.TypeEditor;
using TypeLibrary.Data.Contracts;

namespace TypeLibrary.Data.Repositories
{
    public class TransportTypeRepository : GenericRepository<TypeLibraryDbContext, TransportType>, ITransportTypeRepository
    {
        public TransportTypeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
