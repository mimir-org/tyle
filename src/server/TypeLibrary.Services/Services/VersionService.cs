using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
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

        public VersionService(INodeRepository nodeRepository, ITransportRepository transportRepository, IInterfaceRepository interfaceRepository)
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
        /// Method will check if exiting object T vs new (updated) object TY.
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
            ValidateIsLatestVersion(latestVersionDm);

            var dmType = latestVersionDm?.GetType();
            var amType = newAm?.GetType();

            if (dmType is null || amType is null)
                throw new MimirorgBadRequestException("CalculateNewVersion<T,TY> T and/or TY can't be null.");

            if (!(dmType == typeof(NodeLibDm) && amType == typeof(NodeLibAm) || 
                dmType == typeof(TransportLibDm) && amType == typeof(TransportLibAm) ||
                dmType == typeof(InterfaceLibDm) && amType == typeof(InterfaceLibAm)))
                throw new MimirorgBadRequestException("CalculateNewVersion<T,TY> type T and/or TY not supported.");

            var nodeDm = dmType == typeof(NodeLibDm) ? latestVersionDm as NodeLibDm : null;
            var nodeAm = amType == typeof(NodeLibAm) ? newAm as NodeLibAm : null;

            var transportDm = dmType == typeof(TransportLibDm) ? latestVersionDm as TransportLibDm : null;
            var transportAm = amType == typeof(TransportLibAm) ? newAm as TransportLibAm : null;

            var interfaceDm = dmType == typeof(InterfaceLibDm) ? latestVersionDm as InterfaceLibDm : null;
            var interfaceAm = amType == typeof(InterfaceLibAm) ? newAm as InterfaceLibAm : null;

            var increaseMajorVersion = false;
            var increaseMinorVersion = false;

            //Validate: Name, RdsCode, RdsName, Aspect, ParentId and TerminalId
            ValidateNameRdsAspectParentTerminal(latestVersionDm, newAm);

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
                throw new MimirorgBadRequestException("You cannot remove existing attributes, only add.");

            if (attrAms.Count >= attrDms.Count)
            {
                if (attrDms.Where(x => attrAms.Any(y => y == x)).ToList().Count != attrDms.Count)
                    throw new MimirorgBadRequestException("You cannot change existing attributes, only add.");

                if (attrAms.Count > attrDms.Count)
                    increaseMajorVersion = true;
            }
            
            //Simple

            if (nodeDm != null && nodeAm != null)
            {
                var simpleDms = nodeDm.Simples?.Select(x => x.Id).ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>();
                var simpleAms = nodeAm.SimpleIdList?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList() ?? new List<string>();

                if (simpleAms.Count < simpleDms.Count)
                    throw new MimirorgBadRequestException("You cannot remove existing simple, only add.");

                if (simpleAms.Count >= simpleDms.Count)
                {
                    if (simpleDms.Where(x => simpleAms.Any(y => y == x)).ToList().Count != simpleDms.Count)
                        throw new MimirorgBadRequestException("You cannot change existing simple, only add.");

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
                    throw new MimirorgBadRequestException("You cannot remove existing node terminals, only add.");

                if (amList?.Count >= dmList?.Count)
                {
                    for (var i = 0; i < dmList.Count; i++)
                    {
                        if (amList[i].TerminalId != dmList[i].TerminalId)
                            throw new MimirorgBadRequestException("You cannot change existing node terminal's terminal id, only add.");

                        if (amList[i].Quantity != dmList[i].Quantity)
                            throw new MimirorgBadRequestException("You cannot change existing node terminal's number, only add.");

                        if (amList[i].ConnectorDirection != dmList[i].ConnectorDirection)
                            throw new MimirorgBadRequestException("You cannot change existing node terminal's direction, only add.");
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
                    throw new MimirorgBadRequestException("You cannot remove existing predefined selected attributes, only add.");

                if (amList?.Count >= dmList?.Count)
                {
                    for (var i = 0; i < dmList.Count; i++)
                    {
                        if (amList[i].Key != dmList[i].Key)
                            throw new MimirorgBadRequestException("You cannot change existing predefined selected attribute key, only add.");

                        if (amList[i].IsMultiSelect != dmList[i].IsMultiSelect)
                            throw new MimirorgBadRequestException("You cannot change existing multi select value for predefined selected attributes, only add.");

                        if (amList[i].Values.Count != dmList[i].Values.Count)
                            throw new MimirorgBadRequestException("You cannot add/remove existing values (dictionary) for predefined selected attributes.");

                        if (!amList[i].Values.ContentEquals(dmList[i].Values))
                            throw new MimirorgBadRequestException("You cannot add/remove/change existing key values (dictionary) for predefined selected attributes.");

                        var contRefsAm = amList[i]?.ContentReferences?.ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList();
                        var contRefsDm = dmList[i]?.ContentReferences?.ConvertToArray().ToList().OrderBy(x => x, StringComparer.InvariantCultureIgnoreCase).ToList();

                        if (contRefsAm == null && contRefsDm == null)
                            continue;

                        if (contRefsAm?.Count != contRefsDm?.Count)
                            throw new MimirorgBadRequestException("You cannot add/remove existing content references for predefined selected attributes.");

                        if (contRefsAm.Where((t, j) => t != contRefsDm[j]).Any())
                            throw new MimirorgBadRequestException("You cannot change existing content references for predefined selected attributes.");
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
                throw new MimirorgBadRequestException("CalculateNewVersion<T,TY> Existing object and new object is identical, no changes detected.");

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
            throw new MimirorgBadRequestException("CalculateNewVersion<T,TY> Sorry, critical unknown error detected!");
        }

        #region Private

        private async void ValidateIsLatestVersion<T>(T dm)
        {
            if (dm?.GetType() is null)
                throw new MimirorgBadRequestException("ValidateIsLatestVersion<T> T can't be null");

            var nodeDm = dm.GetType() == typeof(NodeLibDm) ? dm as NodeLibDm : null;
            var transportDm = dm.GetType() == typeof(TransportLibDm) ? dm as TransportLibDm : null;
            var interfaceDm = dm.GetType() == typeof(InterfaceLibDm) ? dm as InterfaceLibDm : null;

            if (nodeDm != null)
            {
                var latestVersion = await GetLatestVersion(nodeDm);
                if (double.Parse(nodeDm.Version, CultureInfo.InvariantCulture) < double.Parse(latestVersion.Version, CultureInfo.InvariantCulture))
                    throw new MimirorgBadRequestException($"Latest version is {latestVersion.Version} and not {nodeDm.Version}");
                return;
            }

            if (transportDm != null)
            {
                var latestVersion = await GetLatestVersion(transportDm);
                if (double.Parse(transportDm.Version, CultureInfo.InvariantCulture) < double.Parse(latestVersion.Version, CultureInfo.InvariantCulture))
                    throw new MimirorgBadRequestException($"Latest version is {latestVersion.Version} and not {transportDm.Version}");
                return;
            }

            if (interfaceDm != null)
            {
                var latestVersion = await GetLatestVersion(interfaceDm);
                if (double.Parse(interfaceDm.Version, CultureInfo.InvariantCulture) < double.Parse(latestVersion.Version, CultureInfo.InvariantCulture))
                    throw new MimirorgBadRequestException($"Latest version is {latestVersion.Version} and not {interfaceDm.Version}");
                return;
            }

            throw new MimirorgBadRequestException("ValidateIsLatestVersion<T> T type not supported.");
        }

        private static void ValidateNameRdsAspectParentTerminal<T,TY>(T dm, TY am) where T : class where TY : class
        {
            if (dm?.GetType() is null)
                throw new MimirorgBadRequestException("ValidateNameRdsAspectParentTerminal<T,TY> T and/or TY can't be null");

            var nDm = dm.GetType() == typeof(NodeLibDm) ? dm as NodeLibDm : null;
            var nAm = am.GetType() == typeof(NodeLibAm) ? am as NodeLibAm : null;

            var tDm = dm.GetType() == typeof(TransportLibDm) ? dm as TransportLibDm : null;
            var tAm = am.GetType() == typeof(TransportLibAm) ? am as TransportLibAm : null;

            var iDm = dm.GetType() == typeof(InterfaceLibDm) ? dm as InterfaceLibDm : null;
            var iAm = am.GetType() == typeof(InterfaceLibAm) ? am as InterfaceLibAm : null;

            //Name - Node/Transport/Interface
            if (nDm != null && nAm != null && !string.Equals(nDm.Name?.ToLower(), nAm.Name?.ToLower()) ||
                tDm != null && tAm != null && !string.Equals(tDm.Name?.ToLower(), tAm.Name?.ToLower()) ||
                iDm != null && iAm != null && !string.Equals(iDm.Name?.ToLower(), iAm.Name?.ToLower()))
                throw new MimirorgBadRequestException("Name can't be changed.");

            //RdsCode - Node/Transport/Interface
            if (nDm != null && nAm != null && !string.Equals(nDm.RdsCode?.ToLower(), nAm.RdsCode?.ToLower()) ||
                tDm != null && tAm != null && !string.Equals(tDm.RdsCode?.ToLower(), tAm.RdsCode?.ToLower()) ||
                iDm != null && iAm != null && !string.Equals(iDm.RdsCode?.ToLower(), iAm.RdsCode?.ToLower()))
                throw new MimirorgBadRequestException("RdsCode can't be changed.");

            //RdsName - Node/Transport/Interface
            if (nDm != null && nAm != null && !string.Equals(nDm.RdsName?.ToLower(), nAm.RdsName?.ToLower()) ||
                tDm != null && tAm != null && !string.Equals(tDm.RdsName?.ToLower(), tAm.RdsName?.ToLower()) ||
                iDm != null && iAm != null && !string.Equals(iDm.RdsName?.ToLower(), iAm.RdsName?.ToLower()))
                throw new MimirorgBadRequestException("RdsName can't be changed.");

            //Aspect - Node/Transport/Interface
            if (nDm != null && nAm != null && nDm.Aspect != nAm.Aspect ||
                tDm != null && tAm != null && tDm.Aspect != tAm.Aspect ||
                iDm != null && iAm != null && iDm.Aspect != iAm.Aspect)
                throw new MimirorgBadRequestException("Aspect can't be changed.");

            //ParentId - Node/Transport/Interface
            if (nDm != null && nAm != null && !string.Equals(nDm.ParentId?.ToLower(), nAm.ParentId?.ToLower()) ||
                tDm != null && tAm != null && !string.Equals(tDm.ParentId?.ToLower(), tAm.ParentId?.ToLower()) ||
                iDm != null && iAm != null && !string.Equals(iDm.ParentId?.ToLower(), iAm.ParentId?.ToLower()))
                throw new MimirorgBadRequestException("ParentId can't be changed.");

            //TerminalId - Transport/Interface
            if (tDm != null && tAm != null && !string.Equals(tDm.TerminalId?.ToLower(), tAm.TerminalId?.ToLower()) ||
               iDm != null && iAm != null && !string.Equals(iDm.TerminalId?.ToLower(), iAm.TerminalId?.ToLower()))
                throw new MimirorgBadRequestException("TerminalId can't be changed.");
        }

        #endregion Private
    }
}