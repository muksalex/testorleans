using BankAccountCommon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BankAccountServer
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var host = new HostBuilder().UseOrleans((ctx, siloBuilder) =>
            {
                siloBuilder
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = "Server=tcp:masking-test.database.windows.net,1433;Initial Catalog=Sandip_Orleans;Persist Security Info=False;User ID=maskingadmin;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                })
                .AddAdoNetGrainStorage("SqlServer", options =>
                 {
                     options.Invariant = "System.Data.SqlClient";
                     options.ConnectionString = "Server=tcp:masking-test.database.windows.net,1433;Initial Catalog=Sandip_Orleans;Persist Security Info=False;User ID=maskingadmin;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                 })
                 .AddMemoryGrainStorage("OrleansStorage")
                 .ConfigureServices(services => services.AddSingleton<BankAccountGrain>());
            }).Build();
            host.StartAsync().Wait();

            Console.WriteLine("Press Enter to stop the Orleans Silo host.");
            Console.ReadLine();

            await host.StopAsync();
        }
    }
}
