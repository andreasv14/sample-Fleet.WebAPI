using Fleet.Data;
using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Fleet.Api.Test.Helpers
{
    class ModelTest : BaseModel
    {
    }

    class MockFleetDbContext : FleetDbContext
    {
        public DbSet<ModelTest> Models { get; set; }

        public MockFleetDbContext(DbContextOptions options) : base(options)
        {
        }
    }

    class DataSourceHelper
    {
        public static MockFleetDbContext GetSetupInMemoryDbContext()
        {
            // Setup
            var dbName = Guid.NewGuid().ToString();

            DbContextOptions<MockFleetDbContext> options = new DbContextOptionsBuilder<MockFleetDbContext>()
                            .UseInMemoryDatabase(databaseName: dbName).Options;

            return new MockFleetDbContext(options);
        }

    }
}
