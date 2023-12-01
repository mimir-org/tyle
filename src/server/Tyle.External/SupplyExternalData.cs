using AutoMapper;
using System.Text.Json;
using Tyle.Application.Common;
using Tyle.Application.Common.Requests;
using Tyle.External.Model;
using Tyle.External.Service;
using Tyle.Persistence;
using Tyle.Persistence.Common;

namespace Tyle.External
{
    public class SupplyExternalData
    {
        private IPurposeRepository _purposeRepository;
        private IClassifierRepository _classifierRepository;

        public SupplyExternalData(IPurposeRepository purposeRepository, IClassifierRepository classifierRepository)
        {
            _purposeRepository = purposeRepository;
            _classifierRepository = classifierRepository;
        }

        public async Task<bool> SupplyData()
        {
            //var externalData = new FetchExternalDataFromCL();
            //var purposeData = await externalData.FetchAllData();



            //var rawData = File.ReadAllText("");
            //var externalData = JsonSerializer.Deserialize<List<ExternalType>>(rawData);

            var purposeData = new List<RdlPurposeRequest>();

            foreach(var item in externalData)
                purposeData.Add(new RdlPurposeRequest { Iri=item.Identity, Name=item.Name, Description=item.Description});




            if (purposeData != null)
            {
                var itemsFromDb = await _purposeRepository.GetAll();

                foreach (var item in purposeData)
                {
                    //if exisit and name or descirption has changed.. update
                    var itemFromDb = itemsFromDb.Where(x => x.Iri.Equals(item.Iri)).FirstOrDefault();
                    if (itemFromDb != null)
                    {
                        if (itemFromDb.Name != item.Name || itemFromDb.Description != itemFromDb.Description)
                        {
                            await _purposeRepository.Update(itemFromDb.Id, item);
                        }
                    }
                    else
                    {
                        await _purposeRepository.Create(item);
                    }
                }
            }

            return true;
        }
    }

}

