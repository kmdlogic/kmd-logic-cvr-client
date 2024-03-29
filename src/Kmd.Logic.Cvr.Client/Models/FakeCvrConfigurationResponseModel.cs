// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Kmd.Logic.Cvr.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class FakeCvrConfigurationResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the FakeCvrConfigurationResponseModel
        /// class.
        /// </summary>
        public FakeCvrConfigurationResponseModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the FakeCvrConfigurationResponseModel
        /// class.
        /// </summary>
        public FakeCvrConfigurationResponseModel(System.Guid? id = default(System.Guid?), System.Guid? subscriptionId = default(System.Guid?), string name = default(string), string companyDataFileName = default(string), string productionUnitDataFileName = default(string), string companyEventsDataFileName = default(string), string environment = default(string))
        {
            Id = id;
            SubscriptionId = subscriptionId;
            Name = name;
            CompanyDataFileName = companyDataFileName;
            ProductionUnitDataFileName = productionUnitDataFileName;
            CompanyEventsDataFileName = companyEventsDataFileName;
            Environment = environment;
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
        /// </summary>
        [JsonProperty(PropertyName = "companyDataFileName")]
        public string CompanyDataFileName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productionUnitDataFileName")]
        public string ProductionUnitDataFileName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "companyEventsDataFileName")]
        public string CompanyEventsDataFileName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "environment")]
        public string Environment { get; set; }

    }
}
