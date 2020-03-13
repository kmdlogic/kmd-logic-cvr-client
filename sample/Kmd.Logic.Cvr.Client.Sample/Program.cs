using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Kmd.Logic.Cvr.Client.Sample
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            InitLogger();

            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .AddUserSecrets(typeof(Program).Assembly)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build()
                    .Get<AppConfiguration>();

                await Run(config).ConfigureAwait(false);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
            {
                Log.Fatal(ex, "Caught a fatal unhandled exception");
            }
#pragma warning restore CA1031 // Do not catch general exception types
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static void InitLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }

        private static async Task Run(AppConfiguration configuration)
        {
            var validator = new ConfigurationValidator(configuration);
            if (!validator.Validate())
            {
                return;
            }

            using (var httpClient = new HttpClient())
            using (var tokenProviderFactory = new LogicTokenProviderFactory(configuration.TokenProvider))
            {
                var cvrClient = new CvrClient(httpClient, tokenProviderFactory, configuration.Cvr);

                var configs = await cvrClient.GetAllCvrConfigurationsAsync().ConfigureAwait(false);
                if (configs == null || configs.Count == 0)
                {
                    Log.Error("There are no CVR configurations defined for this subscription");
                    return;
                }

                CvrProviderConfigurationModel cvrProvider;
                if (configuration.Cvr.CvrConfigurationId == Guid.Empty)
                {
                    if (configs.Count > 1)
                    {
                        Log.Error("There is more than one CVR configuration defined for this subscription");
                        return;
                    }

                    cvrProvider = configs[0];
                    configuration.Cvr.CvrConfigurationId = cvrProvider.Id.Value;
                }
                else
                {
                    cvrProvider = configs.FirstOrDefault(x => x.Id == configuration.Cvr.CvrConfigurationId);

                    if (cvrProvider == null)
                    {
                        Log.Error("Invalid CVR configuration id {Id}", configuration.Cvr.CvrConfigurationId);
                        return;
                    }
                }

                Log.Information("Fetching {Cvr} using configuration {Name}", configuration.CvrNumber, cvrProvider.Name);

                var company = await cvrClient.GetCompanyByCvrAsync(configuration.CvrNumber).ConfigureAwait(false);

                Log.Information("Company data: {@Company}", company);
            }
        }
    }
}