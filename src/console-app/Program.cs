using System.Threading.Tasks;
using console_app.config;
using Microsoft.Extensions.Hosting;

namespace console_app
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var bootstrapper = new Bootstrapper();

            await new HostBuilder()
                .ConfigureHostConfiguration(bootstrapper.ConfigureHost)
                .ConfigureServices(bootstrapper.ConfigureServices)
                .UseConsoleLifetime()
                .RunConsoleAsync();
        }
    }
}