using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tyle.Application.Common.Requests;
using Tyle.External.Model;

namespace Tyle.External.Service
{
    internal interface IFetchExternalData
    {
        internal Task<List<RdlPurposeRequest>> FetchAllData();        
    }
}
