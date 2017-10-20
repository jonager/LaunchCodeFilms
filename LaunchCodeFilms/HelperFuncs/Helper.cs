using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using unirest_net.http;
using Newtonsoft.Json;

namespace LaunchCodeFilms.HelperFuncs
{
    public class Helper
    {
        public static object GetPopularMovies()
        {
            HttpResponse<string> request = Unirest.get("https://api.themoviedb.org/3/movie/popular?api_key=?&page=1")
               .header("accept", "application/json")
               .header("Content-Type", "application/json")
               .header("Accept-Encoding:", "gzip, deflate, compress")
               //.header("User-Agent", "Spotify API Console v0.1")
               .asJson<string>();

            object popularMovies = JsonConvert.DeserializeObject<object>(request.Body);

            return popularMovies;
        }
    }
}
