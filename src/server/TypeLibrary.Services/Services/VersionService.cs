using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class VersionService : IVersionService
    {
        private readonly INodeRepository _nodeRepository;
        private readonly ITransportRepository _transportRepository;
        private readonly IInterfaceRepository _interfaceRepository;
        private readonly ITerminalRepository _terminalRepository;

        public VersionService(INodeRepository nodeRepository, ITransportRepository transportRepository, IInterfaceRepository interfaceRepository, ITerminalRepository terminalRepository)
        {
            _nodeRepository = nodeRepository;
            _transportRepository = transportRepository;
            _interfaceRepository = interfaceRepository;
            _terminalRepository = terminalRepository;
        }

        /// <summary>
        /// Method will find and return the latest version.
        /// </summary>
        /// <typeparam name="T">NodeLibDm, TransportLibDm, InterfaceLibDm or TerminalLibDm</typeparam>
        /// <param name="obj">NodeLibDm, TransportLibDm, InterfaceLibDm or TerminalLibDm</param>
        /// <returns>Latest version of NodeLibDm, TransportLibDm or InterfaceLibDm</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        public async Task<T> GetLatestVersion<T>(T obj) where T : class
        {
            if (obj?.GetType() is null)
                throw new MimirorgBadRequestException("GetLatestVersion<T> Parameter T can't be null.");

            var existingDmVersions = new List<T>();

            if (obj.GetType() == typeof(NodeLibDm) && (obj as NodeLibDm)?.Version != null)
            {
                (existingDmVersions as List<NodeLibDm>)?.AddRange(_nodeRepository.Get()
                    .Where(x => x.FirstVersionId == (obj as NodeLibDm)?.FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            else if (obj.GetType() == typeof(InterfaceLibDm) && (obj as InterfaceLibDm)?.Version != null)
            {
                (existingDmVersions as List<InterfaceLibDm>)?.AddRange(_interfaceRepository.Get()
                    .Where(x => x.FirstVersionId == (obj as InterfaceLibDm)?.FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            else if (obj.GetType() == typeof(TransportLibDm) && (obj as TransportLibDm)?.Version != null)
            {
                (existingDmVersions as List<TransportLibDm>)?.AddRange(_transportRepository.Get()
                    .Where(x => x.FirstVersionId == (obj as TransportLibDm)?.FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            else if (obj.GetType() == typeof(TerminalLibDm) && (obj as TerminalLibDm)?.Version != null)
            {
                (existingDmVersions as List<TerminalLibDm>)?.AddRange(_terminalRepository.Get()
                    .Where(x => x.FirstVersionId == (obj as TerminalLibDm)?.FirstVersionId && !x.Deleted).ToList()
                    .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList());
            }

            else
                throw new MimirorgBadRequestException($"GetLatestVersion<T> Parameter T '{obj.GetType()}' not supported.");

            if (existingDmVersions == null || !existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No {obj.GetType()} versions found.");

            return await Task.FromResult(existingDmVersions[^1]);
        }
    }
}