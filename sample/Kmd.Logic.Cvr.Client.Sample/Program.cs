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

            using var httpClient = new HttpClient();
            using var tokenProviderFactory = new LogicTokenProviderFactory(configuration.TokenProvider);
            using var cvrClient = new CvrClient(httpClient, tokenProviderFactory, configuration.Cvr);

            var configs = await cvrClient.GetAllCvrConfigurationsAsync().ConfigureAwait(false);
            if (configs == null || configs.Count == 0)
            {
                Log.Information("There are no CVR configurations defined for this subscription");
                return;
            }

            CvrProviderConfigurationModel cvrProvider;
            if (configuration.Cvr.CvrConfigurationId == Guid.Empty)
            {
                var fakeProviderConfig = await cvrClient.CreateFakeProviderConfiguration($"sample-{Guid.NewGuid()}").ConfigureAwait(false);
                Log.Information("Created Fake Provider configuration with name '{Name}'", fakeProviderConfig.Name);

                cvrProvider = new CvrProviderConfigurationModel
                {
                    Id = fakeProviderConfig.Id,
                    SubscriptionId = fakeProviderConfig.SubscriptionId,
                    Name = fakeProviderConfig.Name,
                    Provider = "Fake Provider",
                };
                configuration.Cvr.CvrConfigurationId = fakeProviderConfig.Id.Value;
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

            if (cvrProvider.Provider == "Fake Provider")
            {
                return;
            }

            Log.Information("Fetching company by id {Id} using configuration {Name}", company.Id, cvrProvider.Name);

            var companyById = await cvrClient.GetCompanyByIdAsync(company.Id).ConfigureAwait(false);

            Log.Information("Fetched one company using two methods: {Success}", company.CvrNumber == companyById.CvrNumber);

            Log.Information("Fetching Production Units for CVR number {Cvr}", configuration.CvrNumber);

            var productionUnits = await cvrClient.GetProductionUnitsAsync(configuration.CvrNumber).ConfigureAwait(false);

            if (productionUnits == null || productionUnits.Count == 0)
            {
                Log.Information("There is no Production Unit defined for this company");
                return;
            }

            Log.Information("Production Units data for Company {@CompanyName}: {@ProductionUnits}", company.CompanyName, productionUnits);

            Log.Information("Fetching Production Unit Detail for production unit number {PNumber}", productionUnits[0].PNumber);

            var productionUnitDetail = await cvrClient.GetProductionUnitDetailAsync(productionUnits[0].PNumber).ConfigureAwait(false);

            Log.Information("Fetching Production Unit Detail for object id {Id}", productionUnitDetail.Id);

            var productionUnitById = await cvrClient.GetProductionUnitDetailByIdAsync(productionUnitDetail.Id).ConfigureAwait(false);

            Log.Information("Fetched one production unit using two methods: {Success}", productionUnitDetail.PNumber == productionUnitById.PNumber);

            Log.Information("Fetching company events using configuration {Name}", cvrProvider.Name);
            var events = await cvrClient.GetAllCompanyEventsAsync(DateTime.Now.AddMonths(-2), DateTime.Today, 1, 100).ConfigureAwait(false);
            Log.Information("Fetched {Amount} company events", events.Count);

            var eventsCount = events.Count;
            if (eventsCount > 0)
            {
                var companyToSubscribe = company.Id;
                Log.Information("Subscribing for events of company with object id {ObjectId}", companyToSubscribe);
                await cvrClient.SubscribeByIdAsync(companyToSubscribe).ConfigureAwait(false);

                var productionUnitToSubscribe = productionUnitDetail.Id;
                Log.Information("Subscribing for events of company's production unit with object id {ObjectId}", productionUnitToSubscribe);
                await cvrClient.SubscribeByIdAsync(productionUnitToSubscribe).ConfigureAwait(false);

                Log.Information("Fetching events for subscribed companies using configuration {Name}", cvrProvider.Name);
                var page = 1;
                var fetchedEventsCount = 0;
                while (fetchedEventsCount == 0)
                {
                    var subscribedEvents = await cvrClient.GetSubscribedCompanyEventsAsync(DateTime.Now.AddMonths(-2), DateTime.Today, page, 100).ConfigureAwait(false);
                    fetchedEventsCount = subscribedEvents.Events.Count;
                    Log.Information("Fetched {Amount} company subscribed events", subscribedEvents.Events.Count);
                    page++;
                }

                var companyToUnSubscribe = company.Id;
                bool success = await cvrClient.UnsubscribeByIdAsync(companyToUnSubscribe).ConfigureAwait(false);

                if (success)
                {
                    Log.Information("Unsubscribed from company events with object id {ObjectId}", companyToUnSubscribe);
                }
            }
        }
    }
}