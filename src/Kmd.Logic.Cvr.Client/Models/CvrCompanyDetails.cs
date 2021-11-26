// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Kmd.Logic.Cvr.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class CvrCompanyDetails
    {
        /// <summary>
        /// Initializes a new instance of the CvrCompanyDetails class.
        /// </summary>
        public CvrCompanyDetails()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the CvrCompanyDetails class.
        /// </summary>
        public CvrCompanyDetails(string businessStartDate = default(string), string cessationDate = default(string), string registrationFrom = default(string), string registrationTo = default(string), string status = default(string))
        {
            BusinessStartDate = businessStartDate;
            CessationDate = cessationDate;
            RegistrationFrom = registrationFrom;
            RegistrationTo = registrationTo;
            Status = status;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "businessStartDate")]
        public string BusinessStartDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "cessationDate")]
        public string CessationDate { get; set; }
        
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "registrationFrom")]
        public string RegistrationFrom { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "registrationTo")]
        public string RegistrationTo { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}
