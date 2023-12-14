using Azure.Identity;
using Tyle.Application.Blocks;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.Core.Blocks;
using Tyle.External.Model;
using Tyle.Persistence.Blocks;

namespace Tyle.External
{
    public class SupplyExternalData
    {
        private IPurposeRepository _purposeRepository;
        private IClassifierRepository _classifierRepository;
        private ISymbolRepository _symbolRepository;
        private string _baseUrlForExternalApi;
        private string _typeOfResource;
        private string _commonLibClientOptions;

        public SupplyExternalData(IPurposeRepository purposeRepository, IClassifierRepository classifierRepository, ISymbolRepository symbolRepository, string baseUrlForExternalApi = "https://commonlibapitest2.azurewebsites.net/", string typeOfResource = "CommonLib", string commonLibClientOptions = "47c797ba-b6e9-4383-92fc-1dd82a30fac0")
        {
            _baseUrlForExternalApi = baseUrlForExternalApi;
            _typeOfResource = typeOfResource;
            _commonLibClientOptions = commonLibClientOptions;
            _purposeRepository = purposeRepository;
            _classifierRepository = classifierRepository;
            _symbolRepository = symbolRepository;
        }

        public async Task SupplyData()
        {
            var tokenCredential = new AzureCliCredential();
            var client2Options = new CommonLibraryClientOptions
            {
                CommonLibraryAppId = _commonLibClientOptions,
                CommonLibraryApiBaseAddress = _baseUrlForExternalApi
            };
            var client1Options = new CommonLibraryClientOptions
            {
                CommonLibraryAppId = "cf965a8b-9283-4849-a38b-b8d9c307c57d",
                CommonLibraryApiBaseAddress = "https://commonlibapitest.azurewebsites.net/"
            };

            var client2 = new CommonLibClient(client2Options, tokenCredential);
            var client1 = new CommonLibClient(client1Options, tokenCredential);

            var symbolExternalData = await GetSymbolsFromCommonlib(client1);
            await SaveSymbolsToDb(symbolExternalData);


            //var purposeExternalData = await GetDataFromCommonlib(ExternalDataType.Purpose, client2);
            //await SaveDataToDb(purposeExternalData, ExternalDataType.Purpose);



            //var classifierExternalData = await GetDataFromCommonlib(ExternalDataType.Classifier, _baseUrlForExternalApi, _commonLibClientOptions);
            //await SaveDataToDb(classifierExternalData, ExternalDataType.Classifier);

            //TODO Medium
            //TODO Predicate
            //TODO Unit
        }

        private async Task<List<EngineeringSymbol>> GetSymbolsFromCommonlib(CommonLibClient client)
        {

            var symbols = await client.GetSymbolsAsync();           

            return symbols;

        }

        private async Task<List<RdlObjectRequest>> GetDataFromCommonlib(ExternalDataType typeData, CommonLibClient client)
        {
            var library = String.Empty;
            var returnData = new List<RdlObjectRequest>();

            switch (typeData)
            {
                case ExternalDataType.Purpose:
                    library = "IMFPurpose";

                    var codes = await client.CodeAsync(library: library, isValid: true);

                    if (codes != null)
                    {
                        foreach (var code in codes)
                        {
                            returnData.Add(new RdlPurposeRequest { Description = code.Description, Iri = code.Identity, Name = code.Name });
                        }
                    }

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
                            //await _purposeRepository.Update(itemFromDb.Id, item);
                        }

                        if (itemFromDb == null)
                        {
                            await _purposeRepository.Create(item);
                        }

                    }
                }
            }
        }

        private async Task SaveSymbolsToDb(List<EngineeringSymbol> symbols)
        {
            var symbolsAlreadyInDb = await _symbolRepository.GetAll();
            
            var symbolsNotInDb = symbols.Except(symbolsAlreadyInDb).ToList();

            if(symbolsNotInDb.Count == 0)
                return;

            await _symbolRepository.Create(symbolsNotInDb);

            return;


        }
    }
}