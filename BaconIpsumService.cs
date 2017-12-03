using System.Net.Http;
using System.Threading.Tasks;

namespace WebApplication
{
    // https://carlos.mendible.com/2017/01/16/net-core-and-nancyfx-can-writing-a-webapi-get-any-simpler/
    public class BaconIpsumMessage
    {
        public string Body {get; set;}
    }

    public interface IBaconIpsumService
    {
        Task<BaconIpsumMessage> GenerateAsync();
    }

    public class BaconIpsumService : IBaconIpsumService
    {
        public async Task<BaconIpsumMessage> GenerateAsync()
        {
            using (var client = new HttpClient())
            {
                var message = new BaconIpsumMessage();
                // Post the message
                message.Body =  await client.GetStringAsync(
                    $"https://baconipsum.com/api/?type=meat-and-filler");

                return message;
            }
        }
    }
}
