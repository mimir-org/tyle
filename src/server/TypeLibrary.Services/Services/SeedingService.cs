using Microsoft.Extensions.Logging;
using Mimirorg.TypeLibrary.Models.Application;
using System;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Constants;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;
// ReSharper disable InconsistentNaming

namespace TypeLibrary.Services.Services;

public class SeedingService : ISeedingService
{
    private const string SymbolFileName = "symbol";
    private const string PredefinedAttributeFileName = "attributepredefined";

    private readonly IAttributeService _attributeService;
    private readonly ISymbolService _symbolService;
    private readonly IFileRepository _fileRepository;
    private readonly ILogger<SeedingService> _logger;
    private readonly IRdsService _rdsService;

    public SeedingService(IAttributeService attributeService, ISymbolService symbolService, IFileRepository fileRepository, ILogger<SeedingService> logger, IRdsService rdsService)
    {
        _attributeService = attributeService;
        _symbolService = symbolService;
        _fileRepository = fileRepository;
        _logger = logger;
        _rdsService = rdsService;
    }

    public async Task LoadDataFromFiles()
    {
        try
        {
            var fileList = _fileRepository.ReadJsonFileList().ToList();

            if (!fileList.Any())
                return;

            var attributePredefinedFiles = fileList.Where(x => x.ToLower().Equals(PredefinedAttributeFileName)).ToList();
            var symbolFileNames = fileList.Where(x => x.ToLower().Equals(SymbolFileName)).ToList();
            var attributesPredefined = _fileRepository.ReadAllFiles<AttributePredefinedLibAm>(attributePredefinedFiles).ToList();
            var symbols = _fileRepository.ReadAllFiles<SymbolLibAm>(symbolFileNames).ToList();

            //await _attributeService.CreatePredefined(attributesPredefined);
            await _symbolService.Create(symbols, CreatedBy.Seeding);
            await _rdsService.Initialize();
        }
        catch (Exception e)
        {
            _logger.LogError($"Could not create initial data from file: error: {e.Message}");
        }
    }
}