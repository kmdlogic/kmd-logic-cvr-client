// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Kmd.Logic.Cvr.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class ServicePlatformCvrProviderConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ServicePlatformCvrProviderConfiguration class.
        /// </summary>
        public ServicePlatformCvrProviderConfiguration()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// ServicePlatformCvrProviderConfiguration class.
        /// </summary>
        /// <param name="environment">Possible values include: 'Production',
        /// 'Test'</param>
        public ServicePlatformCvrProviderConfiguration(System.Guid? id = default(System.Guid?), System.Guid? subscriptionId = default(System.Guid?), string name = default(string), string environment = default(string), string certificateFileName = default(string), string municipalityCvr = default(string), System.Guid? serviceAgreementUuid = default(System.Guid?), System.Guid? userSystemUuid = default(System.Guid?), System.Guid? userUuid = default(System.Guid?))
        {
            Id = id;
            SubscriptionId = subscriptionId;
            Name = name;
            Environment = environment;
            CertificateFileName = certificateFileName;
            MunicipalityCvr = municipalityCvr;
            ServiceAgreementUuid = serviceAgreementUuid;
            UserSystemUuid = userSystemUuid;
            UserUuid = userUuid;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public System.Guid? Id { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "subscriptionId")]
        public System.Guid? SubscriptionId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Production', 'Test'
        /// </summary>
        [JsonProperty(PropertyName = "environment")]
        public string Environment { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "certificateFileName")]
        public string CertificateFileName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "municipalityCvr")]
        public string MunicipalityCvr { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "serviceAgreementUuid")]
        public System.Guid? ServiceAgreementUuid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "userSystemUuid")]
        public System.Guid? UserSystemUuid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "userUuid")]
        public System.Guid? UserUuid { get; set; }

    }
}
