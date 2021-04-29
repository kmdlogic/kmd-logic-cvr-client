using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Kmd.Logic.Cvr.Client.Models;
using Kmd.Logic.Identity.Authorization;
using Microsoft.Rest;

namespace Kmd.Logic.Cvr.Client
{
    /// <summary>
    /// Get the details of a company from the CVR.
    /// </summary>
    /// <remarks>
    /// To access the CVR you:
    /// - Create a Logic subscription
    /// - Have a client credential issued for the Logic platform
    /// - Create a CVR configuration for the distribution service being used.
    /// </remarks>
    [SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "HttpClient is not owned by this class.")]
    public sealed class CvrClient
    {
        private readonly HttpClient _httpClient;
        private readonly CvrOptions _options;
        private readonly ITokenProviderFactory _tokenProviderFactory;

        private InternalClient _internalClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CvrClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client to use. The caller is expected to manage this resource and it will not be disposed.</param>
        /// <param name="tokenProviderFactory">The Logic access token provider factory.</param>
        /// <param name="options">The required configuration options.</param>
        public CvrClient(HttpClient httpClient, LogicTokenProviderFactory tokenProviderFactory, CvrOptions options)
        {
            this._httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this._options = options ?? throw new ArgumentNullException(nameof(options));
            this._tokenProviderFactory = tokenProviderFactory ?? throw new ArgumentNullException(nameof(tokenProviderFactory));
        }

        /// <summary>
        /// Get the details of a company from the CVR register.
        /// </summary>
        /// <param name="cvr">The CVR number.</param>
        /// <returns>The company details or null if the CVR number isn't valid.</returns>
        /// <exception cref="ValidationException">Missing cvr number.</exception>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        /// <exception cref="LogicTokenProviderException">Unable to issue an authorization token.</exception>
        /// <exception cref="CvrConfigurationException">Invalid CVR configuration details.</exception>
        public async Task<Company> GetCompanyByCvrAsync(string cvr)
        {
            var client = this.CreateClient();
            using (var response = await client.GetByCvrWithHttpMessagesAsync(
                                subscriptionId: this._options.SubscriptionId,
                                cvr: cvr,
                                configurationId: this._options.CvrConfigurationId).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return (Company)response.Body;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Get the details of a company from the CVR register.
        /// </summary>
        /// <param name="objectId">The Company's Object ID.</param>
        /// <returns>The company details or null if the object id isn't valid.</returns>
        /// <exception cref="ValidationException">Missing object id.</exception>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        /// <exception cref="LogicTokenProviderException">Unable to issue an authorization token.</exception>
        /// <exception cref="CvrConfigurationException">Invalid CVR configuration details.</exception>
        public async Task<Company> GetCompanyByIdAsync(string objectId)
        {
            var client = this.CreateClient();
            using (var response = await client.GetByIdWithHttpMessagesAsync(
                                subscriptionId: this._options.SubscriptionId,
                                id: objectId,
                                configurationId: this._options.CvrConfigurationId).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return (Company)response.Body;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Get the CVR configurations for the Logic subscription.
        /// </summary>
        /// <returns>The list of CVR configurations.</returns>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        /// <exception cref="LogicTokenProviderException">Unable to issue an authorization token.</exception>
        public async Task<IList<CvrProviderConfigurationModel>> GetAllCvrConfigurationsAsync()
        {
            var client = this.CreateClient();

            return await client.GetAllCvrConfigurationsAsync(subscriptionId: this._options.SubscriptionId).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the production units of a company from the CVR register.
        /// </summary>
        /// <param name="cvr">The Production unit number.</param>
        /// <returns>The list of production units of the company.</returns>
        /// <exception cref="ValidationException">Missing cvr number.</exception>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        /// <exception cref="LogicTokenProviderException">Unable to issue an authorization token.</exception>
        /// <exception cref="CvrConfigurationException">Invalid CVR configuration details.</exception>
        public async Task<IList<ProductionUnit>> GetProductionUnitsAsync(string cvr)
        {
            var client = this.CreateClient();
            using (var response = await client.GetProductionUnitsByCvrWithHttpMessagesAsync(
                                subscriptionId: this._options.SubscriptionId,
                                cvr: cvr,
                                configurationId: this._options.CvrConfigurationId).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return (IList<ProductionUnit>)response.Body;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Get the production unit detail from the CVR register.
        /// </summary>
        /// <param name="pNumber">The Production unit number.</param>
        /// <returns>The production unit detail.</returns>
        /// <exception cref="ValidationException">Missing pNumber.</exception>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        /// <exception cref="LogicTokenProviderException">Unable to issue an authorization token.</exception>
        /// <exception cref="CvrConfigurationException">Invalid CVR configuration details.</exception>
        public async Task<ProductionUnitDetail> GetProductionUnitDetailAsync(string pNumber)
        {
            var client = this.CreateClient();
            using (var response = await client.GetProductionUnitDetailByPNumberWithHttpMessagesAsync(
                                subscriptionId: this._options.SubscriptionId,
                                pNumber: pNumber,
                                configurationId: this._options.CvrConfigurationId).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return (ProductionUnitDetail)response.Body;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Get the production unit detail from the CVR register.
        /// </summary>
        /// <param name="objectId">The Production unit's Object ID.</param>
        /// <returns>The production unit detail.</returns>
        /// <exception cref="ValidationException">Missing object id.</exception>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        /// <exception cref="LogicTokenProviderException">Unable to issue an authorization token.</exception>
        /// <exception cref="CvrConfigurationException">Invalid CVR configuration details.</exception>
        public async Task<ProductionUnitDetail> GetProductionUnitDetailByIdAsync(string objectId)
        {
            var client = this.CreateClient();
            using (var response = await client.GetProductionUnitDetailByIdWithHttpMessagesAsync(
                                subscriptionId: this._options.SubscriptionId,
                                id: objectId,
                                configurationId: this._options.CvrConfigurationId).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return (ProductionUnitDetail)response.Body;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Subscribes for CVR events by Object ID.
        /// </summary>
        /// <param name="objectId">The Company's Object ID.</param>
        /// <returns>True in case of subscribe.</returns>
        /// <exception cref="ValidationException"> When subscriptionId or The CVR Object ID is null.</exception>
        /// <exception cref="SerializationException">Unable process the service response.</exception>
        public async Task<bool> SubscribeByIdAsync(string objectId)
        {
            var client = this.CreateClient();

            using (var response = await client.SubscribeByObjectIdWithHttpMessagesAsync(
                 subscriptionId: this._options.SubscriptionId,
                 objectId: objectId,
                 request: new CvrSubscriptionRequest(this._options.CvrConfigurationId)).ConfigureAwait(false))
            {
                return response.Response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Unsubscribes for CVR events by Object ID.
        /// </summary>
        /// <param name="objectId">The CVR Object ID.</param>
        /// <returns>True in case of unsubscribe.</returns>
        /// <exception cref="ValidationException"> When subscriptionId or object id is null.</exception>
        public async Task<bool> UnsubscribeByIdAsync(string objectId)
        {
            var client = this.CreateClient();

            using (var response = await client.UnsubscribeByObjectIdWithHttpMessagesAsync(
                   subscriptionId: this._options.SubscriptionId,
                   objectId: objectId,
                   configurationId: this._options.CvrConfigurationId).ConfigureAwait(false))
            {
                return response.Response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Gets company events for the nominated period.
        /// </summary>
        /// <param name="dateFrom">Query events from this date and time.</param>
        /// <param name="dateTo">Query events to this date and time.</param>
        /// <param name="pageNo">The page number to query, starting at 1.</param>
        /// <param name="pageSize">The maximum number of results to return.</param>
        /// <returns>List of citizen records.</returns>
        public async Task<IList<CompanyEvent>> GetAllCompanyEventsAsync(DateTime dateFrom, DateTime dateTo, int pageNo, int pageSize)
        {
            var client = this.CreateClient();

            using (var response = await client.GetEventsWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
                dateFrom: dateFrom,
                dateTo: dateTo,
                configurationId: this._options.CvrConfigurationId,
                pageNo: pageNo,
                pageSize: pageSize).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return response.Body as IList<CompanyEvent>;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Gets Subscribed company events for the nominated period.
        /// </summary>
        /// <param name="dateFrom">Query events from this date and time.</param>
        /// <param name="dateTo">Query events to this date and time.</param>
        /// <param name="pageNo">The page number to query, starting at 1.</param>
        /// <param name="pageSize">The maximum number of results to return.</param>
        /// <returns>Subscribed citizen records.</returns>
        public async Task<SubscribedCompanyEvents> GetSubscribedCompanyEventsAsync(DateTime dateFrom, DateTime dateTo, int pageNo, int pageSize)
        {
            var client = this.CreateClient();

            using (var response = await client.GetSubscribedEventsWithHttpMessagesAsync(
                subscriptionId: this._options.SubscriptionId,
                dateFrom: dateFrom,
                dateTo: dateTo,
                configurationId: this._options.CvrConfigurationId,
                pageNo: pageNo,
                pageSize: pageSize).ConfigureAwait(false))
            {
                switch (response.Response.StatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        return response.Body as SubscribedCompanyEvents;
                    case System.Net.HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new CvrConfigurationException(response.Body as string ?? "Invalid configuration provided to access CVR service");
                }
            }
        }

        /// <summary>
        /// Gets or Intialize the instance of the <see cref="InternalClient"/> class.
        /// </summary>
        /// <returns>The instance of KMDLogicCVRServiceServiceAPI.</returns>
        internal InternalClient CreateClient()
        {
            if (this._internalClient != null)
            {
                return this._internalClient;
            }

            var tokenProvider = this._tokenProviderFactory.GetProvider(this._httpClient);

            this._internalClient = new InternalClient(new TokenCredentials(tokenProvider))
            {
                BaseUri = this._options.CvrServiceUri,
            };

            return this._internalClient;
        }

        internal CvrOptions GetOptions()
        {
            return this._options;
        }
    }
}