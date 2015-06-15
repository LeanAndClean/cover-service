namespace CoverService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class CoverService : ICoverService
    {
        private const string defaultImageUrl = "http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png";

        private readonly Dictionary<string, string[]> cache = new Dictionary<string, string[]>();

        public async Task<string[]> GetCoverUrlsAsync(string mbid)
        {
            try
            {
                string[] result;
                if (!cache.TryGetValue(mbid, out result))
                {
                    result = await FetchUrlsFromCoverArtArchive(mbid);
                    if (result.Length == 0) result = new[] { defaultImageUrl  };
                    cache[mbid] = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return new[] { defaultImageUrl  };
            }
        }

        public async Task<string[]> FetchUrlsFromCoverArtArchive(string mbid)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync("http://coverartarchive.org/release/" + mbid);
                response.EnsureSuccessStatusCode();

                dynamic json = JObject.Parse(await response.Content.ReadAsStringAsync());
                return ((IEnumerable<dynamic>) json.images).Select(x => (string) x.image).ToArray();
            }
        }
    }
}
