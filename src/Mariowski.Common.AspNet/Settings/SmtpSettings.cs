using Mariowski.Common.Markers;

namespace Mariowski.Common.AspNet.Settings
{
    public class SmtpSettings : ISettings
    {
        public const string SectionName = "SMTP";

        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}