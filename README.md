# KMD Logic CVR Client

A dotnet client library for accessing the Danish CVR register via the Logic platform.

## How to use this client library

In projects or components where you need to access the register, add a NuGet package reference to [Kmd.Logic.Cvr.Client](https://www.nuget.org/packages/Kmd.Logic.Cvr.Client/).

The simplest example to get a company details is:

```csharp
using (var httpClient = new HttpClient())
using (var tokenProviderFactory = new LogicTokenProviderFactory(configuration.TokenProvider))
{
    var cvrClient = new CvrClient(httpClient, tokenProviderFactory, configuration.Cvr);
    var company = await cvrClient.GetCompanyByCvrAsync(configuration.CvrNumber).ConfigureAwait(false);
}
```

The `LogicTokenProviderFactory` authorizes access to the Logic platform through the use of a Logic Identity issued client credential. The authorization token is reused until it  expires. You would generally create a single instance of `LogicTokenProviderFactory`.

The `CvrClient` accesses the Logic CVR service which in turn interacts with one of the data providers.

## How to configure the CVR client

Perhaps the easiest way to configure the CVR client is from Application Settings.

```json
{
  "TokenProvider": {
    "ClientId": "",
    "ClientSecret": "",
    "AuthorizationScope": ""
  },
  "Cvr": {
    "SubscriptionId": "",
    "CvrConfigurationId": ""
  }
}
```

To get started:

1. Create a subscription in [Logic Console](https://console.kmdlogic.io). This will provide you the `SubscriptionId`.
2. Request a client credential. Once issued you can view the `ClientId`, `ClientSecret` and `AuthorizationScope` in [Logic Console](https://console.kmdlogic.io).
3. Create a CVR configuration. Select the CVR provider you have an agreement with and upload the access certificate. If you haven't done this already, you can begin testing with the Fake CVR Provider. This will give you the `CvrConfigurationId`.

## Sample application

A simple console application is included to demonstrate how to call Logic CVR API. You will need to provide the settings described above in `appsettings.json`.

When run you should see the details of the _Company_ for the nominated CVR number is printed to the console.

## Datafordeler Provider

The Datafordeler service is available to any organisation which require access to the CVR register.

To gain access, you must:

1. Create a user in the Self Service Portal
2. Add a Service User by supplying a FOCES certificate
3. Request access to the CVR Service `HentCVRData` 

Useful links:

1. [Datafordeler Website](https://datafordeler.dk)
2. [Self Service Portal (Production)](https://selfservice.datafordeler.dk)
3. [Self Service Portal (Test)](https://test03-selfservice.datafordeler.dk/)
4. [Requesting Access](https://datafordeler.dk/vejledning/brugeradgang/anmodning-om-adgang/det-centrale-virksomhedsregister-cvr/)
5. [CVR Service Details](https://datafordeler.dk/dataoversigt/det-centrale-virksomhedsregister-cvr/hentcvrdata/)

## Service Platform Provider

The Service Platform provider is for exclusive use by municipalities.

For CVR purposes Logic connects to [CVROnline](https://www.serviceplatformen.dk/administration/serviceOverview/show?uuid=c0daecde-e278-43b7-84fd-477bfeeea027) service.

To gain access, a user with a FOCES/VOCES certificate must send the request for a Service Agreement in the STS Administration portal for the required environment (Test or Production).

The process of Service Agreement approval can be sometimes accelerated up by sending e-mail to the Service Platform Help-desk, including service agreement UUID. When service agreement is approved, the client must create the configuration at Logic Console.

Logic CVR configuration parameters for Service Platform:

- Name: your customer name in Service Platform which identifies specific configuration within all resources
- Certificate: The `p12` certificate which has been uploaded during configuration of IT-Service at STS Administration portal
- Municipality CVR: The CVR of the municipality that you will be requesting access to Service Platform on behalf of

Useful links:

1. [Service Platform (Production)](https://www.serviceplatformen.dk)
2. [Service Platform (Test)](https://exttestwww.serviceplatformen.dk)
3. [STS Administration Portal](https://www.serviceplatformen.dk/administration/dashboard/outerpage?page=sts)
4. [General technical documentation](https://www.serviceplatformen.dk/administration/help/faq)
5. [More specific documentation (CvrOnline)](https://exttestwww.serviceplatformen.dk/administration/serviceOverview/show?uuid=c0daecde-e278-43b7-84fd-477bfeeea027)
6. [Service Platform Help-desk](mailto:helpdesk@serviceplatformen.dk)

## CVR Fake Provider

The Fake Provider is a great solution for use in Demo or Test environments and also allows you to begin development immediately whilst you wait for your formal credentials.

The Fake Provider will return well-described test data for a CVR number.These can be viewed in the fake folder of this repository. This includes the CVR test data set.

If not one of the well-described tests, the Fake Provider exhibits the following behaviour:

- The CVR number must be 8 digits long, with the first digit greater than 0 (D1 > 0).
- The Fake provider returns random data, using the CVR number as the seed. This ensures recurring calls return the same response

NOTE: While every attempt is made to keep the generated random data consistent, this is **not guaranteed**. If you need a reliable response, please use a well-known test or request for a suitable one to be added.