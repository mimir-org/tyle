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
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class LibraryTypeFileService : ILibraryTypeFileService
    {
        private readonly ILibraryTypeRepository _libraryTypeRepository;
        private readonly ILibraryTypeService _libraryTypeService;
        private readonly IMapper _mapper;

        public LibraryTypeFileService(ILibraryTypeRepository libraryTypeRepository, ILibraryTypeService libraryTypeService, IMapper mapper)
        {
            _libraryTypeRepository = libraryTypeRepository;
            _libraryTypeService = libraryTypeService;
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
            var types = stream.ToArray().Deserialize<List<CreateLibraryType>>();
            await CreateLibraryTypeComponentsAsync(types);
        }

        /// <summary>
        /// Create a file from all types
        /// </summary>
        /// <returns></returns>
        public byte[] CreateFile()
        {
            var types = _libraryTypeService.GetAllTypes().ToList();
            return types.Serialize();
        }

        #region Private

        private async Task CreateLibraryTypeComponentsAsync(ICollection<CreateLibraryType> libraryTypes)
        {
            var existingTypes = _libraryTypeRepository.GetAll().ToList();
            var newTypes = new List<CreateLibraryType>();
            
            foreach (var createLibraryType in libraryTypes)
            {
                LibraryType libraryType = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeType>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceType>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportType>(createLibraryType),
                    _ => null
                };

                if(libraryType == null || existingTypes.Any(x => x.Id == libraryType.Id))
                    continue;

                newTypes.Add(createLibraryType);
            }
            if(newTypes.Any())
                await _libraryTypeService.CreateLibraryTypes(newTypes);
        }

        #endregion Private
    }
}
