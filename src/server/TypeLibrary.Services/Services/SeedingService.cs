/*using Microsoft.Extensions.Logging;
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

    public SeedingService(IAttributeService attributeService, ISymbolService symbolService, IFileRepository fileRepository, ILogger<SeedingService> logger)
    {
        _attributeService = attributeService;
        _symbolService = symbolService;
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
            
            var symbolFileNames = fileList.Where(x => x.ToLower().Equals(SymbolFileName)).ToList();
            var symbols = _fileRepository.ReadAllFiles<SymbolLibAm>(symbolFileNames).ToList();

            //await _attributeService.CreatePredefined(attributesPredefined);
            await _symbolService.Create(symbols, CreatedBy.Seeding);
        }
        catch (Exception e)
        {
            _logger.LogError($"Could not create initial data from file: error: {e.Message}");
        }
    }
}*/