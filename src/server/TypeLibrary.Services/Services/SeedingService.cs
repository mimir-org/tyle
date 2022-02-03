using System;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Models.Application;
using TypeLibrary.Models.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class SeedingService : ISeedingService
    {
        public const string AttributeFileName = "attribute";
        public const string BlobDataFileName = "blobdata";
        public const string ConditionFileName = "condition";
        public const string FormatFileName = "format";
        public const string LocationFileName = "location";
        public const string PredefinedAttributeFileName = "attributepredefined";
        public const string PurposeFileName = "purpose";
        public const string QualifierFileName = "qualifier";
        public const string RdsFileName = "rds";
        public const string RdsCategoryFileName = "rdscategory";
        public const string SimpleTypeFileName = "simpletype";
        public const string SourceFileName = "source";
        public const string TerminalTypeFileName = "terminaltype";
        public const string TransportFileName = "transporttype";
        public const string UnitFileName = "unit";
        
        private readonly IAttributeService _attributeService;
        private readonly IBlobDataService _blobDataService;
        private readonly IConditionService _conditionService;
        private readonly IFormatService _formatService;
        private readonly IQualifierService _qualifierService;
        private readonly ISourceService _sourceService;
        private readonly ILocationService _locationService;
        private readonly IPurposeService _purposeService;
        private readonly IUnitService _unitService;
        private readonly IRdsService _rdsService;
        private readonly ITerminalService _terminalTypeService;
        private readonly ITypeService _typeService;
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<SeedingService> _logger;
        
        public SeedingService(IAttributeService attributeService, IBlobDataService blobDataService, IConditionService conditionService, IFormatService formatService, 
            IQualifierService qualifierService, ISourceService sourceService, ILocationService locationService, IPurposeService purposeService, IUnitService unitService, 
            IRdsService rdsService, ITerminalService terminalTypeService, ITypeService typeService, IFileRepository fileRepository, ILogger<SeedingService> logger)
        {
            _attributeService = attributeService;
            _blobDataService = blobDataService;
            _conditionService = conditionService;
            _formatService = formatService;
            _qualifierService = qualifierService;
            _sourceService = sourceService;
            _locationService = locationService;
            _purposeService = purposeService;
            _unitService = unitService;
            _rdsService = rdsService;
            _terminalTypeService = terminalTypeService;
            _typeService = typeService;
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

                var conditionFiles = fileList.Where(x => x.ToLower().Equals(ConditionFileName)).ToList();
                var formatFiles = fileList.Where(x => x.ToLower().Equals(FormatFileName)).ToList();
                var qualifierFiles = fileList.Where(x => x.ToLower().Equals(QualifierFileName)).ToList();
                var sourceFiles = fileList.Where(x => x.ToLower().Equals(SourceFileName)).ToList();
                var locationFiles = fileList.Where(x => x.ToLower().Equals(LocationFileName)).ToList();
                var purposeFiles = fileList.Where(x => x.ToLower().Equals(PurposeFileName)).ToList();
                var rdsCategoryFiles = fileList.Where(x => x.ToLower().Equals(RdsCategoryFileName)).ToList();
                var unitFiles = fileList.Where(x => x.ToLower().Equals(UnitFileName)).ToList();

                var attributeFiles = fileList.Where(x => x.ToLower().Equals(AttributeFileName)).ToList();
                var attributePredefinedFiles = fileList.Where(x => x.ToLower().Equals(PredefinedAttributeFileName)).ToList();
                var terminalTypeFiles = fileList.Where(x => x.ToLower().Equals(TerminalTypeFileName)).ToList();
                var rdsFiles = fileList.Where(x => x.ToLower().Equals(RdsFileName)).ToList();
                
                var blobDataFileNames = fileList.Where(x => x.ToLower().Equals(BlobDataFileName)).ToList();
                var simpleTypeFileNames = fileList.Where(x => x.ToLower().Equals(SimpleTypeFileName)).ToList();
                var transportFiles = fileList.Where(x => x.ToLower().Equals(TransportFileName)).ToList();
                
                
                var conditions = _fileRepository.ReadAllFiles<ConditionLibAm>(conditionFiles).ToList();
                var formats = _fileRepository.ReadAllFiles<FormatLibAm>(formatFiles).ToList();
                var qualifiers = _fileRepository.ReadAllFiles<QualifierLibAm>(qualifierFiles).ToList();
                var sources = _fileRepository.ReadAllFiles<SourceLibAm>(sourceFiles).ToList();
                var locations = _fileRepository.ReadAllFiles<LocationLibAm>(locationFiles).ToList();
                var purposes = _fileRepository.ReadAllFiles<PurposeLibAm>(purposeFiles).ToList();
                var rdsCategories = _fileRepository.ReadAllFiles<RdsCategoryLibAm>(rdsCategoryFiles).ToList();
                var units = _fileRepository.ReadAllFiles<UnitLibAm>(unitFiles).ToList();

                var attributes = _fileRepository.ReadAllFiles<AttributeLibAm>(attributeFiles).ToList();
                var attributesPredefined = _fileRepository.ReadAllFiles<AttributePredefinedLibDm>(attributePredefinedFiles).ToList();
                var terminalTypes = _fileRepository.ReadAllFiles<TerminalLibAm>(terminalTypeFiles).ToList();
                var rds = _fileRepository.ReadAllFiles<RdsLibAm>(rdsFiles).ToList();
                var blobData = _fileRepository.ReadAllFiles<BlobDataLibAm>(blobDataFileNames).ToList();
                var simpleTypes = _fileRepository.ReadAllFiles<SimpleLibAm>(simpleTypeFileNames).ToList();
                var transports = _fileRepository.ReadAllFiles<TypeLibAm>(transportFiles).ToList();
                
                await _conditionService.CreateConditions(conditions);
                await _formatService.CreateFormats(formats);
                await _qualifierService.CreateQualifiers(qualifiers);
                await _sourceService.CreateSources(sources);
                await _locationService.CreateLocations(locations);
                await _purposeService.CreatePurposes(purposes);
                await _rdsService.CreateRdsCategories(rdsCategories);
                await _unitService.CreateUnits(units);
                
                await _attributeService.CreateAttributes(attributes);
                await _attributeService.CreateAttributesPredefined(attributesPredefined);
                await _terminalTypeService.CreateTerminalTypes(terminalTypes);
                await _rdsService.CreateRdsAsync(rds);
                await _blobDataService.CreateBlobData(blobData);
                await _typeService.CreateSimpleTypes(simpleTypes);

                var existingLibraryTypes = _typeService.GetAllTypes().ToList();
                transports = transports.Where(x => existingLibraryTypes.All(y => y.Key != x.Key)).ToList();
                _typeService.ClearAllChangeTracker();
                await _typeService.CreateTypes(transports);
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not create initial data from file: error: {e.Message}");
            }
        }
    }
}