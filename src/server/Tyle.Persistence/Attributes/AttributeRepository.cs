using System;
using System.Globalization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Tyle.Application.Attributes;
using Tyle.Core.Attributes;
using Tyle.Core.Attributes.ValueConstraints;

namespace Tyle.Persistence.Attributes;

public class AttributeRepository : IAttributeRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<AttributeDao> _dbSet;
    private readonly IMapper _mapper;

    public AttributeRepository(TyleDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Attributes;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AttributeType>> GetAll()
    {
        var attributeDaos = await _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.AttributeUnits)
            .ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint)
            .ThenInclude(x => x.ValueList)
            .AsSplitQuery()
            .ToListAsync();

        return _mapper.Map<List<AttributeType>>(attributeDaos);
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        var attributeDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

        if (attributeDao == null)
        {
            return null;
        }

        await _context.Entry(attributeDao).Reference(x => x.Predicate).LoadAsync();
        await _context.Entry(attributeDao).Collection(x => x.AttributeUnits).Query().Include(x => x.Unit).LoadAsync();
        await _context.Entry(attributeDao).Reference(x => x.ValueConstraint).Query().Include(x => x.ValueList).LoadAsync();

        return _mapper.Map<AttributeType>(attributeDao);
    }

    public async Task<AttributeType> Create(AttributeType attribute)
    {
        var attributeDao = _mapper.Map<AttributeDao>(attribute);

        if (attribute.ValueConstraint != null)
        {
            attributeDao.ValueConstraint!.ValueList = attribute.ValueConstraint switch
            {
                InDecimalValueList c => c.ValueList.Select(x => new ValueListEntryDao(attributeDao.Id, x.ToString("F19", CultureInfo.InvariantCulture))).ToList(),
                InIntegerValueList c => c.ValueList.Select(x => new ValueListEntryDao(attributeDao.Id, x.ToString())).ToList(),
                InIriValueList c => c.ValueList.Select(x => new ValueListEntryDao(attributeDao.Id, x.AbsoluteUri)).ToList(),
                InStringValueList c => c.ValueList.Select(x => new ValueListEntryDao(attributeDao.Id, x)).ToList(),
                _ => attributeDao.ValueConstraint!.ValueList
            };
        }

        await _dbSet.AddAsync(attributeDao);
        await _context.SaveChangesAsync();

        return await Get(attributeDao.Id);
    }

    public async Task<AttributeType> Update(AttributeType attribute)
    {
        var currentAttribute = await Get(attribute.Id) ?? throw new KeyNotFoundException($"No attribute type with id {attribute.Id} found.");
        var newAttributeDao = _mapper.Map<AttributeDao>(attribute);

        if (attribute.ValueConstraint != null)
        {
            newAttributeDao.ValueConstraint!.ValueList = attribute.ValueConstraint switch
            {
                InDecimalValueList c => c.ValueList.Select(x => new ValueListEntryDao(newAttributeDao.Id, x.ToString("F19", CultureInfo.InvariantCulture))).ToList(),
                InIntegerValueList c => c.ValueList.Select(x => new ValueListEntryDao(newAttributeDao.Id, x.ToString())).ToList(),
                InIriValueList c => c.ValueList.Select(x => new ValueListEntryDao(newAttributeDao.Id, x.AbsoluteUri)).ToList(),
                InStringValueList c => c.ValueList.Select(x => new ValueListEntryDao(newAttributeDao.Id, x)).ToList(),
                _ => newAttributeDao.ValueConstraint!.ValueList
            };
        }

        foreach (var unit in currentAttribute.Units)
        {
            if (newAttributeDao.AttributeUnits.FirstOrDefault(x => x.UnitId == unit.Id) != null) continue;

            var unitAttribute = new AttributeUnitDao(attribute.Id, unit.Id);
            _context.AttributeUnits.Attach(unitAttribute);
            _context.AttributeUnits.Remove(unitAttribute);
        }

        var valueConstraintStub = new ValueConstraintDao // This is a stub, only used to delete the old value constraint
        {
            AttributeId = attribute.Id,
            ConstraintType = "",
            DataType = ""
        };
        _context.ValueConstraints.Attach(valueConstraintStub);
        _context.ValueConstraints.Remove(valueConstraintStub);

        await _context.SaveChangesAsync();

        _dbSet.Attach(newAttributeDao);
        _context.Entry(newAttributeDao).State = EntityState.Modified;

        foreach (var unitAttribute in newAttributeDao.AttributeUnits)
        {
            if (currentAttribute.Units.FirstOrDefault(x => x.Id == unitAttribute.UnitId) != null) continue;

            await _context.AttributeUnits.AddAsync(unitAttribute);
        }

        await _context.SaveChangesAsync();

        return await Get(newAttributeDao.Id);
    }

    public async Task Delete(Guid id)
    {
        var attributeDao = await _dbSet.FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"No attribute type with id {id} found.");
        _dbSet.Remove(attributeDao);
        await _context.SaveChangesAsync();
    }
}
