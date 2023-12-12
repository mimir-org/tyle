using Azure.Core;
using Azure.Identity;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<List<SymbolFromCommonlib>> GetSymbolsAsync()
        {
            try
            {
                var adress = "https://commonlibapitest.azurewebsites.net/api/symbol/ReadEngineeringSymbol?allVersions=false";
                var content = new StringContent("", Encoding.UTF8, "application/json");
                //HttpResponseMessage response = await _requestSender.SendRequest(HttpMethod.Post, adress, content);

                var symbols = "[{\"@id\":\"symbol:467fa586-1b75-44f1-9b81-a53546be104b\",\"@graph\":[{\"@id\":\"_:b0\",\"sym:positionY\":{\"@value\":\"12\",\"@type\":\"xsd:decimal\"},\"sym:positionX\":{\"@value\":\"24\",\"@type\":\"xsd:decimal\"},\"@type\":\"sym:CenterOfRotation\"},{\"@id\":\"symbol:467fa586-1b75-44f1-9b81-a53546be104b\",\"esmde:id\":\"467fa586-1b75-44f1-9b81-a53546be104b\",\"dc:description\":\"\",\"sym:height\":{\"@value\":\"24\",\"@type\":\"xsd:integer\"},\"dc:identifier\":\"HJXwQaiwWj\",\"esmde:oid\":\"01d31a01-d3a7-4315-b9a1-c2e96006dd06\",\"@type\":\"sym:Symbol\",\"rdfs:label\":\"Test-Label\",\"dc:issued\":{\"@value\":\"2023-11-29T11:43:12.7732574Z\",\"@type\":\"xsd:dateTime\"},\"sym:hasShape\":{\"@id\":\"_:b1\"},\"esmde:status\":\"Issued\",\"sym:hasCenterOfRotation\":{\"@id\":\"_:b0\"},\"pav:version\":\"1\",\"dc:modified\":{\"@value\":\"2023-11-29T11:42:40.565825Z\",\"@type\":\"xsd:dateTime\"},\"dc:created\":{\"@value\":\"2023-11-29T11:41:06.777426Z\",\"@type\":\"xsd:dateTime\"},\"sym:width\":{\"@value\":\"48\",\"@type\":\"xsd:integer\"}},{\"@id\":\"_:b1\",\"sym:hasSerialization\":{\"@value\":\"M24 12.48L48 24V0L24 11.52L0 0V24L24 12.48ZM1 1.58923L22.6891 12L1 22.4108V1.58923ZM47 22.4108L25.3109 12L47 1.58923V22.4108Z\",\"@type\":\"sym:svg-path-data\"},\"@type\":\"sym:Shape\"}],\"@context\":{\"symbol\":\"https://rdf.equinor.com/engineering-symbols/\",\"pav\":\"http://purl.org/pav/\",\"rdf\":\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\",\"esmde\":\"https://rdf.equinor.com/engineering-symbol-metadata-editor#\",\"sym\":\"http://example.equinor.com/symbol#\",\"xsd\":\"http://www.w3.org/2001/XMLSchema#\",\"rdfs\":\"http://www.w3.org/2000/01/rdf-schema#\",\"foaf\":\"http://xmlns.com/foaf/0.1/\",\"dc\":\"http://purl.org/dc/terms/\"}},{\"@id\":\"symbol:efd68e67-be03-4a7d-a5b6-2ea4570c8c72\",\"@graph\":[{\"@id\":\"_:b0\",\"sym:hasSerialization\":{\"@value\":\"M9 6V10H9.5V6H9ZM9 14V18H9.5V14H9ZM9 22V26H9.5V22H9ZM9 30L9 34H9.5L9.5 30H9ZM9 38V42H9.5V38H9ZM9 46V50H9.5V46H9ZM9 54L9 58H9.5L9.5 54H9ZM9 62V66H9.5V62H9ZM9 70V74H9.5V70H9ZM9 78V82H9.5V78H9ZM9 86V90H9.5V86H9ZM18 0V96H3.8147e-06V0H18ZM9.5 1V2H9V1H1V95H9V94H9.5V95H17V1H9.5Z\",\"@type\":\"sym:svg-path-data\"},\"@type\":\"sym:Shape\"},{\"@id\":\"symbol:efd68e67-be03-4a7d-a5b6-2ea4570c8c72\",\"dc:creator\":{\"@id\":\"_:b1\"},\"sym:height\":{\"@value\":\"96\",\"@type\":\"xsd:integer\"},\"esmde:oid\":\"b842d3e0-0fd4-4be6-bb3d-0467be950673\",\"dc:description\":\"\",\"@type\":\"sym:Symbol\",\"sym:hasCenterOfRotation\":{\"@id\":\"_:b2\"},\"pav:previousVersion\":{\"@id\":\"symbol:685f9f26-cacd-4605-8972-a4bd64489726\"},\"dc:modified\":{\"@value\":\"1970-01-01T00:00:00Z\",\"@type\":\"xsd:dateTime\"},\"esmde:id\":\"efd68e67-be03-4a7d-a5b6-2ea4570c8c72\",\"sym:width\":{\"@value\":\"18\",\"@type\":\"xsd:integer\"},\"sym:hasShape\":{\"@id\":\"_:b0\"},\"sym:hasConnectionPoint\":[{\"@id\":\"_:b3\"},{\"@id\":\"_:b4\"}],\"esmde:status\":\"Issued\",\"pav:version\":\"3\",\"dc:created\":{\"@value\":\"2023-11-29T09:13:01.867054Z\",\"@type\":\"xsd:dateTime\"},\"rdfs:label\":\"ND0015\",\"dc:identifier\":\"ND0015\",\"dc:issued\":{\"@value\":\"2023-11-29T12:27:47.6646293Z\",\"@type\":\"xsd:dateTime\"}},{\"@id\":\"_:b1\",\"foaf:mbox\":\"TMACK@equinor.com\",\"foaf:name\":\"Tore Mackay (Webstep AS)\"},{\"@id\":\"_:b2\",\"sym:positionY\":{\"@value\":\"48\",\"@type\":\"xsd:decimal\"},\"sym:positionX\":{\"@value\":\"9\",\"@type\":\"xsd:decimal\"},\"@type\":\"sym:CenterOfRotation\"},{\"@id\":\"_:b3\",\"sym:connectorDirection\":{\"@value\":\"0\",\"@type\":\"xsd:integer\"},\"dc:identifier\":\"fOngoNdbaO\",\"sym:positionY\":{\"@value\":\"-5\",\"@type\":\"xsd:decimal\"},\"sym:positionX\":{\"@value\":\"5\",\"@type\":\"xsd:decimal\"},\"@type\":\"sym:ConnectionPoint\"},{\"@id\":\"_:b4\",\"sym:connectorDirection\":{\"@value\":\"0\",\"@type\":\"xsd:integer\"},\"dc:identifier\":\"kUgwLlSrnY\",\"sym:positionY\":{\"@value\":\"-5\",\"@type\":\"xsd:decimal\"},\"sym:positionX\":{\"@value\":\"0\",\"@type\":\"xsd:decimal\"},\"@type\":\"sym:ConnectionPoint\"}],\"@context\":{\"symbol\":\"https://rdf.equinor.com/engineering-symbols/\",\"pav\":\"http://purl.org/pav/\",\"rdf\":\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\",\"esmde\":\"https://rdf.equinor.com/engineering-symbol-metadata-editor#\",\"sym\":\"http://example.equinor.com/symbol#\",\"xsd\":\"http://www.w3.org/2001/XMLSchema#\",\"rdfs\":\"http://www.w3.org/2000/01/rdf-schema#\",\"foaf\":\"http://xmlns.com/foaf/0.1/\",\"dc\":\"http://purl.org/dc/terms/\"}},{\"@graph\":[{\"@id\":\"http://example.com#39e78cf4-d722-4ceb-89a4-7412175164aa\",\"@type\":\"sym:Symbol\",\"sym:drawColor\":\"#FF0000\",\"sym:fillColor\":\"#00FF00\",\"sym:hasConnectionPoint\":[{\"sym:connectorDirection\":90,\"sym:positionX\":87,\"sym:positionY\":13,\"dc:identifier\":\"1\"},{\"sym:connectorDirection\":180,\"sym:positionX\":82,\"sym:positionY\":13,\"dc:identifier\":\"2\"}],\"sym:hasShape\":[{\"sym:hasSerialization\":{\"@type\":\"sym:svg-path-data\",\"@value\":\"M52 40.5C52 42.7091 50.2091 44.5 48 44.5C45.7909 44.5 44 42.7091 44 40.5C44 38.2909 45.7909 36.5 48 36.5C50.2091 36.5 52 38.2909 52 40.5ZM51 40.5C51 38.8431 49.6569 37.5 48 37.5C46.3431 37.5 45 38.8431 45 40.5C45 42.1569 46.3431 43.5 48 43.5C49.6569 43.5 51 42.1569 51 40.5ZM88 20.5H76.7266C80.6809 26.1692 83 33.0638 83 40.5C83 50.9622 78.4096 60.3522 71.1329 66.7659L83 90.5H13L24.8671 66.7659C17.5904 60.3522 13 50.9622 13 40.5C13 21.17 28.67 5.5 48 5.5H88V20.5ZM48 74.5C66.7777 74.5 82 59.2777 82 40.5C82 33.0245 79.5874 26.1124 75.4984 20.5C75.2521 20.1619 74.9997 19.8285 74.7413 19.5H86.8571V6.5H49.1429V6.51885C48.7634 6.50631 48.3825 6.5 48 6.5C29.2223 6.5 14 21.7223 14 40.5C14 59.2777 29.2223 74.5 48 74.5ZM25.6501 67.4359L14.618 89.5H81.382L70.3499 67.4359C64.2874 72.4719 56.4973 75.5 48 75.5C39.5027 75.5 31.7126 72.4719 25.6501 67.4359Z\"}}],\"sym:height\":96,\"sym:width\":96,\"dc:created\":{\"@type\":\"xsd:dateTime\",\"@value\":\"2023-09-19T00:00:00\"},\"dcterms:creator\":{\"foaf:mbox\":\"kari.normann@mail.com\",\"foaf:name\":\"Kari Normann\"},\"dc:description\":\"Pump symbol as defined in XXX\",\"dc:identifier\":\"PP007A\",\"dc:issued\":{\"@type\":\"xsd:dateTime\",\"@value\":\"2023-09-19T00:00:00\"},\"dc:modified\":{\"@type\":\"xsd:dateTime\",\"@value\":\"2023-09-19T00:00:00\"},\"pav:version\":\"0.1.0\",\"rdfs:label\":\"Pump PP007A\"}],\"@context\":[\"http://jsonld-context.dyreriket.xyz/rdfs.json\",\"http://jsonld-context.dyreriket.xyz/sh.json\",\"http://jsonld-context.dyreriket.xyz/pav.json\",\"http://jsonld-context.dyreriket.xyz/skos.json\",{\"@version\":1.1,\"dc\":\"http://purl.org/dc/terms/\",\"foaf\":\"http://xmlns.com/foaf/0.1/\",\"owl\":\"http://www.w3.org/2002/07/owl#\",\"pav\":\"http://purl.org/pav/\",\"rdf\":\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\",\"rdfs\":\"http://www.w3.org/2000/01/rdf-schema#\",\"sh\":\"http://www.w3.org/ns/shacl#\",\"skos\":\"http://www.w3.org/2004/02/skos/core#\",\"vann\":\"http://purl.org/vocab/vann/\",\"vs\":\"http://www.w3.org/2003/06/sw-vocab-status/ns#\",\"xsd\":\"http://www.w3.org/2001/XMLSchema#\",\"sym\":\"http://example.equinor.com/symbol#\",\"sym:connectorDirection\":{\"@type\":\"xsd:integer\"},\"sym:drawColor\":{\"@type\":\"xsd:string\"},\"sym:fillColor\":{\"@type\":\"xsd:string\"},\"sym:hasConnectionPoint\":{\"@type\":\"@id\",\"@container\":\"@set\"},\"sym:hasSerialization\":{},\"sym:hasShape\":{\"@type\":\"@id\",\"@container\":\"@set\"},\"sym:height\":{\"@type\":\"xsd:integer\"},\"sym:isConnectionPointOn\":{\"@type\":\"@id\"},\"sym:isPointOn\":{\"@type\":\"@id\"},\"sym:positionX\":{\"@type\":\"xsd:integer\"},\"sym:positionY\":{\"@type\":\"xsd:integer\"},\"sym:width\":{\"@type\":\"xsd:integer\"},\"pav:createdBy\":{\"@type\":\"@id\",\"@container\":\"@set\"},\"pav:contributedBy\":{\"@type\":\"@id\",\"@container\":\"@set\"}}]}]";
                string jsonLdString = "{\"@context\":\"http://schema.org\",\"@type\":\"Person\",\"name\":\"John Doe\",\"jobTitle\":\"Software Developer\"}";
                                
                                                                
                var symbols1 = JsonConvert.DeserializeObject<List<SymbolFromCommonlibRoot>>(symbols);



          



                return new List<SymbolFromCommonlib>();
               // return await response.Content.ReadFromJsonAsync<List<SymbolFromCommonlib>>();

            }
            catch (Exception ex)
            {

                throw;
            }
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