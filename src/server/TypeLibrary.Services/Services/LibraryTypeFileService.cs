using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using TypeLibrary.Models.Enums;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using TypeLibrary.Data.Contracts;

using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class LibraryTypeFileService : ILibraryTypeFileService
    {
        private readonly ILibraryTypeRepository _libraryTypeRepository;
        private readonly ITypeService _typeService;
        private readonly IMapper _mapper;

        public LibraryTypeFileService(ILibraryTypeRepository libraryTypeRepository, ITypeService typeService, IMapper mapper)
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
            var types = stream.ToArray().Deserialize<List<TypeAm>>();
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

        private async Task CreateLibraryTypeComponentsAsync(ICollection<TypeAm> libraryTypes)
        {
            var existingTypes = _libraryTypeRepository.GetAll().ToList();
            var newTypes = new List<TypeAm>();
            
            foreach (var createLibraryType in libraryTypes)
            {
                TypeDm typeDm = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeDm>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceDm>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportDm>(createLibraryType),
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
