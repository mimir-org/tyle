using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Abstractions;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.External.Model;

namespace Tyle.External;

public class CommonLibSyncingService : IHostedService, IDisposable
{
    private bool _disposedValue;
    private readonly IServiceProvider _serviceProvider;
    private Timer? _timer = null;

    public CommonLibSyncingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var nextSyncTime = new DateTime(now.Year, now.Month, now.Day, 2, 0, 0).AddDays(1);

        _timer = new Timer(Sync, null, TimeSpan.FromSeconds((nextSyncTime - now).TotalSeconds), TimeSpan.FromDays(1));

        Sync(null);

        return Task.CompletedTask;
    }

    private async void Sync(object? state)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();

        IDownstreamApi? downstreamApi = scope.ServiceProvider.GetService<IDownstreamApi>();
        IPurposeRepository? purposeRepository = scope.ServiceProvider.GetService<IPurposeRepository>();

        if (downstreamApi == null || purposeRepository == null)
        {
            return;
        }

        var purposes = await downstreamApi.GetForAppAsync<IEnumerable<ExternalType>>("CommonLib", options =>
        {
            options.RelativePath = "api/Code/IMFPurpose";
            options.AcquireTokenOptions.AuthenticationOptionsName = "AzureAd";
        });

        if (purposes == null)
        {
            return;
        }

        var purposesDb = await purposeRepository.GetAll();

        var newPurposes = purposes.Where(purpose => purposesDb.All(purposeDb => purposeDb.Iri != new Uri(purpose.Identity)));

        foreach (var purpose in newPurposes)
        {
            var purposeRequest = new RdlPurposeRequest
            {
                Name = purpose.Name,
                Description = purpose.Description.Length > 0 ? purpose.Description : null,
                Iri = purpose.Identity
            };

            await purposeRepository.Create(purposeRequest);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            _timer?.Dispose();
        }

        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
