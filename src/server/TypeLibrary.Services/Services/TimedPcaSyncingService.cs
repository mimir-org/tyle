using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services;

public class TimedPcaSyncingService : IHostedService, IDisposable
{
    private bool _disposedValue;
    private readonly ILogger<TimedPcaSyncingService> _logger;
    private readonly IMapper _mapper;
    private readonly IServiceProvider _serviceProvider;
    private readonly IApplicationSettingsRepository _settings;
    private Timer _timer = null;
    private readonly IAttributeReferenceRepository _attributeReferenceRepository;
    private readonly IQuantityDatumReferenceRepository _quantityDatumReferenceRepository;
    private readonly IUnitReferenceRepository _unitReferenceRepository;

    public TimedPcaSyncingService(ILogger<TimedPcaSyncingService> logger, IMapper mapper, IServiceProvider serviceProvider, IApplicationSettingsRepository settings, IAttributeReferenceRepository attributeReferenceReferenceRepository, IQuantityDatumReferenceRepository quantityDatumReferenceRepository, IUnitReferenceRepository unitReferenceRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _serviceProvider = serviceProvider;
        _settings = settings;
        _attributeReferenceRepository = attributeReferenceReferenceRepository;
        _quantityDatumReferenceRepository = quantityDatumReferenceRepository;
        _unitReferenceRepository = unitReferenceRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var settingsSyncTime = DateTime.Parse(_settings.PcaSyncTime);
        var nextSyncTime = new DateTime(now.Year, now.Month, now.Day, settingsSyncTime.Hour, settingsSyncTime.Minute, settingsSyncTime.Second, settingsSyncTime.Millisecond);
        if (now > nextSyncTime)
            nextSyncTime = nextSyncTime.AddDays(1);

        _timer = new Timer(Sync, null, TimeSpan.FromSeconds((nextSyncTime - now).TotalSeconds), TimeSpan.FromDays(1));

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        InitialSync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

        return Task.CompletedTask;
    }

    private async Task InitialSync()
    {
        await Task.Delay(10000);
        _logger.LogInformation("Timed PCA syncing service started.");
        _logger.LogInformation($"After initial sync, syncing will be performed every day at {_settings.PcaSyncTime}");
        _logger.LogInformation("Performing initial PCA sync...");
        Sync(null);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Timed PCA syncing service stopped.");
        return Task.CompletedTask;
    }

    private async void Sync(object state)
    {
        _logger.LogInformation("Attempting to sync data from PCA...");
        _logger.LogInformation("--------------------------------------------------");
        await SyncUnits();
        _logger.LogInformation("--------------------------------------------------");
        await SyncAttributes();
        _logger.LogInformation("--------------------------------------------------");
        await SyncQuantityDatums();
        _logger.LogInformation("--------------------------------------------------");
        _logger.LogInformation("PCA sync completed.");
    }

    private async Task SyncUnits()
    {
        var pcaUnitsFetch = _unitReferenceRepository.FetchUnitsFromReference();
        using var scope = _serviceProvider.CreateScope();
        var unitService = scope.ServiceProvider.GetService<IUnitService>();
        var dbUnits = unitService.Get().ExcludeDeleted().ToList();

        var dbUnitReferences = new Dictionary<string, string>();
        foreach (var unit in dbUnits)
        {
            if (unit.TypeReference == null) continue;

            if (!unit.TypeReference.Contains("posccaesar.org")) continue;

            if (dbUnitReferences.ContainsKey(unit.TypeReference))
            {
                _logger.LogError("Duplicate PCA type references in Unit table.");
            }
            else
            {
                dbUnitReferences.Add(unit.TypeReference, unit.Id);
            }
        }

        var pcaUnits = await pcaUnitsFetch;
        _logger.LogInformation("Retrieved unit data from PCA...");

        var created = 0;

        foreach (var pcaUnit in pcaUnits)
        {
            if (dbUnitReferences.ContainsKey(pcaUnit.TypeReference))
            {
                var storedUnit = unitService.Get(dbUnitReferences[pcaUnit.TypeReference]);

                if (storedUnit.Equals(pcaUnit)) continue;

                _logger.LogError($"Unit with id {storedUnit.Id} deviates from PCA.");
            }
            else
            {
                await unitService.Create(pcaUnit, CreatedBy.PcaSyncJob);
                created++;
            }
        }

        _logger.LogInformation("Unit sync from PCA completed.");
        _logger.LogInformation($"Number of new units: {created}");
    }

    private async Task SyncAttributes()
    {
        var pcaAttributesFetch = _attributeReferenceRepository.FetchAttributesFromReference();
        using var scope = _serviceProvider.CreateScope();
        var attributeService = scope.ServiceProvider.GetService<IAttributeService>();
        var dbAttributes = attributeService.Get().ExcludeDeleted().ToList();

        var dbAttributeReferences = new Dictionary<string, string>();
        foreach (var attribute in dbAttributes)
        {
            if (attribute.TypeReference == null) continue;

            if (!attribute.TypeReference.Contains("posccaesar.org")) continue;

            if (dbAttributeReferences.ContainsKey(attribute.TypeReference))
            {
                _logger.LogError("Duplicate PCA type references in Attribute table.");
            }
            else
            {
                dbAttributeReferences.Add(attribute.TypeReference, attribute.Id);
            }
        }

        var pcaAttributes = await pcaAttributesFetch;
        _logger.LogInformation("Retrieved attribute data from PCA...");

        var created = 0;

        foreach (var pcaAttribute in pcaAttributes)
        {
            if (dbAttributeReferences.ContainsKey(pcaAttribute.TypeReference))
            {
                var storedAttribute = attributeService.Get(dbAttributeReferences[pcaAttribute.TypeReference]);

                if (AttributeIsUnchanged(storedAttribute, pcaAttribute)) continue;

                _logger.LogError($"Attribute with id {storedAttribute.Id} deviates from PCA.");
            }
            else
            {
                await attributeService.Create(pcaAttribute, CreatedBy.PcaSyncJob);
                created++;
            }
        }

        _logger.LogInformation("Attribute sync from PCA completed.");
        _logger.LogInformation($"Number of new attributes: {created}");
    }

    private static bool AttributeIsUnchanged(AttributeLibCm stored, AttributeLibAm external)
    {
        if (stored.Name != external.Name) return false;

        var storedDefaultUnit = stored.AttributeUnits.FirstOrDefault(x => x.IsDefault);
        var externalDefaultUnit = external.AttributeUnits.FirstOrDefault(x => x.IsDefault);

        if (storedDefaultUnit != null)
        {
            if (externalDefaultUnit == null)
            {
                return false;
            }
            if (storedDefaultUnit.Unit.Id != externalDefaultUnit.UnitId)
            {
                return false;
            }
        }

        var storedUnits = stored.AttributeUnits.Select(x => x.Unit.Id).ToList();
        storedUnits.Sort();
        var externalUnits = external.AttributeUnits.Select(x => x.UnitId).ToList();
        externalUnits.Sort();

        return storedUnits.SequenceEqual(externalUnits);
    }

    private async Task SyncQuantityDatums()
    {
        var pcaQuantityDatumsFetch = _quantityDatumReferenceRepository.FetchQuantityDatumsFromReference();
        using var scope = _serviceProvider.CreateScope();
        var quantityDatumService = scope.ServiceProvider.GetService<IQuantityDatumService>();
        var dbQuantityDatums = quantityDatumService.Get().ExcludeDeleted().ToList();

        var dbQuantityDatumsReferences = new Dictionary<string, string>();
        foreach (var quantityDatum in dbQuantityDatums)
        {
            if (quantityDatum.TypeReference == null) continue;

            if (!quantityDatum.TypeReference.Contains("posccaesar.org")) continue;

            if (dbQuantityDatumsReferences.ContainsKey(quantityDatum.TypeReference))
            {
                _logger.LogError("Duplicate PCA type references in QuantityDatum table.");
            }
            else
            {
                dbQuantityDatumsReferences.Add(quantityDatum.TypeReference, quantityDatum.Id);
            }
        }

        var pcaQuantityDatums = await pcaQuantityDatumsFetch;
        _logger.LogInformation("Retrieved quantity datum data from PCA...");

        var created = 0;

        foreach (var pcaQuantityDatum in pcaQuantityDatums)
        {
            if (dbQuantityDatumsReferences.ContainsKey(pcaQuantityDatum.TypeReference))
            {
                var storedQuantityDatum = quantityDatumService.Get(dbQuantityDatumsReferences[pcaQuantityDatum.TypeReference]);

                if (storedQuantityDatum.Equals(pcaQuantityDatum)) continue;

                _logger.LogError($"Quantity datum with id {storedQuantityDatum.Id} deviates from PCA.");
            }
            else
            {
                await quantityDatumService.Create(pcaQuantityDatum, CreatedBy.PcaSyncJob);
                created++;
            }
        }

        _logger.LogInformation("Quantity datum sync from PCA completed.");
        _logger.LogInformation($"Number of new quantity datums: {created}");
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
            _timer?.Dispose();

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}