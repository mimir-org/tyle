﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TypeLibrary.Data.Contracts;
using TypeLibrary.Models.Application;
using TypeLibrary.Models.Data;
using TypeLibrary.Services.Contracts;

namespace TypeLibrary.Services.Services
{
    public class TerminalTypeService : ITerminalTypeService
    {
        private readonly ITerminalTypeRepository _terminalTypeRepository;
        private readonly IAttributeTypeRepository _attributeTypeRepository;
        private readonly IMapper _mapper;

        public TerminalTypeService(ITerminalTypeRepository terminalTypeRepository, IAttributeTypeRepository attributeTypeRepository, IMapper mapper)
        {
            _terminalTypeRepository = terminalTypeRepository;
            _attributeTypeRepository = attributeTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all terminals
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TerminalType> GetTerminals()
        {
            return _terminalTypeRepository.GetAll()
                //.Include(x => x.TerminalCategory)
                .Include(x => x.Attributes)
                .ToList();
        }


        //TODO: Returner en
        /// <summary>
        /// Get all terminals
        /// </summary>
        /// <returns></returns>
        public List<TerminalType> GetTerminalsByCategory()
        {
            var result = _terminalTypeRepository.GetAll()
                .Include(x => x.Attributes)
                .Include(x => x.Children)
                .ToList();
            
            return result;
        }

        /// <summary>
        /// Create a terminal type
        /// </summary>
        /// <param name="createTerminalType"></param>
        /// <returns></returns>
        public async Task<TerminalType> CreateTerminalType(CreateTerminalType createTerminalType)
        {
            var data = await CreateTerminalTypes(new List<CreateTerminalType> { createTerminalType });
            return data.SingleOrDefault();
        }

        /// <summary>
        /// Create from a list of terminal types
        /// </summary>
        /// <param name="createTerminalTypes"></param>
        /// <returns></returns>
        public async Task<List<TerminalType>> CreateTerminalTypes(List<CreateTerminalType> createTerminalTypes)
        {
            if (createTerminalTypes == null || !createTerminalTypes.Any())
                return new List<TerminalType>();

            var data = _mapper.Map<List<TerminalType>>(createTerminalTypes);
            var existing = _terminalTypeRepository.GetAll().ToList();
            var notExisting = data.Where(x => existing.All(y => y.Id != x.Id)).ToList();

            if (!notExisting.Any())
                return new List<TerminalType>();

            foreach (var entity in notExisting)
            {
                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeTypeRepository.Attach(entityAttribute, EntityState.Unchanged);
                }

                await _terminalTypeRepository.CreateAsync(entity);
                await _terminalTypeRepository.SaveAsync();

                foreach (var entityAttribute in entity.Attributes)
                {
                    _attributeTypeRepository.Detach(entityAttribute);
                }
            }

            return data;
        }
    }
}
