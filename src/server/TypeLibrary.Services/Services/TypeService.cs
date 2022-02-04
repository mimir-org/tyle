using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Client;
using TypeLibrary.Models.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TypeService : ITypeService
    {
        private readonly INodeTerminalRepository _nodeTypeTerminalTypeRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly ITypeRepository _libraryTypeComponentRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISimpleRepository _simpleTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public TypeService(INodeTerminalRepository nodeTypeTerminalTypeRepository, ILibraryRepository libraryRepository, 
            ITypeRepository libraryTypeComponentRepository, IAttributeRepository attributeRepository, ISimpleRepository simpleTypeRepository,
            IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _nodeTypeTerminalTypeRepository = nodeTypeTerminalTypeRepository;
            _libraryRepository = libraryRepository;
            _libraryTypeComponentRepository = libraryTypeComponentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _attributeRepository = attributeRepository;
            _simpleTypeRepository = simpleTypeRepository;
        }

        /// <summary>
        /// Get typeAm by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ignoreNotFound"></param>
        /// <returns></returns>
        public async Task<TypeLibDm> GetTypeById(string id, bool ignoreNotFound = false)
        {
            var libraryTypeComponent = await _libraryTypeComponentRepository.GetAsync(id);

            if (!ignoreNotFound && libraryTypeComponent == null)
                throw new MimirorgNotFoundException($"The typeAm with id: {id} could not be found.");

            if (libraryTypeComponent is NodeLibDm)
            {
                return await _libraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<NodeLibDm>()
                    .Include(x => x.TerminalTypes)
                    .Include(x => x.AttributeList)
                    .Include(x => x.SimpleTypes)
                    .ThenInclude(y => y.AttributeList)
                    .FirstOrDefaultAsync();
            }

            if (libraryTypeComponent is TransportLibDm)
            {
                return await _libraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<TransportLibDm>()
                    .Include(x => x.TerminalDm)
                    .Include(x => x.AttributeList)
                    .FirstOrDefaultAsync();
            }

            if (libraryTypeComponent is InterfaceLibDm)
            {
                return await _libraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<InterfaceLibDm>()
                    .Include(x => x.TerminalDm)
                    .FirstOrDefaultAsync();
            }

            return libraryTypeComponent;
        }

        /// <summary>
        /// Create library components
        /// </summary>
        /// <param name="typeAmList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TypeLibDm>> CreateTypes(ICollection<TypeLibAm> typeAmList)
        {
            return await CreateLibraryTypes(typeAmList, false);
        }

        /// <summary>
        /// Create a library component
        /// </summary>
        /// <param name="typeAm"></param>
        /// <returns></returns>
        public async Task<T> CreateType<T>(TypeLibAm typeAm) where T : class, new()
        {
            return await CreateLibraryType<T>(typeAm, false);
        }

        /// <summary>
        /// Update a library typeAm based on id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="typeAm"></param>
        /// <param name="updateMajorVersion"></param>
        /// <param name="updateMinorVersion"></param>
        /// <returns></returns>
        public async Task<T> UpdateType<T>(string id, TypeLibAm typeAm, bool updateMajorVersion, bool updateMinorVersion) where T : class, new()
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgNullReferenceException("Can't update a typeAm without an id");

            if (typeAm == null)
                throw new MimirorgNullReferenceException("Can't update a null typeAm");

            var existingType = await GetTypeById(id);

            if (existingType?.Id == null)
                throw new MimirorgNotFoundException($"There is no typeAm with id:{id} to update.");

            var existingTypeVersions = GetAllTypes()
                .Where(x => x.TypeId == existingType.TypeId)
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (double.Parse(existingType.Version, CultureInfo.InvariantCulture) <
                double.Parse(existingTypeVersions[^1].Version, CultureInfo.InvariantCulture))
                throw new MimirorgInvalidOperationException($"Not allowed to edit previous {existingType.Version} version. Latest version is {existingTypeVersions[^1].Version}");

            if (updateMajorVersion || updateMinorVersion)
            {
                typeAm.Version = updateMajorVersion ?
                    existingTypeVersions[^1].Version.IncrementMajorVersion() :
                    existingTypeVersions[^1].Version.IncrementMinorVersion();

                typeAm.TypeId = existingTypeVersions[0].TypeId;

                return await CreateLibraryType<T>(typeAm, true);
            }

            typeAm.Version = existingType.Version;
            typeAm.TypeId = existingType.TypeId;

            await DeleteType(id);

            return await CreateLibraryType<T>(typeAm, true);
        }

        /// <summary>
        /// Get all library types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TypeLibAm> GetAllTypes()
        {
            var nodeTypes = _libraryTypeComponentRepository
                .GetAll()
                .OfType<NodeLibDm>()
                .Include(x => x.TerminalTypes)
                .Include("TerminalTypes.TerminalDm")
                .Include(x => x.AttributeList)
                .Include(x => x.SimpleTypes)
                .ThenInclude(y => y.AttributeList)
                .ToList();

            var transportTypes = _libraryTypeComponentRepository
                .GetAll()
                .OfType<TransportLibDm>()
                .Include(x => x.AttributeList)
                .ToList();

            var interfaceType = _libraryTypeComponentRepository
                .GetAll()
                .OfType<InterfaceLibDm>()
                .ToList();

            foreach (var clt in nodeTypes.Select(x => _mapper.Map<TypeLibAm>(x)))
            {
                yield return clt;
            }

            foreach (var clt in transportTypes.Select(x => _mapper.Map<TypeLibAm>(x)))
            {
                yield return clt;
            }

            foreach (var clt in interfaceType.Select(x => _mapper.Map<TypeLibAm>(x)))
            {
                yield return clt;
            }
        }

        /// <summary>
        /// Convert a TypeDm to TypeAm
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enum"></param>
        /// <returns></returns>
        public async Task<TypeLibAm> ConvertToCreateType(string id, LibraryFilter @enum)
        {
            switch (@enum)
            {
                case LibraryFilter.Node:
                    var nodeItem = await _libraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<NodeLibDm>()
                        .Include(x => x.TerminalTypes)
                        .Include("TerminalTypes.TerminalDm")
                        .Include(x => x.AttributeList)
                        .Include(x => x.SimpleTypes)
                        .FirstOrDefaultAsync();

                    if (nodeItem == null)
                        throw new MimirorgNotFoundException($"There is no typeAm with id: {id} and @enum: {@enum}");

                    return _mapper.Map<TypeLibAm>(nodeItem);

                case LibraryFilter.Interface:
                    var interfaceItem = await _libraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<InterfaceLibDm>()
                        .FirstOrDefaultAsync();

                    if (interfaceItem == null)
                        throw new MimirorgNotFoundException($"There is no typeAm with id: {id} and @enum: {@enum}");

                    return _mapper.Map<TypeLibAm>(interfaceItem);

                case LibraryFilter.Transport:
                    var transportItem = await _libraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<TransportLibDm>()
                        .Include(x => x.AttributeList)
                        .FirstOrDefaultAsync();

                    if (transportItem == null)
                        throw new MimirorgNotFoundException($"There is no typeAm with id: {id} and @enum: {@enum}");

                    return _mapper.Map<TypeLibAm>(transportItem);

                default:
                    throw new MimirorgInvalidOperationException("Filter typeAm mismatch");
            }
        }

        /// <summary>
        /// Create a simple typeAm
        /// </summary>
        /// <param name="simpleAm"></param>
        /// <returns></returns>
        public async Task<SimpleLibDm> CreateSimpleType(SimpleLibAm simpleAm)
        {
            var newType = _mapper.Map<SimpleLibDm>(simpleAm);
            var existingType = await _simpleTypeRepository.GetAsync(newType.Id);

            if (existingType != null)
                throw new MimirorgDuplicateException($"TypeDm with name {simpleAm.Name} already exist.");

            foreach (var attribute in newType.AttributeList)
            {
                _attributeRepository.Attach(attribute, EntityState.Unchanged);
            }

            await _simpleTypeRepository.CreateAsync(newType);
            await _simpleTypeRepository.SaveAsync();

            return newType;
        }

        /// <summary>
        /// Create simple types
        /// </summary>
        /// <param name="simpleAmList"></param>
        /// <returns></returns>
        public async Task CreateSimpleTypes(ICollection<SimpleLibAm> simpleAmList)
        {
            if (simpleAmList == null || !simpleAmList.Any())
                return;

            foreach (var typeAm in simpleAmList)
            {
                var newType = _mapper.Map<SimpleLibDm>(typeAm);
                var existingType = await _simpleTypeRepository.GetAsync(newType.Id);

                if (existingType != null)
                {
                    _simpleTypeRepository.Detach(existingType);
                    continue;
                }

                foreach (var attribute in newType.AttributeList)
                {
                    _attributeRepository.Attach(attribute, EntityState.Unchanged);
                }

                await _simpleTypeRepository.CreateAsync(newType);
                await _simpleTypeRepository.SaveAsync();
                _simpleTypeRepository.Detach(newType);

                foreach (var attribute in newType.AttributeList)
                {
                    _attributeRepository.Detach(attribute);
                }
            }
        }

        /// <summary>
        /// Get all simple types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SimpleLibDm> GetSimpleTypes()
        {
            var types = _simpleTypeRepository.GetAll().Include(x => x.AttributeList).ToList();
            return types;
        }

        /// <summary>
        /// Clear the change tracker
        /// </summary>
        public void ClearAllChangeTracker()
        {
            _nodeTypeTerminalTypeRepository?.Context?.ChangeTracker.Clear();
            _libraryTypeComponentRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _simpleTypeRepository?.Context?.ChangeTracker.Clear();
        }

        /// <summary>
        /// Delete a typeAm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteType(string id)
        {
            var existingType = await GetTypeById(id);

            if (existingType == null)
                throw new MimirorgNotFoundException($"Could not delete typeAm with id: {id}. The typeAm was not found.");

            if (existingType is NodeLibDm typeToDelete)
            {
                foreach (var terminalType in typeToDelete.TerminalTypes)
                {
                    _nodeTypeTerminalTypeRepository.Attach(terminalType, EntityState.Deleted);
                }

                await _nodeTypeTerminalTypeRepository.SaveAsync();
            }

            await _libraryTypeComponentRepository.Delete(existingType.Id);
            await _libraryTypeComponentRepository.SaveAsync();
        }

        public async Task<TypeLibCm> GetType(string searchString)
        {
            var objectBlocks = await _libraryRepository.GetNodes(searchString);
            var transports = await _libraryRepository.GetTransports(searchString);
            var interfaces = await _libraryRepository.GetInterfaces(searchString);
            //TODO: Correct subprojects return when implemented
            var subProjects = new List<SubProjectLibCm>();

            var library = new TypeLibCm
            {
                ObjectBlocks = objectBlocks.ToList(),
                Transports = transports.ToList(),
                Interfaces = interfaces.ToList(),
                SubProjects = subProjects.ToList()
            };

            return library;
        }

        public async Task<IEnumerable<NodeLibCm>> GetNodes()
        {
            return await _libraryRepository.GetNodes();
        }

        public async Task<IEnumerable<TransportLibCm>> GetTransports()
        {
            return await _libraryRepository.GetTransports();
        }

        public async Task<IEnumerable<InterfaceLibCm>> GetInterfaces()
        {
            return await _libraryRepository.GetInterfaces();
        }

        public Task<IEnumerable<SubProjectLibCm>> GetSubProjects(string searchString = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        #region Private

        private async Task<IEnumerable<TypeLibDm>> CreateLibraryTypes(ICollection<TypeLibAm> createLibraryTypes, bool createNewFromExistingVersion)
        {
            var createdLibraryTypes = new List<TypeLibDm>();

            if (createLibraryTypes == null || !createLibraryTypes.Any())
                return createdLibraryTypes;

            var currentUser = _contextAccessor.GetName();
            var dateTimeNow = DateTime.Now.ToUniversalTime();

            foreach (var createLibraryType in createLibraryTypes)
            {
                if (!createNewFromExistingVersion)
                {
                    createLibraryType.Version = "1.0";
                    createLibraryType.Created = dateTimeNow;
                    createLibraryType.CreatedBy = currentUser;
                }
                else
                {
                    createLibraryType.Updated = dateTimeNow;
                    createLibraryType.UpdatedBy = currentUser;
                }

                if (createLibraryType.Aspect == Aspect.Location)
                    createLibraryType.ObjectType = ObjectType.ObjectBlock;

                TypeLibDm typeDm = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeLibDm>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceLibDm>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportLibDm>(createLibraryType),
                    _ => null
                };

                if (typeDm?.Id == null)
                    return null;

                typeDm.TypeId = !createNewFromExistingVersion ? typeDm.Id : createLibraryType.TypeId;

                switch (typeDm)
                {
                    case NodeLibDm nt:
                        {
                            if (nt.AttributeList != null && nt.AttributeList.Any())
                            {
                                foreach (var attribute in nt.AttributeList)
                                {
                                    _attributeRepository.Attach(attribute, EntityState.Unchanged);
                                }
                            }

                            if (nt.SimpleTypes != null && nt.SimpleTypes.Any())
                            {
                                foreach (var simpleType in nt.SimpleTypes)
                                {
                                    _simpleTypeRepository.Attach(simpleType, EntityState.Unchanged);
                                }
                            }
                            await _libraryTypeComponentRepository.CreateAsync(nt);
                            await _libraryTypeComponentRepository.SaveAsync();

                            if (nt.AttributeList != null && nt.AttributeList.Any())
                            {
                                foreach (var attribute in nt.AttributeList)
                                {
                                    _attributeRepository.Detach(attribute);
                                }
                            }

                            if (nt.SimpleTypes != null && nt.SimpleTypes.Any())
                            {
                                foreach (var simpleType in nt.SimpleTypes)
                                {
                                    _simpleTypeRepository.Detach(simpleType);
                                }
                            }

                            createdLibraryTypes.Add(nt);
                            continue;
                        }

                    case InterfaceLibDm it:

                        if (it.AttributeList != null && it.AttributeList.Any())
                        {
                            foreach (var attribute in it.AttributeList)
                            {
                                _attributeRepository.Attach(attribute, EntityState.Unchanged);
                            }
                        }

                        await _libraryTypeComponentRepository.CreateAsync(it);
                        await _libraryTypeComponentRepository.SaveAsync();

                        if (it.AttributeList != null && it.AttributeList.Any())
                        {
                            foreach (var attribute in it.AttributeList)
                            {
                                _attributeRepository.Detach(attribute);
                            }
                        }

                        createdLibraryTypes.Add(it);
                        continue;

                    case TransportLibDm tt:
                        {
                            if (tt.AttributeList != null && tt.AttributeList.Any())
                            {
                                foreach (var attribute in tt.AttributeList)
                                {
                                    _attributeRepository.Attach(attribute, EntityState.Unchanged);
                                }
                            }

                            await _libraryTypeComponentRepository.CreateAsync(tt);
                            await _libraryTypeComponentRepository.SaveAsync();

                            if (tt.AttributeList != null && tt.AttributeList.Any())
                            {
                                foreach (var attribute in tt.AttributeList)
                                {
                                    _attributeRepository.Detach(attribute);
                                }
                            }

                            createdLibraryTypes.Add(tt);
                            continue;
                        }

                    default:
                        continue;
                }
            }


            return createdLibraryTypes;
        }

        private async Task<T> CreateLibraryType<T>(TypeLibAm typeAm, bool createNewFromExistingVersion) where T : class, new()
        {
            if (typeAm == null)
                return null;

            var data = (await CreateLibraryTypes(new List<TypeLibAm> { typeAm }, createNewFromExistingVersion))?.FirstOrDefault();

            if (data == null)
                throw new MimirorgNullReferenceException("Could not create typeAm");

            var obj = await _libraryRepository.GetLibraryItem<T>(data.Id);
            return obj;
        }

        private void SetLibraryTypeVersion(TypeLibDm newTypeDm, TypeLibDm existingTypeDm)
        {
            if (string.IsNullOrWhiteSpace(newTypeDm?.Version) || string.IsNullOrWhiteSpace(existingTypeDm?.Version))
                throw new MimirorgInvalidOperationException("'Null' error when setting version for library typeAm");

            //TODO: The rules for when to trigger major/minor version incrementation is not finalized!

            //typeDm.Version = existingTypeDm.Version.IncrementMinorVersion();

        }

        #endregion Private
    }
}
