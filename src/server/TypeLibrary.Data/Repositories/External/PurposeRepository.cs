using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.External
{
    public class PurposeRepository : IPurposeRepository
    {
        private readonly IApplicationSettingsRepository _settings;
        private readonly ICacheRepository _cacheRepository;

        public PurposeRepository(IApplicationSettingsRepository settings, ICacheRepository cacheRepository)
        {
            _settings = settings;
            _cacheRepository = cacheRepository;
        }

        public async Task<IEnumerable<PurposeLibDm>> Get()
        {
            var data = await _cacheRepository.GetOrCreateAsync("hardcoded_purposes", async () => await FetchPurposes());
            return data;
        }

        #region Private Methods

        private Task<List<PurposeLibDm>> FetchPurposes()
        {
            var purposes = new List<PurposeLibDm>
            {
                new() { Id = "6D5631D342FD7087EFC357950380A927", Name = "Actuate (Automation)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/6D5631D342FD7087EFC357950380A927", Description = null },
                new() { Id = "2F0AE1652BFC13BE4020EF7668125435", Name = "Break (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/2F0AE1652BFC13BE4020EF7668125435", Description = null },
                new() { Id = "401F568751441CFE4372D43259806E5C", Name = "Compress (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/401F568751441CFE4372D43259806E5C", Description = null },
                new() { Id = "C40DA4ED96C364065D607D8FFB578934", Name = "Control (Automation)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/C40DA4ED96C364065D607D8FFB578934", Description = null },
                new() { Id = "B3A8FF6B199B0A4370AF42C9CC3CF4A8", Name = "Convert (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/B3A8FF6B199B0A4370AF42C9CC3CF4A8", Description = null },
                new() { Id = "5A8952301B8CF7C60E23FC6AD22750E0", Name = "Distribute (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/5A8952301B8CF7C60E23FC6AD22750E0", Description = null },
                new() { Id = "27C9ECEF8B184A5C709889851BF83D23", Name = "Distribute (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/27C9ECEF8B184A5C709889851BF83D23", Description = null },
                new() { Id = "E1C727BB796BD853CF566A79E67CE343", Name = "Drive (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/E1C727BB796BD853CF566A79E67CE343", Description = null },
                new() { Id = "A1B9C3F46C74187CB8A84A085C18F08D", Name = "Enclose (Structural)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/A1B9C3F46C74187CB8A84A085C18F08D", Description = null },
                new() { Id = "82AD91759478BD3BE63A108EC72E2FD3", Name = "Generate (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/82AD91759478BD3BE63A108EC72E2FD3", Description = null },
                new() { Id = "06706D4B8ED61C927F861CC56F6583A9", Name = "Heat (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/06706D4B8ED61C927F861CC56F6583A9", Description = null },
                new() { Id = "F7733DA95FCC1E8A373C6134650B44EE", Name = "Heat (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/F7733DA95FCC1E8A373C6134650B44EE", Description = null },
                new() { Id = "DC5F88652145F5B023D136420EB947C2", Name = "Manifold (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/DC5F88652145F5B023D136420EB947C2", Description = null },
                new() { Id = "CD701808905714BA12356D82E4BDAF34", Name = "Measure (Automation)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/CD701808905714BA12356D82E4BDAF34", Description = null },
                new() { Id = "D8ED7A557D50E397C05AEE6A3223321A", Name = "Mix (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/D8ED7A557D50E397C05AEE6A3223321A", Description = null },
                new() { Id = "6ADF97F83ACF6453D4A6A4B1070F3754", Name = "None", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/6ADF97F83ACF6453D4A6A4B1070F3754", Description = null },
                new() { Id = "276B0BE0DF9D5922494D46645C4AB70B", Name = "Pump (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/276B0BE0DF9D5922494D46645C4AB70B", Description = null },
                new() { Id = "6587942D0ED434745D606C3B5B30A4F5", Name = "React (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/6587942D0ED434745D606C3B5B30A4F5", Description = null },
                new() { Id = "60809DFBEEDAB4F88167BC1DF1823987", Name = "Separate (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/60809DFBEEDAB4F88167BC1DF1823987", Description = null },
                new() { Id = "C542FBC7523E54D545785D4097CEACED", Name = "Store (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/C542FBC7523E54D545785D4097CEACED", Description = null },
                new() { Id = "FDAA9A9118CD325D3B28E69C80F3F9F6", Name = "Support (Structural)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/FDAA9A9118CD325D3B28E69C80F3F9F6", Description = null },
                new() { Id = "8B1F08EE0EA4E965FF9052454934F008", Name = "Switch (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/8B1F08EE0EA4E965FF9052454934F008", Description = null },
                new() { Id = "349BA7BC5F81E4811823F1A7AB272522", Name = "Transform (Electrical)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/349BA7BC5F81E4811823F1A7AB272522", Description = null },
                new() { Id = "D5D99392704AC016527336FE66B9FBBC", Name = "Transform (Process)", Iri = $"{_settings.ApplicationSemanticUrl}/purpose/D5D99392704AC016527336FE66B9FBBC", Description = null }
            };

            return Task.FromResult(purposes.OrderBy(x => x.Name, StringComparer.InvariantCultureIgnoreCase).ToList());
        }

        #endregion Private Methods
    }
}