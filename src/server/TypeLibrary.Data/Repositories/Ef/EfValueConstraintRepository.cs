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

    public async Task Update(ValueConstraint? valueConstraint, ValueConstraintLibAm? valueConstraintAm, Guid attributeId)
    {
        if (valueConstraint == null)
        {
            if (valueConstraintAm == null) return;

            var newValueConstraint = new ValueConstraint();
            newValueConstraint.SetConstraints(valueConstraintAm);
            newValueConstraint.AttributeId = attributeId;
            await CreateAsync(newValueConstraint);
            await SaveAsync();
        }
        else
        {
            if (valueConstraintAm == null)
            {
                await Delete(valueConstraint.Id);
            }
            else
            {
                var existingValueConstraint = await GetAsync(valueConstraint.Id);
                existingValueConstraint.SetConstraints(valueConstraintAm);
                Update(existingValueConstraint);
            }

            await SaveAsync();
        }
    }
}