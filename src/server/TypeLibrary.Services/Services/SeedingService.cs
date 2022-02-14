using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;
using ILibraryTypeService = TypeLibrary.Services.Contracts.ILibraryTypeService;

namespace TypeLibrary.Services.Services
{
    public class SeedingService : ISeedingService
    {
        public const string AttributeFileName = "attribute";
        public const string BlobFileName = "blob";
        public const string AttributeConditionFileName = "attributecondition";
        public const string AttributeFormatFileName = "attributeformat";
        public const string AttributeAspectFileName = "attributeaspect";
        public const string PredefinedAttributeFileName = "attributepredefined";
        public const string PurposeFileName = "purpose";
        public const string AttributeQualifierFileName = "attributequalifier";
        public const string RdsFileName = "rds";
        public const string RdsCategoryFileName = "rdscategory";
        public const string SimpleFileName = "simple";
        public const string AttributeSourceFileName = "attributesource";
        public const string TerminalTypeFileName = "terminal";
        public const string TransportFileName = "transport";
        public const string UnitFileName = "unit";
        
        private readonly IAttributeService _attributeService;
        private readonly IBlobService _blobService;
        private readonly IAttributeConditionService _attributeConditionService;
        private readonly IAttributeFormatService _attributeFormatService;
        private readonly IAttributeQualifierService _attributeQualifierService;
        private readonly IAttributeSourceService _attributeSourceService;
        private readonly IAttributeAspectService _attributeAspectService;
        private readonly IPurposeService _purposeService;
        private readonly IUnitService _unitService;
        private readonly IRdsService _rdsService;
        private readonly ITerminalService _terminalService;
        private readonly ILibraryTypeService _libraryTypeService;
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<SeedingService> _logger;
        
        public SeedingService(IAttributeService attributeService, IBlobService blobService, IAttributeConditionService attributeConditionService, IAttributeFormatService attributeFormatService, 
            IAttributeQualifierService attributeQualifierService, IAttributeSourceService attributeSourceService, IAttributeAspectService attributeAspectService, IPurposeService purposeService, IUnitService unitService, 
            IRdsService rdsService, ITerminalService terminalService, ILibraryTypeService libraryTypeService, IFileRepository fileRepository, ILogger<SeedingService> logger)
        {
            _attributeService = attributeService;
            _blobService = blobService;
            _attributeConditionService = attributeConditionService;
            _attributeFormatService = attributeFormatService;
            _attributeQualifierService = attributeQualifierService;
            _attributeSourceService = attributeSourceService;
            _attributeAspectService = attributeAspectService;
            _purposeService = purposeService;
            _unitService = unitService;
            _rdsService = rdsService;
            _terminalService = terminalService;
            _libraryTypeService = libraryTypeService;
            _fileRepository = fileRepository;
            _logger = logger;
        }
     
        public async Task LoadDataFromFiles()
        {
            try
            {
                var fileList = _fileRepository.ReadJsonFileList().ToList();

                if (!fileList.Any())
                    return;

                var attributeConditionFiles = fileList.Where(x => x.ToLower().Equals(AttributeConditionFileName)).ToList();
                var attributeFormatFiles = fileList.Where(x => x.ToLower().Equals(AttributeFormatFileName)).ToList();
                var attributeQualifierFiles = fileList.Where(x => x.ToLower().Equals(AttributeQualifierFileName)).ToList();
                var attributeSourceFiles = fileList.Where(x => x.ToLower().Equals(AttributeSourceFileName)).ToList();
                var attributeAspectFiles = fileList.Where(x => x.ToLower().Equals(AttributeAspectFileName)).ToList();
                var purposeFiles = fileList.Where(x => x.ToLower().Equals(PurposeFileName)).ToList();
                var rdsCategoryFiles = fileList.Where(x => x.ToLower().Equals(RdsCategoryFileName)).ToList();
                var unitFiles = fileList.Where(x => x.ToLower().Equals(UnitFileName)).ToList();

                var attributeFiles = fileList.Where(x => x.ToLower().Equals(AttributeFileName)).ToList();
                var attributePredefinedFiles = fileList.Where(x => x.ToLower().Equals(PredefinedAttributeFileName)).ToList();
                var terminalFiles = fileList.Where(x => x.ToLower().Equals(TerminalTypeFileName)).ToList();
                var rdsFiles = fileList.Where(x => x.ToLower().Equals(RdsFileName)).ToList();
                
                var blobFileNames = fileList.Where(x => x.ToLower().Equals(BlobFileName)).ToList();
                var simpleFileNames = fileList.Where(x => x.ToLower().Equals(SimpleFileName)).ToList();
                var transportFiles = fileList.Where(x => x.ToLower().Equals(TransportFileName)).ToList();
                
                
                var attributeConditions = _fileRepository.ReadAllFiles<AttributeConditionLibAm>(attributeConditionFiles).ToList();
                var attributeFormats = _fileRepository.ReadAllFiles<AttributeFormatLibAm>(attributeFormatFiles).ToList();
                var attributeQualifiers = _fileRepository.ReadAllFiles<AttributeQualifierLibAm>(attributeQualifierFiles).ToList();
                var attributeSources = _fileRepository.ReadAllFiles<AttributeSourceLibAm>(attributeSourceFiles).ToList();
                var attributeAspects = _fileRepository.ReadAllFiles<AttributeAspectLibAm>(attributeAspectFiles).ToList();
                var purposes = _fileRepository.ReadAllFiles<PurposeLibAm>(purposeFiles).ToList();
                var rdsCategories = _fileRepository.ReadAllFiles<RdsCategoryLibAm>(rdsCategoryFiles).ToList();
                var units = _fileRepository.ReadAllFiles<UnitLibAm>(unitFiles).ToList();

                var attributes = _fileRepository.ReadAllFiles<AttributeLibAm>(attributeFiles).ToList();
                var attributesPredefined = _fileRepository.ReadAllFiles<AttributePredefinedLibDm>(attributePredefinedFiles).ToList();
                var terminals = _fileRepository.ReadAllFiles<TerminalLibAm>(terminalFiles).ToList();
                var rds = _fileRepository.ReadAllFiles<RdsLibAm>(rdsFiles).ToList();
                var blobs = _fileRepository.ReadAllFiles<BlobLibAm>(blobFileNames).ToList();
                var simple = _fileRepository.ReadAllFiles<SimpleLibAm>(simpleFileNames).ToList();
                var transports = _fileRepository.ReadAllFiles<LibraryTypeLibAm>(transportFiles).ToList();
                
                await _attributeConditionService.CreateAttributeConditions(attributeConditions);
                await _attributeFormatService.CreateAttributeFormats(attributeFormats);
                await _attributeQualifierService.CreateAttributeQualifiers(attributeQualifiers);
                await _attributeSourceService.CreateAttributeSources(attributeSources);
                await _attributeAspectService.CreateAttributeAspects(attributeAspects);
                await _purposeService.CreatePurposes(purposes);
                await _rdsService.CreateRdsCategories(rdsCategories);
                await _unitService.CreateUnits(units);
                
                await _attributeService.CreateAttributes(attributes);
                await _attributeService.CreateAttributesPredefined(attributesPredefined);
                await _terminalService.CreateTerminals(terminals);
                await _rdsService.CreateRdsAsync(rds);
                await _blobService.CreateBlob(blobs);
                await _libraryTypeService.CreateSimpleTypes(simple);

                var existingLibraryTypes = _libraryTypeService.GetAllLibraryTypes().ToList();
                transports = transports.Where(x => existingLibraryTypes.All(y => y.Key != x.Key)).ToList();
                _libraryTypeService.ClearAllChangeTracker();
                await _libraryTypeService.CreateLibraryTypes(transports);
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not create initial data from file: error: {e.Message}");
            }
        }
    }
}