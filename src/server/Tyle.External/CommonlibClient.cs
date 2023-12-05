using Azure.Core;
using Azure.Identity;
using Equinor.TI.CommonLibrary.Client;
using Newtonsoft.Json;
using Statoil.TI.CommonLibrary.Entities;
using Statoil.TI.CommonLibrary.Entities.GenericView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tyle.External
{
    public class CommonLibClient
    {

        private static readonly JsonSerializerSettings SERIALIZER_SETTINGS = new JsonSerializerSettings
        {
            Converters = { (JsonConverter) new QueryConditionConverter() }
        };

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

        public async Task<List<Code>> CodeAsync(string library, string scope = null, string name = null, string description = null, bool? isValid = null, string filter = null)
        {
            return await GetAsync<List<Code>>($"/api/Code/{library}?scope={scope}&name={name}&description={description}&isValid={isValid}&$filter={filter}");
        }


        private async Task<TResponse> GetAsync<TResponse>(FormattableString address)
        {
            HttpResponseMessage response = await _requestSender.SendRequest(HttpMethod.Get, BuildSafeUrl(address));
            await EnsureSuccessStatusCode(response);


            return JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync(), SERIALIZER_SETTINGS);
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
            string responseBody = text;
            throw new CommonLibraryApiRequestException(response.StatusCode, responseBody);
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

    public ConnectionHandler(CommonLibraryClientOptions options, HttpClient httpClient)
    {
        if (options.TokenProviderConnectionString == null)
        {
            throw new ArgumentException("TokenProviderConnectionString is required when creating a CommonLibraryClient");
        }

        _options = options;
        _client = httpClient ?? commonHttpClient;
        _tokenCredential = CommonLibraryTokenCredential.GetTokenCredential(options.TokenProviderConnectionString, options);
        _tokenRequestContext = new TokenRequestContext(new string[1] { _options.CommonLibraryAppId + "/.default" });
    }

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

    private const int CONNECTION_STRING_KEY_VALUE_ARGUMENTS_MAX = 2;

    private const string AZURE_APPKEY = "AppKey";

    private const string AZURE_TENANTID = "TenantId";

    private const string AZURE_APPID = "AppId";

    private const string APP_KEYVAULT_CERTIFICATE_SECRET_IDENTIFIER = "KeyVaultCertificateSecretIdentifier";

    private const string CERTIFICATE_STORE_LOCATION = "CertificateStoreLocation";

    private const string CERTIFICATE_THUMBPRINT = "CertificateThumbprint";

    private const string CERTIFICATE_SUBJECTNAME = "CertificateSubjectName";

    private const string RUNAS = "RunAs";

    private const string RUNAS_TYPE_CURRENT_USER = "CurrentUser";

    private const string RUNAS_TYPE_DEVELOPER = "Developer";

    private const string RUNAS_TYPE_APP = "App";

    private const string DEVELOPER_TYPE_DEVELOPERTOOL = "DeveloperTool";

    private const string DEVELOPER_TOOL_VISUALSTUDIO = "VisualStudio";

    private const string DEVELOPER_TOOL_AZURECLI = "AzureCli";

    private const string MS_SUPPORT_URL = "https://learn.microsoft.com/en-us/dotnet/api/overview/azure/app-auth-migration";

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