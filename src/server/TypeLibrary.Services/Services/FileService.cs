using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TypeLibrary.Services.Contracts;
using ILibraryService = TypeLibrary.Services.Contracts.ILibraryService;

namespace TypeLibrary.Services.Services
{
    public class FileService : IFileService
    {
        private readonly ILibraryService _libraryTypeService;
        private readonly IMapper _mapper;

        public FileService(ILibraryService libraryTypeService, IMapper mapper)
        {
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
            throw new NotImplementedException();
            //await using var stream = new MemoryStream();
            //await file.CopyToAsync(stream, cancellationToken);
            //var types = stream.ToArray().Deserialize<List<LibraryTypeLibAm>>();
            //await CreateLibraryTypeComponentsAsync(types);
        }

        /// <summary>
        /// Create a file from all types
        /// </summary>
        /// <returns></returns>
        public byte[] CreateFile()
        {
            throw new NotImplementedException();
            //var types = _libraryTypeService.GetAllLibraryTypes().ToList();
            //return types.Serialize();
        }

        #region Private

        //private async Task CreateLibraryTypeComponentsAsync(ICollection<LibraryTypeLibAm> libraryTypes)
        //{
        //    var existingTypes = _libraryLibraryTypeRepository.GetAll().ToList();
        //    var newTypes = new List<LibraryTypeLibAm>();
            
        //    foreach (var createLibraryType in libraryTypes)
        //    {
        //        LibraryTypeLibDm libraryTypeDm = createLibraryType.ObjectType switch
        //        {
        //            ObjectType.ObjectBlock => _mapper.Map<NodeLibDm>(createLibraryType),
        //            ObjectType.Interface => _mapper.Map<InterfaceLibDm>(createLibraryType),
        //            ObjectType.Transport => _mapper.Map<TransportLibDm>(createLibraryType),
        //            _ => null
        //        };

        //        if(libraryTypeDm == null || existingTypes.Any(x => x.Id == libraryTypeDm.Id))
        //            continue;

        //        newTypes.Add(createLibraryType);
        //    }
        //    if(newTypes.Any())
        //        await _libraryTypeService.CreateLibraryTypes(newTypes);
        //}

        #endregion Private
    }
}
