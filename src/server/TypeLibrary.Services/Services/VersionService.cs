using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mimirorg.Common.Exceptions;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class VersionService : IVersionService
    {
        private readonly IMapper _mapper;
        private readonly IEfNodeRepository _nodeRepository;
        private readonly IEfTransportRepository _transportRepository;
        private readonly IEfInterfaceRepository _interfaceRepository;

        public VersionService(IMapper mapper, IEfNodeRepository nodeRepository, IEfTransportRepository transportRepository, IEfInterfaceRepository interfaceRepository)
        {
            _mapper = mapper;
            _nodeRepository = nodeRepository;
            _transportRepository = transportRepository;
            _interfaceRepository = interfaceRepository;
        }


        public async Task<T> GetLatestVersion<T>(T obj) where T : class
        {
            if (obj?.GetType() is null)
                throw new MimirorgBadRequestException("GetLatestVersion<T>(T obj) Parameter T can't be null.");

            var existingDmVersions = new List<T>();

            if (obj.GetType() == typeof(NodeLibDm) && (obj as NodeLibDm)?.Version != null)
            {
                (existingDmVersions as List<NodeLibDm>)?.AddRange(_nodeRepository.GetAllNodes()
                    .Where(x => x.FirstVersionId == (obj as NodeLibDm).FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            else if (obj.GetType() == typeof(InterfaceLibDm) && (obj as InterfaceLibDm)?.Version != null)
            {
                (existingDmVersions as List<InterfaceLibDm>)?.AddRange(_interfaceRepository.GetAllInterfaces()
                    .Where(x => x.FirstVersionId == (obj as InterfaceLibDm).FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            
            else if (obj.GetType() == typeof(TransportLibDm) && (obj as TransportLibDm)?.Version != null)
            {
                (existingDmVersions as List<TransportLibDm>)?.AddRange(_transportRepository.GetAllTransports()
                    .Where(x => x.FirstVersionId == (obj as TransportLibDm).FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            else
                throw new MimirorgBadRequestException($"GetLatestVersion<T>(T obj) Parameter T '{obj.GetType()}' not supported.");

            if (existingDmVersions == null || !existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No {obj.GetType()} versions found.");

            return await Task.FromResult(existingDmVersions[^1]);
        }
    }
}