using Microsoft.AspNetCore.Hosting;

namespace Mariowski.Common.AspNet.Extensions
{
    public static class HostingEnvironmentExtensions
    {
        /// <summary>
        /// Compares the current hosting environment name to 'Test'.
        /// </summary>
        /// <param name="hostingEnvironment">An instance of <see cref="T:Microsoft.AspNetCore.Hosting.IHostingEnvironment" />.</param>
        /// <returns>True if it is the test environment, otherwise false.</returns>
        public static bool IsTest(this IHostingEnvironment hostingEnvironment) 
            => hostingEnvironment.IsEnvironment("Test");
    }
}
