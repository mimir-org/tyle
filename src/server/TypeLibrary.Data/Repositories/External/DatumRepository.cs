using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Mimirorg.TypeLibrary.Extensions;
using Mimirorg.TypeLibrary.Models.Application;
using Newtonsoft.Json;
using TypeLibrary.Data.Common;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Data.Contracts.Common;
using TypeLibrary.Data.Models;
using TypeLibrary.Data.Models.External;

namespace TypeLibrary.Data.Repositories.External
{
    public class DatumRepository : IAttributeConditionRepository, IAttributeQualifierRepository, IAttributeSourceRepository, IAttributeFormatRepository
    {
        private const string Pca = "PCA";
        private readonly IApplicationSettingsRepository _settings;
        private readonly ICacheRepository _cacheRepository;

        public DatumRepository(IApplicationSettingsRepository settings, ICacheRepository cacheRepository)
        {
            _settings = settings;
            _cacheRepository = cacheRepository;
        }

        #region Condition - Range Specifying Quantity Datum

        public async Task<List<AttributeConditionLibDm>> GetConditions()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_conditions", async () => await FetchDatums<AttributeConditionLibDm>(SparQlWebClient.PcaAttributeConditions, "condition"));
            return data.ToList();
        }

        #endregion Condition - Range Specifying Quantity Datum

        #region Qualifier - Quantity Datum with specified Scope

        public async Task<List<AttributeQualifierLibDm>> GetQualifiers()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_qualifiers", async () => await FetchDatums<AttributeQualifierLibDm>(SparQlWebClient.PcaAttributeQualifiers, "qualifier"));
            return data.ToList();
        }

        #endregion Qualifier - Quantity Datum with specified Scope

        #region Source - Quantity Datum with specified Provenance

        public async Task<List<AttributeSourceLibDm>> GetSources()
        {
            var data = await _cacheRepository.GetOrCreateAsync("pca_sources", async () => await FetchDatums<AttributeSourceLibDm>(SparQlWebClient.PcaAttributeSources, "source"));
            return data.ToList();
        }

        #endregion Source - Quantity Datum with specified Provenance

        #region Formats

        public IEnumerable<AttributeFormatLibDm> GetFormats()
        {
            var formats = new List<AttributeFormatLibDm>
            {
                new()
                {
                    Id = "NotSet".CreateMd5(),
                    Name = "NotSet",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("NotSet")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Unsigned Float".CreateMd5(),
                    Name = "Unsigned Float",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Unsigned Float")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Float".CreateMd5(),
                    Name = "Float",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Float")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Unsigned Integer".CreateMd5(),
                    Name = "Unsigned Integer",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Unsigned Integer")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Table".CreateMd5(),
                    Name = "Table",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Table")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Selection".CreateMd5(),
                    Name = "Selection",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Selection")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Text and doc reference".CreateMd5(),
                    Name = "Text and doc reference",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Text and doc reference")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "Boolean".CreateMd5(),
                    Name = "Boolean",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("Boolean")}",
                    TypeReferences = null,
                    Description = null
                },
                new()
                {
                    Id = "String".CreateMd5(),
                    Name = "String",
                    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/format/{HttpUtility.UrlEncode("String")}",
                    TypeReferences = null,
                    Description = null
                }
            };

            return formats;
        }

        #endregion Formats

        #region Private methods

        private string CreateTypeReferences(string name, string iri, string source)
        {
            var data = new List<TypeReferenceAm>
            {
                new()
                {
                    Name = name,
                    Iri = iri,
                    Source = source
                }
            };

            return JsonConvert.SerializeObject(data);
        }

        private string StripDatumLabel(string label)
        {
            return string.IsNullOrWhiteSpace(label) ?
                null :
                label.Replace("datum", string.Empty, StringComparison.InvariantCultureIgnoreCase).Trim();
        }

        private Task<List<T>> FetchDatums<T>(string query, string typeName) where T : IDatum, new()
        {
            var client = new SparQlWebClient
            {
                EndPoint = SparQlWebClient.PcaEndPoint,
                Query = query
            };

            var datums = new List<T>();
            var data = client.Get<PcaDatum>().ToList();

            if (!data.Any())
                return Task.FromResult(datums);

            // Create a NotSet item in front
            var notSetId = "NotSet".CreateMd5();
            var notSetItem = new T
            {
                Id = notSetId,
                Name = "NotSet",
                Iri = $"{_settings.ApplicationSemanticUrl}/attribute/{typeName}/{notSetId}",
                TypeReferences = null,
                Description = null
            };

            datums.Add(notSetItem);

            foreach (var datum in data)
            {
                var strippedName = StripDatumLabel(datum.Datum_Label);
                var id = $"{strippedName}".CreateMd5();
                var iri = $"{_settings.ApplicationSemanticUrl}/attribute/{typeName}/{id}";
                var typeReferences = CreateTypeReferences(datum.Datum_Label, datum.Datum, Pca);

                var item = new T
                {
                    Id = id,
                    Iri = iri,
                    Description = null,
                    Name = strippedName,
                    TypeReferences = typeReferences
                };

                datums.Add(item);
            }

            return Task.FromResult(datums);


            //var conditions = new List<AttributeConditionLibDm>
            //{
            //new()
            //{
            //    Id = "NotSet".CreateMd5(),
            //    Name = "NotSet",
            //    Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("NotSet")}",
            //    TypeReferences = null,
            //    Description = null
            //},
            //    new()
            //    {
            //        Id = "Minimum".CreateMd5(),
            //        Name = "Minimum",
            //        Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Minimum")}",
            //        TypeReferences = CreateTypeReference(Pca, "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004049"),
            //        Description = @"A Minimum datum represents a minimal magnitude of the relevant quantity."
            //    },
            //    new()
            //    {
            //        Id = "Nominal".CreateMd5(),
            //        Name = "Nominal",
            //        Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Nominal")}",
            //        TypeReferences = CreateTypeReference(Pca, "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004045"),
            //        Description = @"A Nominal datum represents a conventional (and, in many cases, standardised) magnitude used to classify the relevant artefact."
            //    },
            //    new()
            //    {
            //        Id = "Maximum".CreateMd5(),
            //        Name = "Maximum",
            //        Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Maximum")}",
            //        TypeReferences = CreateTypeReference(Pca, "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004048"),
            //        Description = @"A Maximum datum represents a maximal magnitude of the relevant quantity."
            //    },
            //    new()
            //    {
            //        Id = "Actual".CreateMd5(),
            //        Name = "Actual",
            //        Iri = $"{_settings.ApplicationSemanticUrl}/attribute/condition/{HttpUtility.UrlEncode("Actual")}",
            //        TypeReferences = CreateTypeReference(Pca, "http://rds.posccaesar.org/ontology/plm/rdl/PCA_100004050"),
            //        Description = @"An Actual datum represents a singular measured magnitude of the relevant quantity."
            //    }
            //};

            //return conditions;
        }

        #endregion Private methods


    }
}