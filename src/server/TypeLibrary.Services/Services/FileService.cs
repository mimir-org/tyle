using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Enums;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using Mimirorg.TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class FileService : IFileService
    {
        private readonly ITypeRepository _libraryTypeRepository;
        private readonly ITypeService _typeService;
        private readonly IMapper _mapper;

        public FileService(ITypeRepository libraryTypeRepository, ITypeService typeService, IMapper mapper)
        {
            _libraryTypeRepository = libraryTypeRepository;
            _typeService = typeService;
            _mapper = mapper;
        }

        /// <summary>
        ///  Load types from file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task LoadDataFromFile(IFormFile file, CancellationToken cancellationToken)
        {
            await using var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);
            var types = stream.ToArray().Deserialize<List<TypeLibAm>>();
            await CreateLibraryTypeComponentsAsync(types);
        }

        /// <summary>
        /// Create a file from all types
        /// </summary>
        /// <returns></returns>
        public byte[] CreateFile()
        {
            var types = _typeService.GetAllTypes().ToList();
            return types.Serialize();
        }

        #region Private

        private async Task CreateLibraryTypeComponentsAsync(ICollection<TypeLibAm> libraryTypes)
        {
            var existingTypes = _libraryTypeRepository.GetAll().ToList();
            var newTypes = new List<TypeLibAm>();
            
            foreach (var createLibraryType in libraryTypes)
            {
                TypeLibDm typeDm = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeLibDm>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceLibDm>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportLibDm>(createLibraryType),
                    _ => null
                };

                if(typeDm == null || existingTypes.Any(x => x.Id == typeDm.Id))
                    continue;

                newTypes.Add(createLibraryType);
            }
            if(newTypes.Any())
                await _typeService.CreateTypes(newTypes);
        }

        #endregion Private
    }
}
