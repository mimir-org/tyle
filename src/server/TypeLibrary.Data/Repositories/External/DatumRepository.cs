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

        private List<string> GetUnitNames()
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

        public Task<UnitLibDm> CreateUnit(UnitLibDm unit)
        {
            throw new System.NotImplementedException();
        }

        #endregion Units
    }
}