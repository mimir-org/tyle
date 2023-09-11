using System;
using System.Threading.Tasks;
using Mimirorg.Common.Abstract;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.Ef;

public class EfValueConstraintRepository : GenericRepository<TypeLibraryDbContext, ValueConstraint>, IEfValueConstraintRepository
{
    public EfValueConstraintRepository(TypeLibraryDbContext dbContext) : base(dbContext)
    {
    }

    public async Task Update(ValueConstraint? valueConstraint, ValueConstraintRequest? request, Guid attributeId)
    {
        if (valueConstraint == null)
        {
            if (request == null) return;

            var newValueConstraint = new ValueConstraint();
            newValueConstraint.SetConstraints(request);
            newValueConstraint.AttributeId = attributeId;
            await CreateAsync(newValueConstraint);
            await SaveAsync();
        }
        else
        {
            if (request == null)
            {
                await Delete(valueConstraint.Id);
            }
            else
            {
                var existingValueConstraint = await GetAsync(valueConstraint.Id);
                existingValueConstraint.SetConstraints(request);
                Update(existingValueConstraint);
            }

            await SaveAsync();
        }
    }
}