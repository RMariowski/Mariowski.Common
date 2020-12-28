using System;
using System.IO;
using Xunit;

namespace Mariowski.Common.LiteDb.Tests
{
    public sealed class DatabaseFixture : IDisposable
    {
        private const string TestDatabaseFileName = "Test.db";
        public const string CollectionName = "Database collection";

        public TestDbContext TestDbContext { get; }

        public DatabaseFixture()
        {
            TestDbContext = CreateTestDbContext();
        }

        public void Dispose()
        {
            TestDbContext.Database.Dispose();
        }

        private static TestDbContext CreateTestDbContext()
        {
            const string newDatabasePath = "Temp/Tests/";

            Directory.CreateDirectory(newDatabasePath);

            var newDatabaseFilePath = $"{newDatabasePath}{TestDatabaseFileName}";
            if (File.Exists(newDatabaseFilePath))
                File.Delete(newDatabaseFilePath);

            var dbContext = new TestDbContext(newDatabaseFilePath);
            return dbContext;
        }
    }

    [CollectionDefinition(DatabaseFixture.CollectionName)]
    public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
    {
    }
}