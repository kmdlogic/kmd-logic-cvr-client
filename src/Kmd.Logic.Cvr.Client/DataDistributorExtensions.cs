using System;
using System.IO;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;

namespace Kmd.Logic.Cvr.Client
{
    public static class DataDistributorExtensions
    {
        /// <summary>
        /// Creates Data Distributor CVR configuration.
        /// </summary>
        /// <param name="cvrClient">CVR Client.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <returns>Created configuration.</returns>
        public static async Task<CvrProviderConfiguration> CreateDataDistributorConfiguration(
            this CvrClient cvrClient,
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentException($"'{nameof(certificatePassword)}' cannot be null or empty.", nameof(certificatePassword));

            var client = cvrClient.CreateClient();
            var options = cvrClient.GetOptions();

            using (var response = await client.CreateDataDistributorCvrConfigurationWithHttpMessagesAsync(
                subscriptionId: options.SubscriptionId,
                name: name,
                environment: environment.ToString("g"),
                certificate: certificate,
                certificatePassword: certificatePassword).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return response.Body;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        var responseMessage = await response.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new CvrConfigurationException(responseMessage);
                }
            }
        }

        /// <summary>
        /// Updates Data Distributor CVR configuration.
        /// </summary>
        /// <param name="cvrClient">CVR Client.</param>
        /// <param name="configurationId">ID of the configuration.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <returns>Created configuration.</returns>
        public static async Task<CvrProviderConfiguration> UpdateDataDistributorConfiguration(
            this CvrClient cvrClient,
            Guid configurationId,
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword)
        {
            if (configurationId == Guid.Empty) throw new ArgumentException($"'{nameof(configurationId)}' cannot be empty.", nameof(configurationId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentException($"'{nameof(certificatePassword)}' cannot be null or empty.", nameof(certificatePassword));

            var client = cvrClient.CreateClient();
            var options = cvrClient.GetOptions();

            using (var response = await client.UpdateDataDistributorCvrConfigurationWithHttpMessagesAsync(
                subscriptionId: options.SubscriptionId,
                configurationId: configurationId,
                name: name,
                environment: environment.ToString("g"),
                certificate: certificate,
                certificatePassword: certificatePassword).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return response.Body;
                    default:
                        var responseMessage = await response.Response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new CvrConfigurationException(responseMessage);
                }
            }
        }
    }
}