using Kmd.Logic.Identity.Authorization;

namespace Kmd.Logic.Cvr.Client.Sample
{
    internal class AppConfiguration
    {
        public string CvrNumber { get; set; } = "33866941";

        public LogicTokenProviderOptions TokenProvider { get; set; } = new LogicTokenProviderOptions();

        public CvrOptions Cvr { get; set; } = new CvrOptions();
    }
}