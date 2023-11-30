using Azure.Identity;
using Equinor.TI.CommonLibrary.Client;
using Tyle.Application.Common.Requests;
using Tyle.External.Model;


namespace Tyle.External.Service
{
    public class FetchExternalDataFromCL : IFetchExternalData
    {
        public async Task<List<RdlPurposeRequest>> FetchAllData()
        {

            var tokenCredential = new AzureCliCredential();
            var clientOptions = new CommonLibraryClientOptions
            {
                CommonLibraryAppId = "47c797ba-b6e9-4383-92fc-1dd82a30fac0",
                CommonLibraryApiBaseAddress = "https://commonlibapitest2.azurewebsites.net/"
            };

            var client = new CommonLibraryClient(clientOptions, tokenCredential);

            var codes = await client.CodeAsync(library: "IMFPurpose", isValid: true);


            var returnData = new List<RdlPurposeRequest>();

            if (codes != null)
            {
                foreach (var code in codes)
                {
                    returnData.Add(new RdlPurposeRequest { Description = code.Description, Iri = code.Identity, Name = code.Name });
                }
            }
            return returnData;
        }
    }




}
