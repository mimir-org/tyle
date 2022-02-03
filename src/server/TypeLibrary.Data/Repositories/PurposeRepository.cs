﻿using Mimirorg.Common.Abstract;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Data;

namespace TypeLibrary.Data.Repositories
{
    public class PurposeRepository : GenericRepository<TypeLibraryDbContext, PurposeLibDm>, IPurposeRepository
    {
        public PurposeRepository(TypeLibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
