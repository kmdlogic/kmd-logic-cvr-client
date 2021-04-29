using System;
using System.IO;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;

namespace Kmd.Logic.Cvr.Client
{
    public static class ServicePlatformExtensions
    {
        /// <summary>
        /// Creates Service Platform CVR configuration.
        /// </summary>
        /// <param name="cvrClient">CVR Client.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <param name="serviceAgreementUuid">Service Agreement UUID between calling system and Service Platform.</param>
        /// <param name="userSystemUuid">User System UUID of the calling system.</param>
        /// <param name="userUuid">User UUID of the municipality.</param>
        /// <returns>Created configuration.</returns>
        public static async Task<ServicePlatformCvrProviderConfiguration> CreateServicePlatformConfiguration(
            this CvrClient cvrClient,
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword,
            Guid serviceAgreementUuid,
            Guid userSystemUuid,
            Guid userUuid)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentException($"'{nameof(certificatePassword)}' cannot be null or empty.", nameof(certificatePassword));
            if (serviceAgreementUuid == Guid.Empty) throw new ArgumentException($"'{nameof(serviceAgreementUuid)}' cannot be empty.", nameof(serviceAgreementUuid));
            if (userSystemUuid == Guid.Empty) throw new ArgumentException($"'{nameof(userSystemUuid)}' cannot be empty.", nameof(userSystemUuid));
            if (userUuid == Guid.Empty) throw new ArgumentException($"'{nameof(userUuid)}' cannot be empty.", nameof(userUuid));

            var client = cvrClient.CreateClient();
            var options = cvrClient.GetOptions();

            using (var response = await client.CreateServicePlatformConfigurationWithHttpMessagesAsync(
                subscriptionId: options.SubscriptionId,
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
        /// <param name="cvrClient">CVR Client.</param>
        /// <param name="configurationId">ID of the configuration.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <param name="environment">Environment to use.</param>
        /// <param name="certificate">Stream with certificate.</param>
        /// <param name="certificatePassword">Password to the certificate.</param>
        /// <param name="serviceAgreementUuid">Service Agreement UUID between calling system and Service Platform.</param>
        /// <param name="userSystemUuid">User System UUID of the calling system.</param>
        /// <param name="userUuid">User UUID of the municipality.</param>
        /// <returns>Created configuration.</returns>
        public static async Task<ServicePlatformCvrProviderConfiguration> UpdateServicePlatformConfiguration(
            this CvrClient cvrClient,
            Guid configurationId,
            string name,
            ProviderEnviroment environment,
            Stream certificate,
            string certificatePassword,
            Guid serviceAgreementUuid,
            Guid userSystemUuid,
            Guid userUuid)
        {
            if (configurationId == Guid.Empty) throw new ArgumentException($"'{nameof(configurationId)}' cannot be empty.", nameof(configurationId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            if (certificate is null) throw new ArgumentNullException(nameof(certificate));
            if (string.IsNullOrEmpty(certificatePassword)) throw new ArgumentException($"'{nameof(certificatePassword)}' cannot be null or empty.", nameof(certificatePassword));
            if (serviceAgreementUuid == Guid.Empty) throw new ArgumentException($"'{nameof(serviceAgreementUuid)}' cannot be empty.", nameof(serviceAgreementUuid));
            if (userSystemUuid == Guid.Empty) throw new ArgumentException($"'{nameof(userSystemUuid)}' cannot be empty.", nameof(userSystemUuid));
            if (userUuid == Guid.Empty) throw new ArgumentException($"'{nameof(userUuid)}' cannot be empty.", nameof(userUuid));

            var client = cvrClient.CreateClient();
            var options = cvrClient.GetOptions();

            using (var response = await client.UpdateServicePlatformConfigurationWithHttpMessagesAsync(
                subscriptionId: options.SubscriptionId,
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