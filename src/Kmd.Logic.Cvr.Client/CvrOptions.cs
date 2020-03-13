using System;

namespace Kmd.Logic.Cvr.Client
{
    /// <summary>
    /// Provide the configuration options for using the CVR service.
    /// </summary>
    public sealed class CvrOptions
    {
        /// <summary>
        /// Gets or sets the Logic CVR service.
        /// </summary>
        /// <remarks>
        /// This option should not be overridden except for testing purposes.
        /// </remarks>
        public Uri CvrServiceUri { get; set; } = new Uri("https://gateway.kmdlogic.io/cvr/v1");

        /// <summary>
        /// Gets or sets the Logic Subscription.
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets the Logic CVR Configuration identifier.
        /// </summary>
        public Guid CvrConfigurationId { get; set; }
    }
}