using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TypeLibrary.Data.Contracts;
using Mimirorg.TypeLibrary.Models.Application;
using TypeLibrary.Services.Contracts;
using ITransportService = TypeLibrary.Services.Contracts.ITransportService;

namespace TypeLibrary.Services.Services
{
    public class SeedingService : ISeedingService
    {
        private const string AttributeFileName = "attribute";
        private const string SymbolFileName = "symbol";
        private const string AttributeConditionFileName = "attributecondition";
        private const string AttributeFormatFileName = "attributeformat";
        private const string AttributeAspectFileName = "attributeaspect";
        private const string PredefinedAttributeFileName = "attributepredefined";
        private const string PurposeFileName = "purpose";
        private const string AttributeQualifierFileName = "attributequalifier";
        private const string RdsFileName = "rds";
        private const string SimpleFileName = "simple";
        private const string AttributeSourceFileName = "attributesource";
        private const string TerminalTypeFileName = "terminal";
        private const string TransportFileName = "transport";
        private const string UnitFileName = "unit";

        private readonly IAttributeService _attributeService;
        private readonly ISymbolService _symbolService;
        private readonly IPurposeService _purposeService;
        private readonly IUnitService _unitService;
        private readonly IRdsService _rdsService;
        private readonly ITerminalService _terminalService;
        private readonly ITransportService _transportService;
        private readonly ISimpleService _simpleService;
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<SeedingService> _logger;

        public SeedingService(IAttributeService attributeService, ISymbolService symbolService, IPurposeService purposeService, IUnitService unitService,
            IRdsService rdsService, ITerminalService terminalService, ITransportService transportService, IFileRepository fileRepository, ILogger<SeedingService> logger, ISimpleService simpleService)
        {
            _attributeService = attributeService;
            _symbolService = symbolService;
            _purposeService = purposeService;
            _unitService = unitService;
            _rdsService = rdsService;
            _terminalService = terminalService;
            _transportService = transportService;
            _fileRepository = fileRepository;
            _logger = logger;
            _simpleService = simpleService;
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
                var unitFiles = fileList.Where(x => x.ToLower().Equals(UnitFileName)).ToList();

                var attributeFiles = fileList.Where(x => x.ToLower().Equals(AttributeFileName)).ToList();
                var attributePredefinedFiles = fileList.Where(x => x.ToLower().Equals(PredefinedAttributeFileName)).ToList();
                var terminalFiles = fileList.Where(x => x.ToLower().Equals(TerminalTypeFileName)).ToList();
                var rdsFiles = fileList.Where(x => x.ToLower().Equals(RdsFileName)).ToList();

                var symbolFileNames = fileList.Where(x => x.ToLower().Equals(SymbolFileName)).ToList();
                var simpleFileNames = fileList.Where(x => x.ToLower().Equals(SimpleFileName)).ToList();
                var transportFiles = fileList.Where(x => x.ToLower().Equals(TransportFileName)).ToList();

                var attributeConditions = _fileRepository.ReadAllFiles<AttributeConditionLibAm>(attributeConditionFiles).ToList();
                var attributeFormats = _fileRepository.ReadAllFiles<AttributeFormatLibAm>(attributeFormatFiles).ToList();
                var attributeQualifiers = _fileRepository.ReadAllFiles<AttributeQualifierLibAm>(attributeQualifierFiles).ToList();
                var attributeSources = _fileRepository.ReadAllFiles<AttributeSourceLibAm>(attributeSourceFiles).ToList();
                var attributeAspects = _fileRepository.ReadAllFiles<AttributeAspectLibAm>(attributeAspectFiles).ToList();
                var purposes = _fileRepository.ReadAllFiles<PurposeLibAm>(purposeFiles).ToList();
                var units = _fileRepository.ReadAllFiles<UnitLibAm>(unitFiles).ToList();

                var attributes = _fileRepository.ReadAllFiles<AttributeLibAm>(attributeFiles).ToList();
                var attributesPredefined = _fileRepository.ReadAllFiles<AttributePredefinedLibAm>(attributePredefinedFiles).ToList();
                var terminals = _fileRepository.ReadAllFiles<TerminalLibAm>(terminalFiles).ToList();
                var rds = _fileRepository.ReadAllFiles<RdsLibAm>(rdsFiles).ToList();
                var symbols = _fileRepository.ReadAllFiles<SymbolLibAm>(symbolFileNames).ToList();
                var simple = _fileRepository.ReadAllFiles<SimpleLibAm>(simpleFileNames).ToList();
                var transports = _fileRepository.ReadAllFiles<TransportLibAm>(transportFiles).ToList();

                await _purposeService.CreatePurposes(purposes, true);
                await _unitService.CreateUnits(units, true);
                await _attributeService.Create(attributes, true);
                await _attributeService.CreatePredefined(attributesPredefined, true);
                await _attributeService.CreateAspects(attributeAspects, true);
                await _attributeService.CreateConditions(attributeConditions, true);
                await _attributeService.CreateQualifiers(attributeQualifiers, true);
                await _attributeService.CreateSources(attributeSources, true);
                await _attributeService.CreateFormats(attributeFormats, true);
                await _terminalService.CreateTerminals(terminals, true);
                await _rdsService.CreateRdsAsync(rds, true);
                await _symbolService.CreateSymbol(symbols, true);

                _simpleService.ClearAllChangeTrackers();
                await _simpleService.CreateSimple(simple, true);

                _transportService.ClearAllChangeTrackers();
                await _transportService.Create(transports, true);
            }
            catch (Exception e)
            {
                _logger.LogError($"Could not create initial data from file: error: {e.Message}");
            }
        }
    }
}