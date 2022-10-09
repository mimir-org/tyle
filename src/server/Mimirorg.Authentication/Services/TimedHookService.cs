using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mimirorg.Authentication.Contracts;
using Mimirorg.TypeLibrary.Enums;
using Mimirorg.TypeLibrary.Models.Common;

namespace Mimirorg.Authentication.Services
{
    public class TimedHookService : IHostedService, ITimedHookService, IDisposable
    {
        private bool _disposedValue;
        private Timer _timer = null!;
        private Timer _cleanupTimer = null!;
        private readonly ILogger<TimedHookService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly HttpClient _httpClient;
        public Queue<CacheKey> HookQueue { get; set; }

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


            foreach (var hook in allHooks)
            {
                var data = new CacheInvalidation
                {
                    Secret = hook.Company?.Secret,
                    Key = hook.Key
                };

                try
                {
                    using var response = await _httpClient.PostAsJsonAsync(hook.Iri, data);
                    if (!response.IsSuccessStatusCode)
                        _logger.LogInformation($"Can't send CacheInvalidation data to {hook.Iri}. Code: {response.StatusCode}. Message: {response.ReasonPhrase}");
                }
                catch (Exception e)
                {
                    _logger.LogInformation($"Can't send CacheInvalidation data to {hook.Iri}. Message: {e.Message}");
                }
            }
        }

        /// <summary>
        /// Cleanup users and tokens
        /// </summary>
        /// <param name="state"></param>
        private async void CleanUpUsers(object state)
        {
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
}