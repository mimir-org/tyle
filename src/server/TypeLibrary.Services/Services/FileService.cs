using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Mimirorg.Common.Extensions;
using Mimirorg.TypeLibrary.Enums;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;
using ILibraryTypeService = TypeLibrary.Services.Contracts.ILibraryTypeService;

namespace TypeLibrary.Services.Services
{
    public class FileService : IFileService
    {
        private readonly ILibraryTypeRepository _libraryLibraryTypeRepository;
        private readonly ILibraryTypeService _libraryTypeService;
        private readonly IMapper _mapper;

        public FileService(ILibraryTypeRepository libraryLibraryTypeRepository, ILibraryTypeService libraryTypeService, IMapper mapper)
        {
            _libraryLibraryTypeRepository = libraryLibraryTypeRepository;
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
            var types = stream.ToArray().Deserialize<List<LibraryTypeLibAm>>();
            await CreateLibraryTypeComponentsAsync(types);
        }

        /// <summary>
        /// Create a file from all types
        /// </summary>
        /// <returns></returns>
        public byte[] CreateFile()
        {
            var types = _libraryTypeService.GetAllLibraryTypes().ToList();
            return types.Serialize();
        }

        #region Private

        private async Task CreateLibraryTypeComponentsAsync(ICollection<LibraryTypeLibAm> libraryTypes)
        {
            var existingTypes = _libraryLibraryTypeRepository.GetAll().ToList();
            var newTypes = new List<LibraryTypeLibAm>();
            
            foreach (var createLibraryType in libraryTypes)
            {
                LibraryTypeLibDm libraryTypeDm = createLibraryType.ObjectType switch
                {
                    ObjectType.ObjectBlock => _mapper.Map<NodeLibDm>(createLibraryType),
                    ObjectType.Interface => _mapper.Map<InterfaceLibDm>(createLibraryType),
                    ObjectType.Transport => _mapper.Map<TransportLibDm>(createLibraryType),
                    _ => null
                };

                if(libraryTypeDm == null || existingTypes.Any(x => x.Id == libraryTypeDm.Id))
                    continue;

                newTypes.Add(createLibraryType);
            }
            if(newTypes.Any())
                await _libraryTypeService.CreateLibraryTypes(newTypes);
        }

        #endregion Private
    }
}
