using System;
using LiteDB;

namespace Mariowski.Common.LiteDb
{
    public abstract class LiteDbContext : IDisposable
    {
        public LiteDatabase Database { get; }
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Setups mapper
        /// </summary>
        static LiteDbContext()
        {
            BsonMapper.Global.EnumAsInteger = true;
        }

        /// <summary>
        /// Starts LiteDB database using a connection string for file system database.
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        protected LiteDbContext(string connectionString)
        {
            Database = new LiteDatabase(connectionString);
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~LiteDbContext()
        {
            Dispose(false);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
                Database?.Dispose();

            IsDisposed = true;
        }
    }
}