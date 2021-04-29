using System;
using System.IO;

namespace Kmd.Logic.Cvr.Client.Models
{
    public class AddOrUpdateDataFordelerConfigurationParameters
    {
        public AddOrUpdateDataFordelerConfigurationParameters(
            Guid? configurationId,
            string name,
            CvrEnviroment environment,
            Stream certificate,
            string certificatePassword)
        {
            this.ConfigurationId = configurationId;
            this.Name = name;
            this.Environment = environment;
            this.Certificate = certificate;
            this.CertificatePassword = certificatePassword;
        }

        public Guid? ConfigurationId { get; }

        public string Name { get; }

        public CvrEnviroment Environment { get; }

        public Stream Certificate { get; }

        public string CertificatePassword { get; }
    }
}