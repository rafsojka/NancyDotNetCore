using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApplication
{
    // https://carlos.mendible.com/2017/01/16/net-core-and-nancyfx-can-writing-a-webapi-get-any-simpler/
    // public class BaconIpsumMessage
    // {
    //     public string Body {get; set;}
    // }

    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAsync();
        Task<int> PostAsync(Movie movie);
    }

    public class MoviesService : IMoviesService
    {
        public async Task<IEnumerable<Movie>> GetAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> PostAsync(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
