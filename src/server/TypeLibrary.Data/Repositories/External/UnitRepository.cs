using System.Collections.Generic;
using System.Threading.Tasks;
using Mimirorg.TypeLibrary.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.External
{
    public class UnitRepository : IUnitRepository
    {
        private readonly IApplicationSettingsRepository _settings;

        public UnitRepository(IApplicationSettingsRepository settings)
        {
            _settings = settings;
        }

        #region Units

        public IEnumerable<UnitLibDm> GetUnits()
        {
            var url = $"{_settings.ApplicationSemanticUrl}/unit/";
            const string salt = "Mb.Models.Data.Enums.Unit";

            var unitNames = GetUnitNames();

            foreach (var unitName in unitNames)
            {
                var id = $"{unitName}-{salt}".CreateMd5();

                yield return new UnitLibDm
                {
                    Id = id,
                    Name = unitName,
                    Iri = $"{url}{id}"
                };
            }
        }

        public Task<UnitLibDm> CreateUnit(UnitLibDm unit)
        {
            throw new System.NotImplementedException();
        }

        #endregion Units

        #region Private methods

        private static List<string> GetUnitNames()
        {
            return new List<string>
            {
                "[list]",
                "%",
                "%/min",
                "<specific>",
                "1:n",
                "2x100%",
                "2x50%",
                "3x50%",
                "A",
                "bara",
                "barg",
                "bbl/d",
                "CF/hr",
                "composite",
                "db",
                "degC",
                "degF",
                "FC",
                "FO",
                "FR",
                "ft/sec",
                "Hz",
                "inch",
                "IP",
                "kA",
                "kA/1sec",
                "kA/3sec",
                "Kelvin",
                "kg/m3",
                "kV",
                "kVA",
                "kVAh",
                "kW",
                "liter/MSm3",
                "m",
                "m/s",
                "m3/d",
                "m3/h",
                "micron",
                "min",
                "mm",
                "mm2",
                "mS",
                "MVA",
                "MW",
                "N/A",
                "No dead pockets",
                "None",
                "NotSet",
                "Ohm",
                "Pascal",
                "ppb",
                "ppm",
                "psi",
                "psig",
                "rpm",
                "S",
                "SCF/hr",
                "sec",
                "Sm3/d",
                "Sm3/h",
                "sq.inch",
                "V",
                "Vah",
                "W",
                "weight %",
                "Ω"
            };
        }

        #endregion
    }
}