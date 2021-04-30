using System;
using System.IO;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;

namespace Kmd.Logic.Cvr.Client
{
    public partial class CvrClient
    {
        /// <summary>
        /// Creates Service Platform CVR configuration.
        /// </summary>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <param name="serviceAgreementUuid">Service Agreement UUID between calling system and Service Platform.</param>
        /// <param name="userSystemUuid">User System UUID of the calling system.</param>
        /// <param name="userUuid">User UUID of the municipality.</param>
        /// <returns>Created configuration.</returns>
        /// <exception cref="ArgumentNullException">No parameter can be empty or null.</exception>
        public async Task<ServicePlatformCvrProviderConfiguration> CreateServicePlatformConfiguration(
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword,
            Guid serviceAgreementUuid,
            Guid userSystemUuid,
            Guid userUuid)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (serviceAgreementUuid == Guid.Empty) throw new ArgumentNullException(nameof(serviceAgreementUuid));
            if (userSystemUuid == Guid.Empty) throw new ArgumentNullException(nameof(userSystemUuid));
            if (userUuid == Guid.Empty) throw new ArgumentNullException(nameof(userUuid));

            var client = this.CreateClient();
            using (var response = await client.CreateServicePlatformConfigurationWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
                name: name,
                environment: environment.ToString("g"),
                certificate: certificate,
                certificatePassword: certificatePassword,
                serviceAgreementUuid: serviceAgreementUuid.ToString(),
                userSystemUuid: userSystemUuid.ToString(),
                userUuid: userUuid.ToString()).ConfigureAwait(false))
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
        /// Updates Service Platform CVR configuration.
        /// </summary>
        /// <param name="configurationId">ID of the configuration.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <param name="serviceAgreementUuid">Service Agreement UUID between calling system and Service Platform.</param>
        /// <param name="userSystemUuid">User System UUID of the calling system.</param>
        /// <param name="userUuid">User UUID of the municipality.</param>
        /// <returns>Created configuration.</returns>
        /// <exception cref="ArgumentNullException">No parameter can be empty or null.</exception>
        public async Task<ServicePlatformCvrProviderConfiguration> UpdateServicePlatformConfiguration(
            Guid configurationId,
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword,
            Guid serviceAgreementUuid,
            Guid userSystemUuid,
            Guid userUuid)
        {
            if (configurationId == Guid.Empty) throw new ArgumentNullException(nameof(configurationId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentNullException(nameof(certificatePassword));
            if (serviceAgreementUuid == Guid.Empty) throw new ArgumentNullException(nameof(serviceAgreementUuid));
            if (userSystemUuid == Guid.Empty) throw new ArgumentNullException(nameof(userSystemUuid));
            if (userUuid == Guid.Empty) throw new ArgumentNullException(nameof(userUuid));

            var client = this.CreateClient();
            using (var response = await client.UpdateServicePlatformConfigurationWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
                configurationId: configurationId,
                name: name,
                environment: environment.ToString("g"),
                certificate: certificate,
                certificatePassword: certificatePassword,
                serviceAgreementUuid: serviceAgreementUuid.ToString(),
                userSystemUuid: userSystemUuid.ToString(),
                userUuid: userUuid.ToString()).ConfigureAwait(false))
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