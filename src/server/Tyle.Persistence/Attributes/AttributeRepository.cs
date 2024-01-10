using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Abstractions;
using Tyle.Application.Attributes;
using Tyle.Application.Attributes.Requests;
using Tyle.Application.Common;
using Tyle.Converters;
using Tyle.Core.Attributes;
using Tyle.Core.Common;

namespace Tyle.Persistence.Attributes;

public class AttributeRepository : IAttributeRepository
{
    private readonly TyleDbContext _context;
    private readonly DbSet<AttributeType> _dbSet;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUserInformationService _userInformationService;

    public AttributeRepository(TyleDbContext context, IServiceProvider serviceProvider, IConfiguration configuration, IMapper mapper, IUserInformationService userInformationService)
    {
        _context = context;
        _dbSet = context.Attributes;
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _mapper = mapper;
        _userInformationService = userInformationService;
    }

    public async Task<IEnumerable<AttributeType>> GetAll(State? state = null)
    {
        var query = _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery();

        if (state != null)
        {
            query = query.Where(x => x.State == state);
        }

        return await query.ToListAsync();
    }

    public async Task<AttributeType?> Get(Guid id)
    {
        return await _dbSet.AsNoTracking()
            .Include(x => x.Predicate)
            .Include(x => x.Units).ThenInclude(x => x.Unit)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AttributeType> Create(AttributeTypeRequest request)
    {
        var attribute = new AttributeType
        {
            Name = request.Name,
            Description = request.Description,
            Version = "1.0",
            CreatedOn = DateTimeOffset.Now,
            CreatedBy = _userInformationService.GetUserId(),
            State = State.Draft,
            PredicateId = request.PredicateId,
            UnitMinCount = request.UnitMinCount,
            UnitMaxCount = request.UnitMaxCount,
            ProvenanceQualifier = request.ProvenanceQualifier,
            RangeQualifier = request.RangeQualifier,
            RegularityQualifier = request.RegularityQualifier,
            ScopeQualifier = request.ScopeQualifier,
            ValueConstraint = _mapper.Map<ValueConstraint>(request.ValueConstraint)
        };

        attribute.LastUpdateOn = attribute.CreatedOn;

        attribute.Units = request.UnitIds.Select(unitId => new AttributeUnitJoin { AttributeId = attribute.Id, UnitId = unitId }).ToList();

        _dbSet.Add(attribute);
        await _context.SaveChangesAsync();

        return await Get(attribute.Id);
    }

    public async Task<AttributeType?> Update(Guid id, AttributeTypeRequest request)
    {
        var attribute = await _dbSet.AsTracking()
            .Include(x => x.Units)
            .Include(x => x.ValueConstraint).ThenInclude(x => x!.ValueList)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return null;
        }

        if (attribute.State != State.Draft)
        {
            throw new InvalidOperationException($"Attributes with state '{attribute.State}' cannot be updated.");
        }

        attribute.Name = request.Name;
        attribute.Description = request.Description;
        if (_userInformationService.GetUserId() != attribute.CreatedBy)
        {
            attribute.ContributedBy.Add(_userInformationService.GetUserId());
        }
        attribute.LastUpdateOn = DateTimeOffset.Now;

        attribute.PredicateId = request.PredicateId;

        var attributeUnitsToRemove = attribute.Units.Where(x => !request.UnitIds.Contains(x.UnitId)).ToList();
        foreach (var attributeUnit in attributeUnitsToRemove)
        {
            attribute.Units.Remove(attributeUnit);
        }

        foreach (var unitId in request.UnitIds)
        {
            if (attribute.Units.Any(x => x.UnitId == unitId)) continue;

            attribute.Units.Add(new AttributeUnitJoin
            {
                AttributeId = id,
                UnitId = unitId
            });
        }

        attribute.UnitMinCount = request.UnitMinCount;
        attribute.UnitMaxCount = request.UnitMaxCount;

        attribute.ProvenanceQualifier = request.ProvenanceQualifier;
        attribute.RangeQualifier = request.RangeQualifier;
        attribute.RegularityQualifier = request.RegularityQualifier;
        attribute.ScopeQualifier = request.ScopeQualifier;

        var valueConstraintComparer = new ValueConstraintComparer();
        var requestedValueConstraint = _mapper.Map<ValueConstraint>(request.ValueConstraint);

        if (!valueConstraintComparer.Equals(attribute.ValueConstraint, requestedValueConstraint))
        {
            if (attribute.ValueConstraint != null)
            {
                if (attribute.ValueConstraint.ConstraintType == ConstraintType.In)
                {
                    _context.ValueListEntries.RemoveRange(attribute.ValueConstraint.ValueList);
                }
                _context.ValueConstraints.Remove(attribute.ValueConstraint);
            }

            attribute.ValueConstraint = requestedValueConstraint;
        }

        await _context.SaveChangesAsync();

        return await Get(id);
    }

    public async Task<bool> Delete(Guid id)
    {
        var attribute = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return false;
        }

        if (attribute.State == State.Approved)
        {
            throw new InvalidOperationException("Approved attributes cannot be deleted.");
        }

        _dbSet.Remove(attribute);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ChangeState(Guid id, State state)
    {
        var attribute = await _dbSet.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        if (attribute == null)
        {
            return false;
        }

        if (state == State.Approved && _configuration.GetValue<bool>("UseCommonLib"))
        {
            using var scope = _serviceProvider.CreateScope();

            var jsonLdConversionService = scope.ServiceProvider.GetService<IJsonLdConversionService>();
            var downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();

            if (jsonLdConversionService == null || downstreamApi == null)
            {
                throw new InvalidOperationException("Couldn't resolve the services needed to post to Common Library.");
            }

            var completeAttribute = await Get(id);

            if (completeAttribute == null)
            {
                return false;
            }

            var attributeAsJsonLd = await jsonLdConversionService.ConvertToJsonLd(completeAttribute);

            var postResponse = await downstreamApi.CallApiForAppAsync("CommonLib", options =>
            {
                options.HttpMethod = "POST";
                options.RelativePath = "/api/imftype/WriteImfType";
                options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";

                options.CustomizeHttpRequestMessage = message =>
                {
                    message.Content = new StringContent(attributeAsJsonLd.ToString(), System.Text.Encoding.UTF8, "application/json-patch+json");
                };
            });

            if (!postResponse.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("Post request to Common Library failed.");
            }
        }

        attribute.State = state;
        await _context.SaveChangesAsync();

        return true;
    }
}