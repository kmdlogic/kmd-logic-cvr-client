using System;
using System.IO;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;

namespace Kmd.Logic.Cvr.Client
{
    public partial class CvrClient
    {
        /// <summary>
        /// Creates Data Distributor CVR configuration.
        /// </summary>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <returns>Created configuration.</returns>
        /// <exception cref="ArgumentNullException">No parameter can be empty or null.</exception>
        public async Task<CvrProviderConfiguration> CreateDataDistributorConfiguration(
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentNullException(nameof(certificatePassword));

            var client = this.CreateClient();
            using (var response = await client.CreateDataDistributorCvrConfigurationWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
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
        /// <param name="configurationId">ID of the configuration.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <returns>Created configuration.</returns>
        /// <exception cref="ArgumentNullException">No parameter can be empty or null.</exception>
        public async Task<CvrProviderConfiguration> UpdateDataDistributorConfiguration(
            Guid configurationId,
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword)
        {
            if (configurationId == Guid.Empty) throw new ArgumentNullException(nameof(configurationId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentNullException(nameof(certificatePassword));

            var client = this.CreateClient();
            using (var response = await client.UpdateDataDistributorCvrConfigurationWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
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