using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using Tyle.Application.Common.Requests;
using Tyle.Core.Common;
using Tyle.External.Model;
using Tyle.External.Service;
using Tyle.Persistence;
using Tyle.Persistence.Common;

namespace Tyle.External
{
    public class SupplyExternalData
    {
        private TyleDbContext _dbContext;
        private IMapper _mapper;

        public SupplyExternalData(TyleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> SupplyData()
        {
            var externalData = new FetchExternalDataFromCL();
            var purposeData = await externalData.FetchAllData();
                    
            var savingClient = new PurposeRepository(_dbContext, _mapper);
            if (purposeData != null)
            {
                foreach (var item in purposeData)
                {
                    await savingClient.Create(item);
                }
            }

            return true;
        }
    }

}

