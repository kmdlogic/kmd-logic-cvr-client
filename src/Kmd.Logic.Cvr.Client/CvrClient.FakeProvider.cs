using System;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;

namespace Kmd.Logic.Cvr.Client
{
    public partial class CvrClient
    {
        /// <summary>
        /// Creates Fake provider CVR configuration.
        /// </summary>
        /// <param name="name">Name of the configuration.</param>
        /// <returns>Created configuration.</returns>
        /// <exception cref="ArgumentNullException">No parameter can be empty or null.</exception>
        public async Task<CvrFakeProviderConfiguration> CreateFakeProviderConfiguration(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            var client = this.CreateClient();
            using (var response = await client.CreateFakeProviderConfigurationWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
                name: name).ConfigureAwait(false))
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
        /// Updates Fake provider CVR configuration.
        /// </summary>
        /// <param name="configurationId">ID of the configuration.</param>
        /// <param name="name">Name of the configuration.</param>
        /// <returns>Created configuration.</returns>
        /// <exception cref="ArgumentNullException">No parameter can be empty or null.</exception>
        public async Task<CvrFakeProviderConfiguration> UpdateFakeProviderConfiguration(Guid configurationId, string name)
        {
            if (configurationId == Guid.Empty) throw new ArgumentNullException(nameof(configurationId));
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            var client = this.CreateClient();
            using (var response = await client.UpdateFakeProviderConfigurationWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
                configurationId: configurationId,
                name: name).ConfigureAwait(false))
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