using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TypeLibrary.Core.Extensions;
using TypeLibrary.Data;
using TypeLibrary.Data.Contracts.Ef;
using TypeLibrary.Data.Models;
using TypeLibrary.Services.Contracts;
using TypeLibrary.Services.Services;
using TypeLibrary.Services.Tests.Repositories;

namespace TypeLibrary.Services.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // Add in memory database
            var options = new DbContextOptionsBuilder<TypeLibraryDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new TypeLibraryDbContext(options);

            // Create some initial data

            #region RDS

            var rds = new RdsLibDm
            {
                Id = "Fake_Rds",
                Name = "Fake Rds",
                Iri = @"https://rds.fake.com/Fake Rds"
            };

            context.Rds.Add(rds);

            #endregion

            #region Purpose

            var purpose = new PurposeLibDm
            {
                Id = "Fake_Purpose",
                Name = "Fake Purpose",
                Iri = @"https://purpose.fake.com/Fake Purpose",
                Created = DateTime.Now,
                CreatedBy = "Test Tester"
            };

            context.Purpose.Add(purpose);

            #endregion

            context.SaveChanges();
            services.AddSingleton(context);

            services.AddSingleton<IEfAttributeRepository, FakeAttributeRepository>();
            services.AddSingleton<IEfNodeRepository, FakeNodeRepository>();
            services.AddSingleton<IEFSimpleRepository, FakeSimpleRepository>();
            services.AddSingleton<IEfTransportRepository, FakeTransportRepository>();
            services.AddSingleton<ITransportService, TransportService>();

            // Add auto-mapper configurations
            services.AddAutoMapperConfigurations();
        }
    }
}
