using Nancy;

namespace WebApplication
{
    // https://carlos.mendible.com/2017/01/16/net-core-and-nancyfx-can-writing-a-webapi-get-any-simpler/
    public class BaconIpsumModule : NancyModule
    {
        public BaconIpsumModule(IBaconIpsumService baconIpsumService)
        {
            Get("/", args => "Super Duper Happy Path running on .NET Core");

            Get("/baconipsum", async args => await baconIpsumService.GenerateAsync());
        }
    }
}
