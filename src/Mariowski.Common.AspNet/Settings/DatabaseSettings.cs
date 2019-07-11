using Mariowski.Common.Markers;

namespace Mariowski.Common.AspNet.Settings
{
    public class DatabaseSettings : ISettings
    {
        public const string SectionName = "Database";
        
        public string ConnectionString { get; set; }
        public bool InMemory { get; set; }
    }
}