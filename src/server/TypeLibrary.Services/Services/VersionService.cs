using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class VersionService : IVersionService
    {
        private readonly IEfNodeRepository _nodeRepository;
        private readonly IEfTransportRepository _transportRepository;
        private readonly IEfInterfaceRepository _interfaceRepository;

        public VersionService(IEfNodeRepository nodeRepository, IEfTransportRepository transportRepository, IEfInterfaceRepository interfaceRepository)
        {
            _nodeRepository = nodeRepository;
            _transportRepository = transportRepository;
            _interfaceRepository = interfaceRepository;
        }

        /// <summary>
        /// Method will find and return the latest version.
        /// </summary>
        /// <typeparam name="T">NodeLibDm, TransportLibDm or InterfaceLibDm</typeparam>
        /// <param name="obj">NodeLibDm, TransportLibDm or InterfaceLibDm</param>
        /// <returns>Latest version of NodeLibDm, TransportLibDm or InterfaceLibDm</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        public async Task<T> GetLatestVersion<T>(T obj) where T : class
        {
            if (obj?.GetType() is null)
                throw new MimirorgBadRequestException("GetLatestVersion<T> Parameter T can't be null.");

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
                throw new MimirorgBadRequestException($"GetLatestVersion<T> Parameter T '{obj.GetType()}' not supported.");

            if (existingDmVersions == null || !existingDmVersions.Any())
                throw new MimirorgBadRequestException($"No {obj.GetType()} versions found.");

            return await Task.FromResult(existingDmVersions[^1]);
        }

        /// <summary>
        /// Method will check if exiting object T changes vs new (updated) object TY is allowed.
        /// Throws exception if any changes are not allowed, or if there are no changes between T and TY.
        /// Returns new major or minor version based on what has changes between T and TY.
        /// </summary>
        /// <typeparam name="T">NodeLibDm, TransportLibDm or InterfaceLibDm</typeparam>
        /// <typeparam name="TY">NodeLibAm, TransportLibAm or InterfaceLibAm</typeparam>
        /// <param name="latestVersionDm">NodeLibDm, TransportLibDm or InterfaceLibDm</param>
        /// <param name="newAm">NodeLibAm, TransportLibAm or InterfaceLibAm</param>
        /// <returns>New version (string) of a Node, Transport og Interface</returns>
        /// <exception cref="MimirorgBadRequestException"></exception>
        public async Task<string> CalculateNewVersion<T,TY>(T latestVersionDm, TY newAm) where T : class where TY : class
        {
            const string exceptionParam = "IncrementVersion<T,TY> Parameters T,TY can't be null.";
            const string exceptionParamClass = "IncrementVersion<T,TY> class type not supported.";
            const string exceptionName = "You cannot change existing name.";
            const string exceptionRdsCode = "You cannot change existing rds code.";
            const string exceptionRdsName = "You cannot change existing rds name.";
            const string exceptionAspect = "You cannot change existing aspect.";
            const string exceptionTerminalId = "You cannot change existing terminal id.";
            const string exceptionAttributeRemove = "You cannot remove existing attributes, only add.";
            const string exceptionAttributeChange = "You cannot change existing attributes, only add.";
            const string exceptionSimpleChange = "You cannot change existing simple, only add.";
            const string exceptionSimpleRemove = "You cannot remove existing simple, only add.";
            const string exceptionNodeTerminalRemove = "You cannot remove existing node terminals, only add.";
            const string exceptionNodeTerminalTerminalId = "You cannot change existing node terminal's terminal id, only add.";
            const string exceptionNodeTerminalNumber = "You cannot change existing node terminal's number, only add.";
            const string exceptionNodeTerminalDirection = "You cannot change existing node terminal's direction, only add.";
            const string exceptionSelectedPredefinedRemove = "You cannot remove existing predefined selected attributes, only add.";
            const string exceptionSelectedPredefinedChange = "You cannot change existing predefined selected attribute key, only add.";
            const string exceptionSelectedPredefinedMulti = "You cannot change existing multi select value for predefined selected attributes, only add.";
            const string exceptionSelectedPredefinedValues = "You cannot add/remove existing values (dictionary) for predefined selected attributes.";
            const string exceptionSelectedPredefinedKey = "You cannot add/remove/change existing key values (dictionary) for predefined selected attributes.";
            const string exceptionSelectedPredefinedContRefsRemove = "You cannot add/remove existing content references for predefined selected attributes.";
            const string exceptionSelectedPredefinedContRefsChange = "You cannot change existing content references for predefined selected attributes.";
            const string exceptionLatestVersionDmAndNewAmIsIdentical = "Existing object and new object is identical, no changes registered.";

            var dmType = latestVersionDm?.GetType();
            var amType = newAm?.GetType();

            if (dmType is null || amType is null)
                throw new MimirorgBadRequestException(exceptionParam);

            if (!(dmType == typeof(NodeLibDm) && amType == typeof(NodeLibAm) || 
                dmType == typeof(TransportLibDm) && amType == typeof(TransportLibAm) ||
                dmType == typeof(InterfaceLibDm) && amType == typeof(InterfaceLibAm)))
                throw new MimirorgBadRequestException(exceptionParamClass);

            var nodeDm = dmType == typeof(NodeLibDm) ? latestVersionDm as NodeLibDm : null;
            var nodeAm = amType == typeof(NodeLibAm) ? newAm as NodeLibAm : null;

            var transportDm = dmType == typeof(TransportLibDm) ? latestVersionDm as TransportLibDm : null;
            var transportAm = amType == typeof(TransportLibAm) ? newAm as TransportLibAm : null;

            var interfaceDm = dmType == typeof(InterfaceLibDm) ? latestVersionDm as InterfaceLibDm : null;
            var interfaceAm = amType == typeof(InterfaceLibAm) ? newAm as InterfaceLibAm : null;

            //Check if node parameter T is the latest version
            if (nodeDm != null)
            {
                var latestVersion = await GetLatestVersion(nodeDm);
                if (latestVersion != null)
                    if (double.Parse(nodeDm.Version, CultureInfo.InvariantCulture) < double.Parse(latestVersion.Version, CultureInfo.InvariantCulture))
                        throw new MimirorgBadRequestException($"Latest version is {latestVersion.Version} and not {nodeDm.Version}");
            }

            //Check if transport parameter T is the latest version
            if (transportDm != null)
            {
                var latestVersion = await GetLatestVersion(transportDm);
                if (latestVersion != null)
                    if (double.Parse(transportDm.Version, CultureInfo.InvariantCulture) < double.Parse(latestVersion.Version, CultureInfo.InvariantCulture))
                        throw new MimirorgBadRequestException($"Latest version is {latestVersion.Version} and not {transportDm.Version}");
            }

            //Check if interface parameter T is the latest version
            if (interfaceDm != null)
            {
                var latestVersion = await GetLatestVersion(interfaceDm);
                if (latestVersion != null)
                    if (double.Parse(interfaceDm.Version, CultureInfo.InvariantCulture) < double.Parse(latestVersion.Version, CultureInfo.InvariantCulture))
                        throw new MimirorgBadRequestException($"Latest version is {latestVersion.Version} and not {interfaceDm.Version}");
            }

            // Check all changes between T and TY. Throw exception if changes are not allowed.
            // Set increaseMinorVersion or increaseMajorVersion flags based on changes between T and TY. 

            var increaseMajorVersion = false;
            var increaseMinorVersion = false;

            //Name

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.Name?.ToLower(), nodeAm.Name?.ToLower()))
                throw new MimirorgBadRequestException(exceptionName);

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.Name?.ToLower(), transportAm.Name?.ToLower()))
                throw new MimirorgBadRequestException(exceptionName);

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.Name?.ToLower(), interfaceAm.Name?.ToLower()))
                throw new MimirorgBadRequestException(exceptionName);

            //RdsCode

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.RdsCode?.ToLower(), nodeAm.RdsCode?.ToLower()))
                throw new MimirorgBadRequestException(exceptionRdsCode);

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.RdsCode?.ToLower(), transportAm.RdsCode?.ToLower()))
                throw new MimirorgBadRequestException(exceptionRdsCode);

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.RdsCode?.ToLower(), interfaceAm.RdsCode?.ToLower()))
                throw new MimirorgBadRequestException(exceptionRdsCode);

            //RdsName

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.RdsName?.ToLower(), nodeAm.RdsName?.ToLower()))
                throw new MimirorgBadRequestException(exceptionRdsName);

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.RdsName?.ToLower(), transportAm.RdsName?.ToLower()))
                throw new MimirorgBadRequestException(exceptionRdsName);

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.RdsName?.ToLower(), interfaceAm.RdsName?.ToLower()))
                throw new MimirorgBadRequestException(exceptionRdsName);

            //Aspect

            if (nodeDm != null && nodeAm != null && nodeDm.Aspect != nodeAm.Aspect)
                throw new MimirorgBadRequestException(exceptionAspect);

            if (transportDm != null && transportAm != null && transportDm.Aspect != transportAm.Aspect)
                throw new MimirorgBadRequestException(exceptionAspect);

            if (interfaceDm != null && interfaceAm != null && interfaceDm.Aspect != interfaceAm.Aspect)
                throw new MimirorgBadRequestException(exceptionAspect);

            //ParentId

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.ParentId?.ToLower(), nodeAm.ParentId?.ToLower()))
                increaseMinorVersion = true;

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.ParentId?.ToLower(), transportAm.ParentId?.ToLower()))
                increaseMinorVersion = true;

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.ParentId?.ToLower(), interfaceAm.ParentId?.ToLower()))
                increaseMinorVersion = true;

            //PurposeName

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.PurposeName?.ToLower(), nodeAm.PurposeName?.ToLower()))
                increaseMinorVersion = true;

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.PurposeName?.ToLower(), transportAm.PurposeName?.ToLower()))
                increaseMinorVersion = true;

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.PurposeName?.ToLower(), interfaceAm.PurposeName?.ToLower()))
                increaseMinorVersion = true;

            //CompanyId

            if (nodeDm != null && nodeAm != null && nodeDm.CompanyId != nodeAm.CompanyId)
                increaseMinorVersion = true;

            if (transportDm != null && transportAm != null && transportDm.CompanyId != transportAm.CompanyId)
                increaseMinorVersion = true;

            if (interfaceDm != null && interfaceAm != null && interfaceDm.CompanyId != interfaceAm.CompanyId)
                increaseMinorVersion = true;

            //Description

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.Description?.ToLower(), nodeAm.Description?.ToLower()))
                increaseMinorVersion = true;

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.Description?.ToLower(), transportAm.Description?.ToLower()))
                increaseMinorVersion = true;

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.Description?.ToLower(), interfaceAm.Description?.ToLower()))
                increaseMinorVersion = true;

            //ContentReferences

            var refsAm = new List<string>();
            var refsDm = new List<string>();

            if (nodeDm != null && nodeAm != null)
            {
                refsDm.AddRange(nodeDm.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>());
                refsAm.AddRange(nodeAm.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>());
            }

            if (transportDm != null && transportAm != null)
            {
                refsDm.AddRange(transportDm.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>());
                refsAm.AddRange(transportAm.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>());
            }

            if (interfaceDm != null && interfaceAm != null)
            {
                refsDm.AddRange(interfaceDm.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>());
                refsAm.AddRange(interfaceAm.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>());
            }

            if (refsAm.Count != refsDm.Count || refsDm.Where(x => refsAm.Any(y => y == x)).ToList().Count != refsDm.Count)
                increaseMinorVersion = true;

            //Attributes

            var attrAms = new List<string>();
            var attrDms = new List<string>();

            if (nodeDm != null && nodeAm != null)
            {
                attrDms.AddRange(nodeDm.Attributes?.Select(x => x.Id).ToList() ?? new List<string>());
                attrAms.AddRange(nodeAm.AttributeIdList?.ToList() ?? new List<string>());
            }

            if (transportDm != null && transportAm != null)
            {
                attrDms.AddRange(transportDm.Attributes?.Select(x => x.Id).ToList() ?? new List<string>());
                attrAms.AddRange(transportAm.AttributeIdList?.ToList() ?? new List<string>());
            }

            if (interfaceDm != null && interfaceAm != null)
            {
                attrDms.AddRange(interfaceDm.Attributes?.Select(x => x.Id).ToList() ?? new List<string>());
                attrAms.AddRange(interfaceAm.AttributeIdList?.ToList() ?? new List<string>());
            }

            if (attrAms.Count < attrDms.Count)
                throw new MimirorgBadRequestException(exceptionAttributeRemove);

            if (attrAms.Count >= attrDms.Count)
            {
                if (attrDms.Where(x => attrAms.Any(y => y == x)).ToList().Count != attrDms.Count)
                    throw new MimirorgBadRequestException(exceptionAttributeChange);

                if (attrAms.Count > attrDms.Count)
                    increaseMajorVersion = true;
            }

            //TerminalId

            if (transportDm != null && transportAm != null && !string.Equals(transportDm.TerminalId?.ToLower(), transportAm.TerminalId?.ToLower()))
                throw new MimirorgBadRequestException(exceptionTerminalId);

            if (interfaceDm != null && interfaceAm != null && !string.Equals(interfaceDm.TerminalId?.ToLower(), interfaceAm.TerminalId?.ToLower()))
                throw new MimirorgBadRequestException(exceptionTerminalId);

            //Simple

            if (nodeDm != null && nodeAm != null)
            {
                var simpleDms = nodeDm.Simples?.Select(x => x.Id).ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>();
                var simpleAms = nodeAm.SimpleIdList?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>();

                if (simpleAms.Count < simpleDms.Count)
                    throw new MimirorgBadRequestException(exceptionSimpleRemove);

                if (simpleAms.Count >= simpleDms.Count)
                {
                    if (simpleDms.Where(x => simpleAms.Any(y => y == x)).ToList().Count != simpleDms.Count)
                        throw new MimirorgBadRequestException(exceptionSimpleChange);

                    if (simpleAms?.Count > simpleDms?.Count)
                        increaseMajorVersion = true;
                }
            }

            //NodeTerminals

            if (nodeDm != null && nodeAm != null)
            {
                var amList = nodeAm.NodeTerminals?.ToList().OrderBy(x => x?.TerminalId, StringComparer.InvariantCultureIgnoreCase).ToList();
                var dmList = nodeDm.NodeTerminals?.ToList().OrderBy(x => x?.TerminalId, StringComparer.InvariantCultureIgnoreCase).ToList();

                if (amList?.Count < dmList?.Count)
                    throw new MimirorgBadRequestException(exceptionNodeTerminalRemove);

                if (amList?.Count >= dmList?.Count)
                {
                    for (var i = 0; i < dmList.Count; i++)
                    {
                        if (amList[i].TerminalId != dmList[i].TerminalId)
                            throw new MimirorgBadRequestException(exceptionNodeTerminalTerminalId);

                        if (amList[i].Number != dmList[i].Number)
                            throw new MimirorgBadRequestException(exceptionNodeTerminalNumber);

                        if (amList[i].ConnectorDirection != dmList[i].ConnectorDirection)
                            throw new MimirorgBadRequestException(exceptionNodeTerminalDirection);
                    }
                }

                if (amList?.Count > dmList?.Count)
                    increaseMajorVersion = true;
            }

            //SelectedAttributePredefined

            if (nodeDm != null && nodeAm != null)
            {
                // ReSharper disable once ConstantConditionalAccessQualifier
                var amList = nodeAm.SelectedAttributePredefined?.ToList().OrderBy(x => x?.Key, StringComparer.InvariantCultureIgnoreCase).ToList();
                var dmList = nodeDm.SelectedAttributePredefined?.ToList().OrderBy(x => x?.Key, StringComparer.InvariantCultureIgnoreCase).ToList();

                if (amList?.Count < dmList?.Count)
                    throw new MimirorgBadRequestException(exceptionSelectedPredefinedRemove);

                if (amList?.Count >= dmList?.Count)
                {
                    for (var i = 0; i < dmList.Count; i++)
                    {
                        if (amList[i].Key != dmList[i].Key)
                            throw new MimirorgBadRequestException(exceptionSelectedPredefinedChange);

                        if (amList[i].IsMultiSelect != dmList[i].IsMultiSelect)
                            throw new MimirorgBadRequestException(exceptionSelectedPredefinedMulti);

                        if (amList[i].Values.Count != dmList[i].Values.Count)
                            throw new MimirorgBadRequestException(exceptionSelectedPredefinedValues);

                        if (!amList[i].Values.ContentEquals(dmList[i].Values))
                            throw new MimirorgBadRequestException(exceptionSelectedPredefinedKey);

                        var contRefsAm = amList[i]?.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList();
                        var contRefsDm = dmList[i]?.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList();

                        if (contRefsAm == null && contRefsDm == null)
                            continue;

                        if (contRefsAm?.Count != contRefsDm?.Count)
                            throw new MimirorgBadRequestException(exceptionSelectedPredefinedContRefsRemove);

                        if (contRefsAm.Where((t, j) => t != contRefsDm[j]).Any())
                            throw new MimirorgBadRequestException(exceptionSelectedPredefinedContRefsChange);
                    }
                }

                if (amList?.Count > dmList?.Count)
                    increaseMajorVersion = true;
            }

            //AttributeAspectIri

            if (nodeDm != null && nodeAm != null && !string.Equals(nodeDm.AttributeAspectIri, nodeAm.AttributeAspectIri, StringComparison.InvariantCultureIgnoreCase))
                increaseMinorVersion = true;

            //Symbol

            if (nodeDm != null && nodeAm != null && nodeDm.Symbol != nodeAm.Symbol)
                increaseMinorVersion = true;

            //Any version changes?
            if (!increaseMajorVersion && !increaseMinorVersion)
                throw new MimirorgBadRequestException(exceptionLatestVersionDmAndNewAmIsIdentical);

            //Increment node version
            if (nodeDm != null)
                return await Task.FromResult(increaseMajorVersion ? nodeDm.Version.IncrementMajorVersion() : nodeDm.Version.IncrementMinorVersion());

            //Increment transport version
            if (transportDm != null)
                return await Task.FromResult(increaseMajorVersion ? transportDm.Version.IncrementMajorVersion() : transportDm.Version.IncrementMinorVersion());

            //Increment interface version
            if (interfaceDm != null)
                return await Task.FromResult(increaseMajorVersion ? interfaceDm.Version.IncrementMajorVersion() : interfaceDm.Version.IncrementMinorVersion());

            //Code should not reach this line
            throw new MimirorgBadRequestException("IncrementVersion<T,TY> Critical error.");
        }
    }
}