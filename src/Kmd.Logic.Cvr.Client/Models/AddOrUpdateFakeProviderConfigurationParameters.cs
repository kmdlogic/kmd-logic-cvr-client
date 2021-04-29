using System;

namespace Kmd.Logic.Cvr.Client.Models
{
    public class AddOrUpdateFakeProviderConfigurationParameters
    {
        public AddOrUpdateFakeProviderConfigurationParameters(Guid? configurationId, string name)
        {
            this.ConfigurationId = configurationId;
            this.Name = name;
        }

        public Guid? ConfigurationId { get; }

        public string Name { get; }
    }
}