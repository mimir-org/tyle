using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mb.Models.Application;
using Mb.Models.Application.TypeEditor;
using Mb.Models.Data.Enums;
using Mb.Models.Enums;
using Mb.Models.Extensions;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class EnumService : IEnumService
    {
        private readonly IEnumBaseRepository _enumBaseRepository;

        public EnumService(IEnumBaseRepository enumBaseRepository)
        {
            _enumBaseRepository = enumBaseRepository;
        }

        /// <summary>
        /// Create an enum
        /// </summary>
        /// <param name="createEnum"></param>
        /// <returns></returns>
        public async Task<EnumBase> CreateEnum(CreateEnum createEnum)
        {
            var enumToDb = createEnum.CreateEnum();
            enumToDb.Id = enumToDb.Key.CreateMd5();
            await _enumBaseRepository.CreateAsync(enumToDb);
            await _enumBaseRepository.SaveAsync();
            return enumToDb;
        }

        /// <summary>
        /// Get all enums of given type
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public IEnumerable<EnumBase> GetAllOfType(EnumType enumType)
        {
            var enumT = enumType.GetEnumTypeFromEnum();
            var method = typeof(Queryable).GetMethod("OfType");
            var generic = method?.MakeGenericMethod(new Type[] { enumT });
            var result = (IEnumerable<EnumBase>)generic?.Invoke(null, new object[] { _enumBaseRepository.GetAll() });
            return result?.ToList();
        }

        /// <summary>
        /// Get all location predefined types
        /// </summary>
        /// <returns></returns>
        public IEnumerable<LocationTypeAm> GetAllLocationTypes()
        {
            var locationTypes = GetAllOfType(EnumType.PredefinedAttributeCategory).OfType<TypeAttribute>().OrderBy(x => x.Name).ToList();
            var mainTypes = locationTypes.Where(x => x.ParentId == null).ToList();
            foreach (var category in mainTypes)
            {
                var locationType = ConvertLocationType(category);
                locationType.LocationSubTypes = locationTypes.Where(x => x.ParentId == category.Id).OrderBy(_ => category.Name).Select(ConvertLocationType).ToList();
                yield return locationType;
            }
        }

        private LocationTypeAm ConvertLocationType(TypeAttribute typeAttribute)
        {
            return new()
            {
                Id = typeAttribute.Id,
                Description = typeAttribute.Description,
                Name = typeAttribute.Name,
                SemanticReference = typeAttribute.SemanticReference,
                LocationSubTypes = new List<LocationTypeAm>()
            };
        }
    }
}
