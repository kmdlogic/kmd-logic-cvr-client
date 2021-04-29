using System;
using System.IO;

namespace Kmd.Logic.Cvr.Client.Models
{
    public class AddOrUpdateServicePlatformConfigurationParameters
    {
        public AddOrUpdateServicePlatformConfigurationParameters(
            Guid? configurationId,
            string name,
            CvrEnviroment environment,
            Stream certificate,
            string certificatePassword,
            Guid serviceAgreementUuid,
            Guid userSystemUuid,
            Guid userUuid)
        {
            this.ConfigurationId = configurationId;
            this.Name = name;
            this.Environment = environment;
            this.Certificate = certificate;
            this.CertificatePassword = certificatePassword;
            this.ServiceAgreementUuid = serviceAgreementUuid;
            this.UserSystemUuid = userSystemUuid;
            this.UserUuid = userUuid;
        }

        public Guid? ConfigurationId { get; }

        public string Name { get; }

        public CvrEnviroment Environment { get; }

        public Stream Certificate { get; set; }

        public string CertificatePassword { get; set; }

        public Guid ServiceAgreementUuid { get; set; }

        public Guid UserSystemUuid { get; set; }

        public Guid UserUuid { get; set; }
    }
}