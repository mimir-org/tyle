using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TypeLibrary.Models.Enums;
using TypeLibrary.Models.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mimirorg.Common.Exceptions;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class LibraryTypeService : ILibraryTypeService
    {
        private readonly INodeTypeTerminalTypeRepository _nodeTypeTerminalTypeRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly ILibraryTypeRepository _libraryTypeComponentRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly ISimpleTypeRepository _simpleTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public LibraryTypeService(INodeTypeTerminalTypeRepository nodeTypeTerminalTypeRepository, ILibraryRepository libraryRepository, 
            ILibraryTypeRepository libraryTypeComponentRepository, IAttributeRepository attributeRepository, ISimpleTypeRepository simpleTypeRepository,
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
        /// Get type by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ignoreNotFound"></param>
        /// <returns></returns>
        public async Task<LibraryType> GetLibraryTypeById(string id, bool ignoreNotFound = false)
        {
            var libraryTypeComponent = await _libraryTypeComponentRepository.GetAsync(id);

            if (!ignoreNotFound && libraryTypeComponent == null)
                throw new MimirorgNotFoundException($"The type with id: {id} could not be found.");

            if (libraryTypeComponent is NodeType)
            {
                return await _libraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<NodeType>()
                    .Include(x => x.TerminalTypes)
                    .Include(x => x.AttributeList)
                    .Include(x => x.SimpleTypes)
                    .ThenInclude(y => y.AttributeList)
                    .FirstOrDefaultAsync();
            }

            if (libraryTypeComponent is TransportType)
            {
                return await _libraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<TransportType>()
                    .Include(x => x.TerminalType)
                    .Include(x => x.AttributeList)
                    .FirstOrDefaultAsync();
            }

            if (libraryTypeComponent is InterfaceType)
            {
                return await _libraryTypeComponentRepository.FindBy(x => x.Id == id)
                    .OfType<InterfaceType>()
                    .Include(x => x.TerminalType)
                    .FirstOrDefaultAsync();
            }

            return libraryTypeComponent;
        }

        /// <summary>
        /// Create library components
        /// </summary>
        /// <param name="createLibraryTypes"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LibraryType>> CreateLibraryTypes(ICollection<CreateLibraryType> createLibraryTypes)
        {
            return await CreateLibraryTypes(createLibraryTypes, false);
        }

        /// <summary>
        /// Create a library component
        /// </summary>
        /// <param name="createLibraryType"></param>
        /// <returns></returns>
        public async Task<T> CreateLibraryType<T>(CreateLibraryType createLibraryType) where T : class, new()
        {
            return await CreateLibraryType<T>(createLibraryType, false);
        }

        /// <summary>
        /// Update a library type based on id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createLibraryType"></param>
        /// <param name="updateMajorVersion"></param>
        /// <param name="updateMinorVersion"></param>
        /// <returns></returns>
        public async Task<T> UpdateLibraryType<T>(string id, CreateLibraryType createLibraryType, bool updateMajorVersion, bool updateMinorVersion) where T : class, new()
        {
            if (string.IsNullOrEmpty(id))
                throw new MimirorgNullReferenceException("Can't update a type without an id");

            if (createLibraryType == null)
                throw new MimirorgNullReferenceException("Can't update a null type");

            var existingType = await GetLibraryTypeById(id);

            if (existingType?.Id == null)
                throw new MimirorgNotFoundException($"There is no type with id:{id} to update.");

            var existingTypeVersions = GetAllLibraryTypes()
                .Where(x => x.TypeId == existingType.TypeId)
                .OrderBy(x => double.Parse(x.Version, CultureInfo.InvariantCulture)).ToList();

            if (double.Parse(existingType.Version, CultureInfo.InvariantCulture) <
                double.Parse(existingTypeVersions[^1].Version, CultureInfo.InvariantCulture))
                throw new MimirorgInvalidOperationException($"Not allowed to edit previous {existingType.Version} version. Latest version is {existingTypeVersions[^1].Version}");

            if (updateMajorVersion || updateMinorVersion)
            {
                createLibraryType.Version = updateMajorVersion ?
                    existingTypeVersions[^1].Version.IncrementMajorVersion() :
                    existingTypeVersions[^1].Version.IncrementMinorVersion();

                createLibraryType.TypeId = existingTypeVersions[0].TypeId;

                return await CreateLibraryType<T>(createLibraryType, true);
            }

            createLibraryType.Version = existingType.Version;
            createLibraryType.TypeId = existingType.TypeId;

            await DeleteLibraryType(id);

            return await CreateLibraryType<T>(createLibraryType, true);
        }

        /// <summary>
        /// Get all library types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CreateLibraryType> GetAllLibraryTypes()
        {
            var nodeTypes = _libraryTypeComponentRepository
                .GetAll()
                .OfType<NodeType>()
                .Include(x => x.TerminalTypes)
                .Include("TerminalTypes.TerminalType")
                .Include(x => x.AttributeList)
                .Include(x => x.SimpleTypes)
                .ThenInclude(y => y.AttributeList)
                .ToList();

            var transportTypes = _libraryTypeComponentRepository
                .GetAll()
                .OfType<TransportType>()
                .Include(x => x.AttributeList)
                .ToList();

            var interfaceType = _libraryTypeComponentRepository
                .GetAll()
                .OfType<InterfaceType>()
                .ToList();

            foreach (var clt in nodeTypes.Select(x => _mapper.Map<CreateLibraryType>(x)))
            {
                yield return clt;
            }

            foreach (var clt in transportTypes.Select(x => _mapper.Map<CreateLibraryType>(x)))
            {
                yield return clt;
            }

            foreach (var clt in interfaceType.Select(x => _mapper.Map<CreateLibraryType>(x)))
            {
                yield return clt;
            }
        }

        /// <summary>
        /// Convert a LibraryType to CreateLibraryType
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<CreateLibraryType> ConvertToCreateLibraryType(string id, LibraryFilter filter)
        {
            switch (filter)
            {
                case LibraryFilter.Node:
                    var nodeItem = await _libraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<NodeType>()
                        .Include(x => x.TerminalTypes)
                        .Include("TerminalTypes.TerminalType")
                        .Include(x => x.AttributeList)
                        .Include(x => x.SimpleTypes)
                        .FirstOrDefaultAsync();

                    if (nodeItem == null)
                        throw new MimirorgNotFoundException($"There is no type with id: {id} and filter: {filter}");

                    return _mapper.Map<CreateLibraryType>(nodeItem);

                case LibraryFilter.Interface:
                    var interfaceItem = await _libraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<InterfaceType>()
                        .FirstOrDefaultAsync();

                    if (interfaceItem == null)
                        throw new MimirorgNotFoundException($"There is no type with id: {id} and filter: {filter}");

                    return _mapper.Map<CreateLibraryType>(interfaceItem);

                case LibraryFilter.Transport:
                    var transportItem = await _libraryTypeComponentRepository
                        .FindBy(x => x.Id == id)
                        .OfType<TransportType>()
                        .Include(x => x.AttributeList)
                        .FirstOrDefaultAsync();

                    if (transportItem == null)
                        throw new MimirorgNotFoundException($"There is no type with id: {id} and filter: {filter}");

                    return _mapper.Map<CreateLibraryType>(transportItem);

                default:
                    throw new MimirorgInvalidOperationException("Filter type mismatch");
            }
        }

        /// <summary>
        /// Create a simple type
        /// </summary>
        /// <param name="simpleType"></param>
        /// <returns></returns>
        public async Task<SimpleType> CreateSimpleType(SimpleTypeAm simpleType)
        {
            var newType = _mapper.Map<SimpleType>(simpleType);
            var existingType = await _simpleTypeRepository.GetAsync(newType.Id);

            if (existingType != null)
                throw new MimirorgDuplicateException($"Type with name {simpleType.Name} already exist.");

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
        /// <param name="simpleTypes"></param>
        /// <returns></returns>
        public async Task CreateSimpleTypes(ICollection<SimpleTypeAm> simpleTypes)
        {
            if (simpleTypes == null || !simpleTypes.Any())
                return;

            foreach (var typeAm in simpleTypes)
            {
                var newType = _mapper.Map<SimpleType>(typeAm);
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
        public IEnumerable<SimpleType> GetSimpleTypes()
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
        /// Delete a type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteLibraryType(string id)
        {
            var existingType = await GetLibraryTypeById(id);

            if (existingType == null)
                throw new MimirorgNotFoundException($"Could not delete type with id: {id}. The type was not found.");

            if (existingType is NodeType typeToDelete)
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

        public async Task<Library> GetLibraryTypes(string searchString)
        {
            var objectBlocks = await _libraryRepository.GetNodeTypes(searchString);
            var transports = await _libraryRepository.GetTransportTypes(searchString);
            var interfaces = await _libraryRepository.GetInterfaceTypes(searchString);
            //TODO: Correct subprojects return when implemented
            var subProjects = new List<LibrarySubProjectItem>();

            var library = new Library
            {
                ObjectBlocks = objectBlocks.ToList(),
                Transports = transports.ToList(),
                Interfaces = interfaces.ToList(),
                SubProjects = subProjects.ToList()
            };

            return library;
        }

        public async Task<IEnumerable<LibraryNodeItem>> GetNodeTypes()
        {
            return await _libraryRepository.GetNodeTypes();
        }

        public async Task<IEnumerable<LibraryTransportItem>> GetTransportTypes()
        {
            return await _libraryRepository.GetTransportTypes();
        }

        public async Task<IEnumerable<LibraryInterfaceItem>> GetInterfaceTypes()
        {
            return await _libraryRepository.GetInterfaceTypes();
        }

        public Task<IEnumerable<LibrarySubProjectItem>> GetSubProjects(string searchString = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        #region Private

        private async Task<IEnumerable<LibraryType>> CreateLibraryTypes(ICollection<CreateLibraryType> createLibraryTypes, bool createNewFromExistingVersion)
        {
            var createdLibraryTypes = new List<LibraryType>();

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

                LibraryType libraryType = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeType>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceType>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportType>(createLibraryType),
                    _ => null
                };

                if (libraryType?.Id == null)
                    return null;

                libraryType.TypeId = !createNewFromExistingVersion ? libraryType.Id : createLibraryType.TypeId;

                switch (libraryType)
                {
                    case NodeType nt:
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

                    case InterfaceType it:

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

                    case TransportType tt:
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

        private async Task<T> CreateLibraryType<T>(CreateLibraryType createLibraryType, bool createNewFromExistingVersion) where T : class, new()
        {
            if (createLibraryType == null)
                return null;

            var data = (await CreateLibraryTypes(new List<CreateLibraryType> { createLibraryType }, createNewFromExistingVersion))?.FirstOrDefault();

            if (data == null)
                throw new MimirorgNullReferenceException("Could not create type");

            var obj = await _libraryRepository.GetLibraryItem<T>(data.Id);
            return obj;
        }

        private void SetLibraryTypeVersion(LibraryType newLibraryType, LibraryType existingLibraryType)
        {
            if (string.IsNullOrWhiteSpace(newLibraryType?.Version) || string.IsNullOrWhiteSpace(existingLibraryType?.Version))
                throw new MimirorgInvalidOperationException("'Null' error when setting version for library type");

            //TODO: The rules for when to trigger major/minor version incrementation is not finalized!

            //libraryType.Version = existingType.Version.IncrementMinorVersion();

        }

        #endregion Private
    }
}
