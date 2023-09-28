using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.Common.Extensions;
using System.Net.Http.Json;
using Mimirorg.Authentication.Enums;
using Mimirorg.Authentication.Models.Client;
using Mimirorg.Authentication.Models.Common;

namespace Mimirorg.Authentication.Services;

public class TimedHookService : IHostedService, ITimedHookService, IDisposable
{
    private bool _disposedValue;
    private Timer _timer = null!;
    private Timer _cleanupTimer = null!;
    private readonly ILogger<TimedHookService> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly HttpClient _httpClient;
    public Queue<CacheKey> HookQueue { get; set; }
    public bool IsMigrationFinished { get; set; }

    public TimedHookService(ILogger<TimedHookService> logger, IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
        _httpClient = new HttpClient();
        HookQueue = new Queue<CacheKey>();
    }

    /// <summary>
    /// When hosted service is starting
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Completed task</returns>
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        _cleanupTimer = new Timer(CleanUpUsers, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
        _logger.LogInformation("Timed hook service started.");
        return Task.CompletedTask;
    }

    /// <summary>
    /// When hosted services is stopping
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Completed task</returns>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Timed hook service stopped.");
        return Task.CompletedTask;
    }

    /// <summary>
    /// Send the hooks to all listeners
    /// </summary>
    /// <param name="state"></param>
    private async void DoWork(object state)
    {
        if (!HookQueue.TryDequeue(out var nextItem))
            return;

        using var scope = _scopeFactory.CreateScope();
        var companyService = scope.ServiceProvider.GetRequiredService<IMimirorgCompanyService>();
        var allHooks = await companyService.GetAllHooksForCache(nextItem);
        var companyWithAnAllHook = new List<int>();

        foreach (var hook in allHooks.OrderBy(x => x.Key).ToList())
        {

            if (companyWithAnAllHook.Contains(hook.CompanyId))
                continue;

            if (hook.Key == CacheKey.All)
            {
                companyWithAnAllHook.Add(hook.Company.Id);

                foreach (var cacheKey in EnumExtensions.AsEnumerable<CacheKey>().ToList())
                    SendCacheInvalidationToClient(hook, cacheKey);
            }
            else
            {
                SendCacheInvalidationToClient(hook, hook.Key);
            }
        }
    }

    /// <summary>
    /// Helper method to send the hooks to all listeners
    /// </summary>
    /// <param name="hook"></param>
    /// <param name="cacheKey"></param>
    private async void SendCacheInvalidationToClient(MimirorgHookCm hook, CacheKey cacheKey)
    {
        try
        {
            using var response = await _httpClient.PostAsJsonAsync(hook.Iri, new CacheInvalidation
            {
                Secret = hook.Company?.Secret,
                Key = cacheKey
            });

            if (!response.IsSuccessStatusCode)
                _logger.LogInformation($"Can't send CacheInvalidation data to {hook.Iri}. Code: {response.StatusCode}. Message: {response.ReasonPhrase}");
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Can't send CacheInvalidation data to {hook.Iri}. Message: {e.Message}");
        }
    }

    /// <summary>
    /// Cleanup users and tokens
    /// </summary>
    /// <param name="state"></param>
    private async void CleanUpUsers(object state)
    {
        if (!IsMigrationFinished) return;

        using var scope = _scopeFactory.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IMimirorgUserService>();
        await userService.RemoveUnconfirmedUsersAndTokens();
    }

    #region Disposable

    /// <summary>
    /// Dispose timed hook service
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue)
            return;

        if (disposing)
        {
            _timer?.Dispose();
            _cleanupTimer?.Dispose();
        }

        _disposedValue = true;
    }

    /// <summary>
    /// Dispose timed hook service
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion
}