using Azure.Identity;
using Equinor.TI.CommonLibrary.Client;
using Statoil.TI.CommonLibrary.Entities;
using System.Data.SqlTypes;
using System.Text.Json;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.External.Model;

namespace Tyle.External
{
    public class SupplyExternalData
    {
        private IPurposeRepository _purposeRepository;
        private IClassifierRepository _classifierRepository;
        private string _baseUrlForExternalApi;
        private string _typeOfResource;
        private string _commonLibClientOptions;

        public SupplyExternalData(IPurposeRepository purposeRepository, IClassifierRepository classifierRepository, string baseUrlForExternalApi = "https://commonlibapitest2.azurewebsites.net/", string typeOfResource = "CommonLib", string commonLibClientOptions = "47c797ba-b6e9-4383-92fc-1dd82a30fac0")
        {
            _baseUrlForExternalApi = baseUrlForExternalApi;
            _typeOfResource = typeOfResource;
            _commonLibClientOptions = commonLibClientOptions;
            _purposeRepository = purposeRepository;
            _classifierRepository = classifierRepository;
        }

        public async Task SupplyData()
        {
     
            var purposeExternalData = await GetDataFromCommonlib(ExternalDataType.Purpose, _baseUrlForExternalApi, _commonLibClientOptions);
            await SaveDataToDb(purposeExternalData, ExternalDataType.Purpose);

            

            //var classifierExternalData = await GetDataFromCommonlib(ExternalDataType.Classifier, _baseUrlForExternalApi, _commonLibClientOptions);
            //await SaveDataToDb(classifierExternalData, ExternalDataType.Classifier);

            //TODO Medium
            //TODO Predicate
            //TODO Unit
        }

        private async Task<List<RdlPurposeRequest>> GetDataFromCommonlib(ExternalDataType typeData, string baseUrl, string commonLibClientOptions)
        {
            var tokenCredential = new AzureCliCredential();
            var clientOptions = new CommonLibraryClientOptions
            {
                CommonLibraryAppId = commonLibClientOptions,
                CommonLibraryApiBaseAddress = baseUrl
            };

            var client = new CommonLibraryClient(clientOptions, tokenCredential);

            var library = String.Empty;

            switch (typeData)
            {
                case ExternalDataType.Purpose:
                    library = "IMFPurpose";
                    break;
                case ExternalDataType.Classifier:
                    //TODO
                    break;
                case ExternalDataType.Medium:
                    //TODO
                    break;
                case ExternalDataType.Predicate:
                    //TODO
                    break;
                case ExternalDataType.Unit:
                    //TODO
                    break;
                default:
                    throw new Exception("External datatype not found");
            }

            var codes = await client.CodeAsync(library: library, isValid:true);

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

        private async Task SaveDataToDb(List<RdlPurposeRequest> externalData, ExternalDataType typeData)
        {
            //TODO: Check if item is removed or inactive

            if (externalData != null)
            {
                if (typeData == ExternalDataType.Purpose)
                {
                    var itemsFromDb = await _purposeRepository.GetAll();

                    foreach (var item in externalData)
                    {
                        var itemFromDb = itemsFromDb.Where(x => x.Iri.Equals(item.Iri)).FirstOrDefault();

                        if (itemFromDb != null && (itemFromDb.Name != item.Name || itemFromDb.Description != itemFromDb.Description))
                        {
                            await _purposeRepository.Update(itemFromDb.Id, item);
                        }
                        
                        if(itemFromDb == null)
                        {
                            await _purposeRepository.Create(item);
                        }

                    }
                }
            }
        }
    }
}


