using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mimirorg.Common.Enums;
using Mimirorg.TypeLibrary.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Services.Services;

public class TimedPcaSyncingService : IHostedService, IDisposable
{
    private bool _disposedValue;
    private readonly ILogger<TimedPcaSyncingService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer = null;
    private readonly IAttributeReferenceRepository _attributeReferenceRepository;
    private readonly IUnitReferenceRepository _unitReferenceRepository;

    public TimedPcaSyncingService(ILogger<TimedPcaSyncingService> logger, IServiceProvider serviceProvider, IAttributeReferenceRepository attributeReferenceReferenceRepository, IUnitReferenceRepository unitReferenceRepository)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _attributeReferenceRepository = attributeReferenceReferenceRepository;
        _unitReferenceRepository = unitReferenceRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(Sync, null, TimeSpan.FromSeconds(10), TimeSpan.FromDays(1));
        _logger.LogInformation("Timed PCA syncing service started.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        _logger.LogInformation("Timed PCA syncing service started.");
        return Task.CompletedTask;
    }

    private async void Sync(object state)
    {
        _logger.LogInformation("Attempting to sync data from PCA...");
        var pcaUnitsFetch = _unitReferenceRepository.FetchUnitsFromReference();
        using var scope = _serviceProvider.CreateScope();
        var unitRepository = scope.ServiceProvider.GetService<IUnitRepository>();
        var dbUnits = unitRepository.Get().ToList();

        var dbUnitReferences = new Dictionary<string, int>();
        foreach (var unit in dbUnits)
        {
            if (unit.TypeReference == null) continue;

            if (unit.TypeReference.Contains("posccaesar.org"))
            {
                if (dbUnitReferences.ContainsKey(unit.TypeReference))
                {
                    _logger.LogError("Duplicate PCA type references in Unit table.");
                }
                else
                {
                    dbUnitReferences.Add(unit.TypeReference, unit.Id);
                }
            }
        }

        var pcaUnits = await pcaUnitsFetch;
        _logger.LogInformation("Retrieved data from PCA...");

        var idsOfUnitsToDelete = new List<int>();
        var unitsToCreate = new List<UnitLibDm>();

        foreach (var pcaUnit in pcaUnits)
        {
            if (dbUnitReferences.ContainsKey(pcaUnit.TypeReference))
            {
                var storedUnit = unitRepository.Get(dbUnitReferences[pcaUnit.TypeReference]);

                if (storedUnit.Equals(pcaUnit)) continue;

                idsOfUnitsToDelete.Add(storedUnit.Id);
            }
            unitsToCreate.Add(pcaUnit);
        }

        await unitRepository.ChangeState(State.Deleted, idsOfUnitsToDelete);
        await unitRepository.Create(unitsToCreate);

        _logger.LogInformation("Data sync from PCA completed.");
        _logger.LogInformation($"Number of updated units: {idsOfUnitsToDelete.Count}");
        _logger.LogInformation($"Number of new units: {unitsToCreate.Count - idsOfUnitsToDelete.Count}");
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
