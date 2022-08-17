using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Mimirorg.TypeLibrary.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Models;

namespace TypeLibrary.Data.Repositories.External
{
    public class DatumRepository : IAttributeConditionRepository, IAttributeQualifierRepository, IAttributeSourceRepository, IAttributeFormatRepository, IUnitRepository
    {
        private readonly IApplicationSettingsRepository _settings;

        public DatumRepository(IApplicationSettingsRepository settings)
        {
            _settings = settings;
        }

        #region Condition

        public IEnumerable<AttributeConditionLibDm> GetConditions()
        {
            var conditions = new List<AttributeConditionLibDm>
            {
                new()
                {
                    Id = "Minimum".CreateMd5(),
                    Name = "Minimum",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Minimum")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004049",
                    Description = @"A Minimum datum represents a minimal magnitude of the relevant quantity."
                },
                new()
                {
                    Id = "Nominal".CreateMd5(),
                    Name = "Nominal",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Nominal")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004045",
                    Description = @"A Nominal datum represents a conventional (and, in many cases, standardised) magnitude used to classify the relevant artefact."
                },
                new()
                {
                    Id = "Maximum".CreateMd5(),
                    Name = "Maximum",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Maximum")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004048",
                    Description = @"A Maximum datum represents a maximal magnitude of the relevant quantity."
                },
                new()
                {
                    Id = "Actual".CreateMd5(),
                    Name = "Actual",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Actual")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004050",
                    Description = @"An Actual datum represents a singular measured magnitude of the relevant quantity."
                }
            };

            return conditions;
        }

        public Task<AttributeConditionLibDm> CreateCondition(AttributeConditionLibDm format)
        {
            throw new System.NotImplementedException();
        }

        #endregion Condition

        #region Qualifier

        public IEnumerable<AttributeQualifierLibDm> GetQualifiers()
        {
            var conditions = new List<AttributeQualifierLibDm>
            {
                new()
                {
                    Id = "Capacity".CreateMd5(),
                    Name = "Capacity",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/qualifier/{HttpUtility.UrlEncode("Capacity")}",
                    ContentReferences = "",
                    Description = ""
                },
                new()
                {
                    Id = "Operating".CreateMd5(),
                    Name = "Operating",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/qualifier/{HttpUtility.UrlEncode("Operating")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004043",
                    Description = "An Operating datum represents a magnitude of the relevant quantity prescribed for, or reported from, normal operation."
                },
                new()
                {
                    Id = "Rating".CreateMd5(),
                    Name = "Rating",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/qualifier/{HttpUtility.UrlEncode("Rating")}",
                    ContentReferences = "",
                    Description = ""
                },
                new()
                {
                    Id = "Required".CreateMd5(),
                    Name = "Required",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/qualifier/{HttpUtility.UrlEncode("Required")}",
                    ContentReferences = "",
                    Description = ""
                }
            };

            return conditions;
        }

        public Task<AttributeQualifierLibDm> CreateQualifier(AttributeQualifierLibDm qualifier)
        {
            throw new System.NotImplementedException();
        }

        #endregion Qualifier

        #region Source

        public IEnumerable<AttributeSourceLibDm> GetSources()
        {
            var sources = new List<AttributeSourceLibDm>
            {
                new()
                {
                    Id = "Required".CreateMd5(),
                    Name = "Required",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/source/{HttpUtility.UrlEncode("Required")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Design".CreateMd5(),
                    Name = "Design",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/source/{HttpUtility.UrlEncode("Design")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004042",
                    Description = @"A Design datum represents a limit magnitude of the relevant quantity within which full function is expected."
                },
                new()
                {
                    Id = "Calculated".CreateMd5(),
                    Name = "Calculated",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/source/{HttpUtility.UrlEncode("Calculated")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004038",
                    Description = @"A Calculated datum is an output value of a simulation or similar model of behaviour of the relevant quantity."
                },
                new()
                {
                    Id = "Measured".CreateMd5(),
                    Name = "Measured",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/source/{HttpUtility.UrlEncode("Measured")}",
                    ContentReferences = "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004039",
                    Description = @"A Measured datum originates in a monitoring of the relevant quantity."
                },
                new()
                {
                    Id = "Capacity".CreateMd5(),
                    Name = "Capacity",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/source/{HttpUtility.UrlEncode("Capacity")}",
                    ContentReferences = "",
                    Description = @""
                }
            };

            return sources;
        }

        public Task<AttributeSourceLibDm> CreateSource(AttributeSourceLibDm source)
        {
            throw new System.NotImplementedException();
        }

        #endregion Source

        #region Formats

        public IEnumerable<AttributeFormatLibDm> GetFormats()
        {
            var formats = new List<AttributeFormatLibDm>
            {
                new()
                {
                    Id = "Unsigned Float".CreateMd5(),
                    Name = "Unsigned Float",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Unsigned Float")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Float".CreateMd5(),
                    Name = "Float",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Float")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Unsigned Integer".CreateMd5(),
                    Name = "Unsigned Integer",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Unsigned Integer")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Table".CreateMd5(),
                    Name = "Table",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Table")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Selection".CreateMd5(),
                    Name = "Selection",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Selection")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Text and doc reference".CreateMd5(),
                    Name = "Text and doc reference",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Text and doc reference")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "Boolean".CreateMd5(),
                    Name = "Boolean",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Boolean")}",
                    ContentReferences = "",
                    Description = @""
                },
                new()
                {
                    Id = "String".CreateMd5(),
                    Name = "String",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("String")}",
                    ContentReferences = "",
                    Description = @""
                }
            };

            return formats;
        }

        public Task<AttributeFormatLibDm> CreateFormat(AttributeFormatLibDm format)
        {
            throw new System.NotImplementedException();
        }

        #endregion Formats

        #region Units

        public IEnumerable<UnitLibDm> GetUnits()
        {
            var units = new List<UnitLibDm>();
            var url = $"{_settings.ApplicationSemanticUrl}/unit/";

            var name = "%"; var id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "%/min"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "[list]"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "<specific>"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "1:n"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "2x100%"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "2x50%"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "3x50%"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "A"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "bara"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "barg"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "bbl/d"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "CF/hr"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "composite"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "db"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "degC"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "degF"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "FC"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "FO"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "FR"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "ft/sec"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Hz"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "inch"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "IP"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kA"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kA/1sec"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kA/3sec"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Kelvin"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kg/m3"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kV"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kVA"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kVAh"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "kW"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "liter/MSm3"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "m"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "m/s"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "m3/d"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "m3/h"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "micron"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "min"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "mm"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "mm2"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "mS"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "MVA"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "MW"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "N/A"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "No dead pockets"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "None"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "NotSet"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Ohm"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Pascal"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "ppb"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "ppm"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "psi"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "psig"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "rpm"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "S"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "SCF/hr"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "sec"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Sm3/d"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Sm3/h"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "sq.inch"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "V"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Vah"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "W"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "weight %"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });
            name = "Ω"; id = $"{name}-Mb.Models.Data.Enums.Unit".CreateMd5(); units.Add(new UnitLibDm { Id = id, Name = name, Iri = $"{url}{HttpUtility.UrlEncode($"{id}")}", ContentReferences = "", Description = "" });

            return units;
        }

        public Task<UnitLibDm> CreateUnit(UnitLibDm unit)
        {
            throw new System.NotImplementedException();
        }

        #endregion Units
    }
}