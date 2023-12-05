using Azure.Core;
using Azure.Identity;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Tyle.External.Model;

namespace Tyle.External
{
    public class CommonLibClient
    {

        private readonly string _apiBaseAddress;
        private readonly IRequestSender _requestSender;

        internal CommonLibClient(IRequestSender requestSender, string apiBaseAddress = null)
        {
            _requestSender = requestSender;
            _apiBaseAddress = apiBaseAddress;
        }

        public CommonLibClient(CommonLibraryClientOptions options, TokenCredential tokenCredential, HttpClient httpClient = null)
                : this(apiBaseAddress: options.CommonLibraryApiBaseAddress, requestSender: new ConnectionHandler(options, httpClient, tokenCredential))
        {
        }

        public async Task<List<ExternalType>> CodeAsync(string library, string scope = null, string name = null, string description = null, bool? isValid = null, string filter = null)
        {
            var response = await GetAsync<string>($"/api/Code/{library}?scope={scope}&name={name}&description={description}&isValid={isValid}&$filter={filter}");
            return response;
        }


        private async Task<List<ExternalType>> GetAsync<TResponse>(FormattableString address)
        {
            HttpResponseMessage response = await _requestSender.SendRequest(HttpMethod.Get, BuildSafeUrl(address));
            await EnsureSuccessStatusCode(response);

            return await response.Content.ReadFromJsonAsync<List<ExternalType>>();

        }

        private string BuildSafeUrl(FormattableString urlFormat)
        {
            string text = null;
            if (urlFormat.ArgumentCount == 0)
            {
                text = urlFormat.Format;
            }
            else
            {
                object[] args = (from a in urlFormat.GetArguments()
                                 select FormattableString.Invariant($"{a}")).Select(Uri.EscapeDataString).Cast<object>().ToArray();
                text = string.Format(urlFormat.Format, args);
            }

            return _apiBaseAddress + text;
        }
        private static async Task EnsureSuccessStatusCode(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            string text = ((response.Content == null) ? string.Empty : (await response.Content.ReadAsStringAsync()));
            throw new Exception(text);
        }
    }
}


internal interface IRequestSender
{
    Task<HttpResponseMessage> SendRequest(HttpMethod verb, string url, HttpContent content = null);
}
internal class ConnectionHandler : IRequestSender
{
    private static readonly HttpClient commonHttpClient = new HttpClient();

    private readonly HttpClient _client;

    private readonly CommonLibraryClientOptions _options;

    private readonly TokenCredential _tokenCredential;

    private readonly TokenRequestContext _tokenRequestContext;



    public ConnectionHandler(CommonLibraryClientOptions options, HttpClient httpClient, TokenCredential tokenCredential)
    {
        _options = options;
        _client = httpClient ?? commonHttpClient;
        _tokenCredential = tokenCredential ?? throw new ArgumentException("TokenCredential is required and cannot be null");
        _tokenRequestContext = new TokenRequestContext(new string[1] { _options.CommonLibraryAppId + "/.default" });
    }

    public async Task<HttpResponseMessage> SendRequest(HttpMethod verb, string url, HttpContent content = null)
    {
        HttpRequestMessage request = new HttpRequestMessage(verb, url);
        if (content != null)
        {
            request.Content = content;
        }

        AccessToken accessToken = await _tokenCredential.GetTokenAsync(_tokenRequestContext, default(CancellationToken));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.Token);
        return await _client.SendAsync(request);
    }

}

internal static class CommonLibraryTokenCredential
{
    private static readonly char[] ConnectionStringConfigSeparator = new char[1] { ';' };

    private static readonly char[] ConnectionStringKvSeparator = new char[1] { '=' };


    public static TokenCredential GetTokenCredential(string connectionString, CommonLibraryClientOptions options)
    {
        Dictionary<string, string> dictionary = ParseProviderConnectionString(connectionString);
        if (!dictionary.TryGetValue("RunAs", out var value))
        {
            return new DefaultAzureCredential();
        }

        return value switch
        {
            "CurrentUser" => throw new CredentialUnavailableException("Unsupported authentication method 'CurrentUser', to learn more check: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/app-auth-migration"),
            "Developer" => GetDeveloper(dictionary),
            "App" => GetApp(dictionary),
            _ => throw new CredentialUnavailableException("Unsupported connection string, to learn more: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/app-auth-migration"),
        };
    }

    private static Dictionary<string, string> ParseProviderConnectionString(string tokenProviderConnectionString)
    {
        if (string.IsNullOrWhiteSpace(tokenProviderConnectionString))
        {
            return new Dictionary<string, string>();
        }

        return (from entry in tokenProviderConnectionString.Split(ConnectionStringConfigSeparator).AsEnumerable()
                select entry.Split(ConnectionStringKvSeparator, 2)).ToDictionary((string[] entry) => entry[0], (string[] entry) => entry[1]);
    }

    private static TokenCredential GetDeveloper(IReadOnlyDictionary<string, string> connectionString)
    {
        if (!connectionString.TryGetValue("DeveloperTool", out var value))
        {
            throw new ArgumentException("Missing developer tool specified: ");
        }

        if (!(value == "VisualStudio"))
        {
            if (value == "AzureCli")
            {
                return new AzureCliCredential();
            }

            throw new ArgumentException("Unsupported developer tool specified: " + value + ", learn more: https://learn.microsoft.com/en-us/dotnet/api/overview/azure/app-auth-migration");
        }

        return new VisualStudioCredential();
    }

    private static TokenCredential GetApp(IReadOnlyDictionary<string, string> connectionString)
    {
        if (connectionString.ContainsKey("KeyVaultCertificateSecretIdentifier"))
        {
            throw new CredentialUnavailableException("Unsupported service principal client: KeyVaultCertificateSecretIdentifier: see https://learn.microsoft.com/en-us/dotnet/api/overview/azure/app-auth-migration to learn more");
        }

        if (connectionString.TryGetValue("AppKey", out var value))
        {
            if (!connectionString.TryGetValue("TenantId", out var value2))
            {
                throw new ArgumentException("Service Principal Client: Missing TenantId for ConnectionString");
            }

            if (!connectionString.TryGetValue("AppId", out var value3))
            {
                throw new ArgumentException("Service Principal Client: Missing AppId for ConnectionString");
            }

            return new ClientSecretCredential(value2, value3, value);
        }

        if (connectionString.TryGetValue("CertificateThumbprint", out var value4))
        {
            if (!connectionString.TryGetValue("TenantId", out var value5))
            {
                throw new ArgumentException("Service Principal Client: Missing TenantId for ConnectionString");
            }

            if (!connectionString.TryGetValue("CertificateStoreLocation", out var value6))
            {
                throw new ArgumentException("Service Principal Client: Missing CertificateStoreLocation for ConnectionString");
            }

            return new ClientCertificateCredential(value5, value4, value6);
        }

        if (connectionString.TryGetValue("CertificateSubjectName", out var value7))
        {
            if (!connectionString.TryGetValue("TenantId", out var value8))
            {
                throw new ArgumentException("Service Principal Client: Missing TenantId for ConnectionString");
            }

            if (!connectionString.TryGetValue("CertificateStoreLocation", out var value9))
            {
                throw new ArgumentException("Service Principal Client: Missing CertificateStoreLocation for ConnectionString");
            }

            return new ClientCertificateCredential(value8, value7, value9);
        }

        if (connectionString.TryGetValue("AppId", out var value10))
        {
            return new ManagedIdentityCredential(value10);
        }

        return new ManagedIdentityCredential();
    }

}


public class CommonLibraryClientOptions
{
    public const string COMMONLIBRARY_APPID_PRODUCTION = "37d598fc-da0f-46bd-949f-7107918d47a5";

    public const string COMMONLIBRARY_ADDRESS_PRODUCTION = "https://commonlibapi.equinor.com/";

    public string TokenProviderConnectionString { get; set; }

    public string CommonLibraryAppId { get; set; } = "37d598fc-da0f-46bd-949f-7107918d47a5";


    public string CommonLibraryApiBaseAddress { get; set; } = "https://commonlibapi.equinor.com/";

}