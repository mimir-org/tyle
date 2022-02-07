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
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Client;
using Mimirorg.TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class LibraryTypeService : Contracts.ILibraryTypeService
    {
        private readonly ITerminalNodeRepository _nodeTypeTerminalTypeRepository;
        private readonly ILibraryTypeItemRepository _libraryTypeItemRepository;
        private readonly ILibraryTypeRepository _libraryLibraryTypeComponentRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISimpleRepository _simpleTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public LibraryTypeService(ITerminalNodeRepository nodeTypeTerminalTypeRepository, ILibraryTypeItemRepository libraryTypeItemRepository, 
            ILibraryTypeRepository libraryLibraryTypeComponentRepository, IAttributeRepository attributeRepository, ISimpleRepository simpleTypeRepository,
            IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _nodeTypeTerminalTypeRepository = nodeTypeTerminalTypeRepository;
            _libraryTypeItemRepository = libraryTypeItemRepository;
            _libraryLibraryTypeComponentRepository = libraryLibraryTypeComponentRepository;
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
        public async Task<LibraryTypeLibDm> GetLibraryTypeById(string id, bool ignoreNotFound = false)
        {
            var libraryTypeComponent = await _libraryLibraryTypeComponentRepository.GetAsync(id);

            if (!ignoreNotFound && libraryTypeComponent == null)
                throw new MimirorgNotFoundException($"The typeAm with id: {id} could not be found.");

            if (libraryTypeComponent is NodeLibDm)
            {
                return await _libraryLibraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<NodeLibDm>()
                    .Include(x => x.Terminals)
                    .Include(x => x.Attributes)
                    .Include(x => x.SimpleTypes)
                    .ThenInclude(y => y.Attributes)
                    .FirstOrDefaultAsync();
            }

            if (libraryTypeComponent is TransportLibDm)
            {
                return await _libraryLibraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<TransportLibDm>()
                    .Include(x => x.Terminal)
                    .Include(x => x.Attributes)
                    .FirstOrDefaultAsync();
            }

            if (libraryTypeComponent is InterfaceLibDm)
            {
                return await _libraryLibraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<InterfaceLibDm>()
                    .Include(x => x.Terminal)
                    .FirstOrDefaultAsync();
            }

            return libraryTypeComponent;
        }

        /// <summary>
        /// Create library components
        /// </summary>
        /// <param name="typeAmList"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LibraryTypeLibDm>> CreateLibraryTypes(ICollection<LibraryTypeLibAm> typeAmList)
        {
            return await CreateLibraryTypes(typeAmList, false);
        }

        /// <summary>
        /// Create a library component
        /// </summary>
        /// <param name="libraryTypeAm"></param>
        /// <returns></returns>
        public async Task<T> CreateLibraryType<T>(LibraryTypeLibAm libraryTypeAm) where T : class, new()
        {
            return await CreateLibraryType<T>(libraryTypeAm, false);
        }

        /// <summary>
        /// Update a library typeAm based on id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libraryTypeAm"></param>
        /// <param name="updateMajorVersion"></param>
        /// <param name="updateMinorVersion"></param>
        /// <returns></returns>
        public async Task<T> UpdateLibraryType<T>(string id, LibraryTypeLibAm libraryTypeAm, bool updateMajorVersion, bool updateMinorVersion) where T : class, new()
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgNullReferenceException("Can't update a typeAm without an id");

            if (libraryTypeAm == null)
                throw new MimirorgNullReferenceException("Can't update a null typeAm");

            var existingType = await GetLibraryTypeById(id);

            if (existingType?.Id == null)
                throw new MimirorgNotFoundException($"There is no typeAm with id:{id} to update.");

            var existingTypeVersions = GetAllLibraryTypes()
                .Where(x => x.TypeId == existingType.TypeId)
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (double.Parse(existingType.Version, CultureInfo.InvariantCulture) <
                double.Parse(existingTypeVersions[^1].Version, CultureInfo.InvariantCulture))
                throw new MimirorgInvalidOperationException($"Not allowed to edit previous {existingType.Version} version. Latest version is {existingTypeVersions[^1].Version}");

            if (updateMajorVersion || updateMinorVersion)
            {
                libraryTypeAm.Version = updateMajorVersion ?
                    existingTypeVersions[^1].Version.IncrementMajorVersion() :
                    existingTypeVersions[^1].Version.IncrementMinorVersion();

                libraryTypeAm.TypeId = existingTypeVersions[0].TypeId;

                return await CreateLibraryType<T>(libraryTypeAm, true);
            }

            libraryTypeAm.Version = existingType.Version;
            libraryTypeAm.TypeId = existingType.TypeId;

            await DeleteLibraryType(id);

            return await CreateLibraryType<T>(libraryTypeAm, true);
        }

        /// <summary>
        /// Get all library types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LibraryTypeLibAm> GetAllLibraryTypes()
        {
            var nodeTypes = _libraryLibraryTypeComponentRepository
                .GetAll()
                .OfType<NodeLibDm>()
                .Include(x => x.Terminals)
                .Include("Terminals.Terminal")
                .Include(x => x.Attributes)
                .Include(x => x.SimpleTypes)
                .ThenInclude(y => y.Attributes)
                .ToList();

            var transportTypes = _libraryLibraryTypeComponentRepository
                .GetAll()
                .OfType<TransportLibDm>()
                .Include(x => x.Attributes)
                .ToList();

            var interfaceType = _libraryLibraryTypeComponentRepository
                .GetAll()
                .OfType<InterfaceLibDm>()
                .ToList();

            foreach (var clt in nodeTypes.Select(x => _mapper.Map<LibraryTypeLibAm>(x)))
            {
                yield return clt;
            }

            foreach (var clt in transportTypes.Select(x => _mapper.Map<LibraryTypeLibAm>(x)))
            {
                yield return clt;
            }

            foreach (var clt in interfaceType.Select(x => _mapper.Map<LibraryTypeLibAm>(x)))
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
        public async Task<LibraryTypeLibAm> ConvertToCreateLibraryType(string id, LibraryFilter @enum)
        {
            switch (@enum)
            {
                case LibraryFilter.Node:
                    var nodeItem = await _libraryLibraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<NodeLibDm>()
                        .Include(x => x.Terminals)
                        .Include("Terminals.Terminal")
                        .Include(x => x.Attributes)
                        .Include(x => x.SimpleTypes)
                        .FirstOrDefaultAsync();

                    if (nodeItem == null)
                        throw new MimirorgNotFoundException($"There is no typeAm with id: {id} and @enum: {@enum}");

                    return _mapper.Map<LibraryTypeLibAm>(nodeItem);

                case LibraryFilter.Interface:
                    var interfaceItem = await _libraryLibraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<InterfaceLibDm>()
                        .FirstOrDefaultAsync();

                    if (interfaceItem == null)
                        throw new MimirorgNotFoundException($"There is no typeAm with id: {id} and @enum: {@enum}");

                    return _mapper.Map<LibraryTypeLibAm>(interfaceItem);

                case LibraryFilter.Transport:
                    var transportItem = await _libraryLibraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<TransportLibDm>()
                        .Include(x => x.Attributes)
                        .FirstOrDefaultAsync();

                    if (transportItem == null)
                        throw new MimirorgNotFoundException($"There is no typeAm with id: {id} and @enum: {@enum}");

                    return _mapper.Map<LibraryTypeLibAm>(transportItem);

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

            foreach (var attribute in newType.Attributes)
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

                foreach (var attribute in newType.Attributes)
                {
                    _attributeRepository.Attach(attribute, EntityState.Unchanged);
                }

                await _simpleTypeRepository.CreateAsync(newType);
                await _simpleTypeRepository.SaveAsync();
                _simpleTypeRepository.Detach(newType);

                foreach (var attribute in newType.Attributes)
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
            var types = _simpleTypeRepository.GetAll().Include(x => x.Attributes).ToList();
            return types;
        }

        /// <summary>
        /// Clear the change tracker
        /// </summary>
        public void ClearAllChangeTracker()
        {
            _nodeTypeTerminalTypeRepository?.Context?.ChangeTracker.Clear();
            _libraryLibraryTypeComponentRepository?.Context?.ChangeTracker.Clear();
            _attributeRepository?.Context?.ChangeTracker.Clear();
            _simpleTypeRepository?.Context?.ChangeTracker.Clear();
        }

        /// <summary>
        /// Delete a typeAm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteLibraryType(string id)
        {
            var existingType = await GetLibraryTypeById(id);

            if (existingType == null)
                throw new MimirorgNotFoundException($"Could not delete typeAm with id: {id}. The typeAm was not found.");

            if (existingType is NodeLibDm typeToDelete)
            {
                foreach (var terminalType in typeToDelete.Terminals)
                {
                    _nodeTypeTerminalTypeRepository.Attach(terminalType, EntityState.Deleted);
                }

                await _nodeTypeTerminalTypeRepository.SaveAsync();
            }

            await _libraryLibraryTypeComponentRepository.Delete(existingType.Id);
            await _libraryLibraryTypeComponentRepository.SaveAsync();
        }

        public async Task<LibraryTypeLibCm> GetLibraryType(string searchString)
        {
            var nodes = await _libraryTypeItemRepository.GetNodes(searchString);
            var transports = await _libraryTypeItemRepository.GetTransports(searchString);
            var interfaces = await _libraryTypeItemRepository.GetInterfaces(searchString);
            //TODO: Correct subprojects return when implemented
            var subProjects = new List<SubProjectLibCm>();

            var library = new LibraryTypeLibCm
            {
                Nodes = nodes.ToList(),
                Transports = transports.ToList(),
                Interfaces = interfaces.ToList(),
                SubProjects = subProjects.ToList()
            };

            return library;
        }

        public async Task<IEnumerable<NodeLibCm>> GetNodes()
        {
            return await _libraryTypeItemRepository.GetNodes();
        }

        public async Task<IEnumerable<TransportLibCm>> GetTransports()
        {
            return await _libraryTypeItemRepository.GetTransports();
        }

        public async Task<IEnumerable<InterfaceLibCm>> GetInterfaces()
        {
            return await _libraryTypeItemRepository.GetInterfaces();
        }

        public Task<IEnumerable<SubProjectLibCm>> GetSubProjects(string searchString = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        #region Private

        private async Task<IEnumerable<LibraryTypeLibDm>> CreateLibraryTypes(ICollection<LibraryTypeLibAm> createLibraryTypes, bool createNewFromExistingVersion)
        {
            var createdLibraryTypes = new List<LibraryTypeLibDm>();

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

                LibraryTypeLibDm libraryTypeDm = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeLibDm>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceLibDm>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportLibDm>(createLibraryType),
                    _ => null
                };

                if (libraryTypeDm?.Id == null)
                    return null;

                libraryTypeDm.TypeId = !createNewFromExistingVersion ? libraryTypeDm.Id : createLibraryType.TypeId;

                switch (libraryTypeDm)
                {
                    case NodeLibDm nt:
                        {
                            if (nt.Attributes != null && nt.Attributes.Any())
                            {
                                foreach (var attribute in nt.Attributes)
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
                            await _libraryLibraryTypeComponentRepository.CreateAsync(nt);
                            await _libraryLibraryTypeComponentRepository.SaveAsync();

                            if (nt.Attributes != null && nt.Attributes.Any())
                            {
                                foreach (var attribute in nt.Attributes)
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

                        if (it.Attributes != null && it.Attributes.Any())
                        {
                            foreach (var attribute in it.Attributes)
                            {
                                _attributeRepository.Attach(attribute, EntityState.Unchanged);
                            }
                        }

                        await _libraryLibraryTypeComponentRepository.CreateAsync(it);
                        await _libraryLibraryTypeComponentRepository.SaveAsync();

                        if (it.Attributes != null && it.Attributes.Any())
                        {
                            foreach (var attribute in it.Attributes)
                            {
                                _attributeRepository.Detach(attribute);
                            }
                        }

                        createdLibraryTypes.Add(it);
                        continue;

                    case TransportLibDm tt:
                        {
                            if (tt.Attributes != null && tt.Attributes.Any())
                            {
                                foreach (var attribute in tt.Attributes)
                                {
                                    _attributeRepository.Attach(attribute, EntityState.Unchanged);
                                }
                            }

                            await _libraryLibraryTypeComponentRepository.CreateAsync(tt);
                            await _libraryLibraryTypeComponentRepository.SaveAsync();

                            if (tt.Attributes != null && tt.Attributes.Any())
                            {
                                foreach (var attribute in tt.Attributes)
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

        private async Task<T> CreateLibraryType<T>(LibraryTypeLibAm libraryTypeAm, bool createNewFromExistingVersion) where T : class, new()
        {
            if (libraryTypeAm == null)
                return null;

            var data = (await CreateLibraryTypes(new List<LibraryTypeLibAm> { libraryTypeAm }, createNewFromExistingVersion))?.FirstOrDefault();

            if (data == null)
                throw new MimirorgNullReferenceException("Could not create typeAm");

            var obj = await _libraryTypeItemRepository.GetLibraryItem<T>(data.Id);
            return obj;
        }

        private void SetLibraryTypeVersion(LibraryTypeLibDm newLibraryTypeDm, LibraryTypeLibDm existingLibraryTypeDm)
        {
            if (string.IsNullOrWhiteSpace(newLibraryTypeDm?.Version) || string.IsNullOrWhiteSpace(existingLibraryTypeDm?.Version))
                throw new MimirorgInvalidOperationException("'Null' error when setting version for library typeAm");

            //TODO: The rules for when to trigger major/minor version incrementation is not finalized!

            //typeDm.Version = existingTypeDm.Version.IncrementMinorVersion();

        }

        #endregion Private
    }
}
