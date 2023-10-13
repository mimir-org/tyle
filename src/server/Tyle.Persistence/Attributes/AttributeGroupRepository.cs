using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

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

    public async Task<ApiResponse<AttributeGroup>> Create(AttributeGroupRequest request)
    {
        var attrbuteReturnItem = new ApiResponse<AttributeGroup>();

        var attributeGroup = new AttributeGroup
        {
            Name = request.Name,
            Description = request.Description,
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId()
        };

        attributeGroup.LastUpdateOn = attributeGroup.CreatedOn;

        foreach (var attributeId in request.AttributeIds)
        {
            if (await _context.Attributes.AsNoTracking().AnyAsync(x => x.Id == attributeId))
            {
                attributeGroup.Attributes.Add(new AttributeGroupAttributeJoin
                {
                    AttributeGroupId = attributeGroup.Id,
                    AttributeId = attributeId
                });
            }
            else
            {
                attrbuteReturnItem.ErrorMessage.Add($"Could not find and add one or more attributes to the attribute group. Please ensure that the attribute you add exists.");
            }
        }

        _dbSet.Add(attributeGroup);
        await _context.SaveChangesAsync();

        attrbuteReturnItem.TValue = await Get(attributeGroup.Id);

        return attrbuteReturnItem;
    }

    public async Task<ApiResponse<AttributeGroup?>> Update(Guid id, AttributeGroupRequest request)
    {
        var attrbuteReturnItem = new ApiResponse<AttributeGroup>();

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

            if (await _context.Attributes.AsNoTracking().AnyAsync(x => x.Id == attributeId))
            {
                attributeGroup.Attributes.Add(new AttributeGroupAttributeJoin
                {
                    AttributeGroupId = id,
                    AttributeId = attributeId
                });
            }
            else
            {
                attrbuteReturnItem.ErrorMessage.Add($"Something happened during updating the attribute group {request.Name}. Could not find one or more of the attributes.");
            }
        }

        await _context.SaveChangesAsync();
        attrbuteReturnItem.TValue = await Get(id);

        return attrbuteReturnItem!;
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