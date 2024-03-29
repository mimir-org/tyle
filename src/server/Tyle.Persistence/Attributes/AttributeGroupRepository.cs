using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Attributes;

namespace Tyle.Persistence.Attributes;

public class AttributeGroupRepository : IAttributeGroupRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<AttributeGroup> _dbSet;
    private readonly IUserInformationService _userInformationService;

    public AttributeGroupRepository(TyleDbContext context, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.AttributeGroups;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<AttributeGroup>> GetAll()
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .ToListAsync();
    }

    public async Task<AttributeGroup?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Predicate)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.Attributes).ThenInclude(x => x.Attribute).ThenInclude(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AttributeGroup> Create(AttributeGroupRequest request)
    {
        var attributeGroup = new AttributeGroup
        {
            Name = request.Name,
            Description = request.Description,
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId()
        };

        attributeGroup.LastUpdateOn = attributeGroup.CreatedOn;

        attributeGroup.Attributes = request.AttributeIds.Select(attributeId => new AttributeGroupAttributeJoin { AttributeGroupId = attributeGroup.Id, AttributeId = attributeId }).ToList();

        _dbSet.Add(attributeGroup);
        await _context.SaveChangesAsync();

        return await Get(attributeGroup.Id);
    }

    public async Task<AttributeGroup?> Update(Guid id, AttributeGroupRequest request)
    {
        var attributeGroup = await _dbSet.AsTracking()
            .Include(x => x.Attributes)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (attributeGroup == null)
        {
            return null;
        }

        attributeGroup.Name = request.Name;
        attributeGroup.Description = request.Description;
        if (_userInformationService.GetUserId() != attributeGroup.CreatedBy)
        {
            attributeGroup.ContributedBy.Add(_userInformationService.GetUserId());
        }
        attributeGroup.LastUpdateOn = DateTimeOffset.Now;

        var attributeGroupAttributesToRemove = attributeGroup.Attributes.Where(x => !request.AttributeIds.Contains(x.AttributeId)).ToList();
        foreach (var attributeGroupAttribute in attributeGroupAttributesToRemove)
        {
            attributeGroup.Attributes.Remove(attributeGroupAttribute);
        }

        foreach (var attributeId in request.AttributeIds)
        {
            if (attributeGroup.Attributes.Any(x => x.AttributeId == attributeId)) continue;

            attributeGroup.Attributes.Add(new AttributeGroupAttributeJoin
            {
                AttributeGroupId = id,
                AttributeId = attributeId
            });
        }

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var attributeGroup = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (attributeGroup == null)
        {
            return false;
        }

        _dbSet.Remove(attributeGroup);
        await _context.SaveChangesAsync();

        return true;
    }
}