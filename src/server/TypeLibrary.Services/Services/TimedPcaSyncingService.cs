using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
    private readonly IServiceProvider _serviceProvider;
    private readonly IApplicationSettingsRepository _settings;
    private Timer _timer;
    private readonly IAttributeReferenceRepository _attributeReferenceRepository;
    private readonly IUnitReferenceRepository _unitReferenceRepository;

    public TimedPcaSyncingService(ILogger<TimedPcaSyncingService> logger, IServiceProvider serviceProvider, IApplicationSettingsRepository settings, IAttributeReferenceRepository attributeReferenceRepository, IUnitReferenceRepository unitReferenceRepository)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _settings = settings;
        _attributeReferenceRepository = attributeReferenceRepository;
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
        _logger.LogInformation("PCA sync completed.");
    }

    private async Task SyncUnits()
    {
        var pcaUnitsFetch = _unitReferenceRepository.FetchUnitsFromReference();
        using var scope = _serviceProvider.CreateScope();
        var unitService = scope.ServiceProvider.GetService<IUnitService>();
        var dbUnits = unitService.Get().ToList();

        var dbUnitReferences = new Dictionary<string, string>();

        foreach (var unit in dbUnits)
        {
            if (unit.TypeReference == null)
                continue;

            if (!unit.TypeReference.Contains("posccaesar.org"))
                continue;

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
            if (dbUnitReferences.TryGetValue(pcaUnit.TypeReference, out var reference))
            {
                var storedUnit = unitService.Get(reference);

                if (storedUnit.Equals(pcaUnit))
                    continue;

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
        var dbAttributes = attributeService.Get().ToList();
        var dbAttributeReferences = new Dictionary<string, string>();

        foreach (var attribute in dbAttributes)
        {
            if (attribute.TypeReference == null)
                continue;

            if (!attribute.TypeReference.Contains("posccaesar.org"))
                continue;

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
            if (dbAttributeReferences.TryGetValue(pcaAttribute.TypeReference, out var reference))
            {
                var storedAttribute = attributeService.Get(reference);

                if (AttributeIsUnchanged(storedAttribute, pcaAttribute))
                    continue;

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
        if (stored.Name != external.Name)
            return false;

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