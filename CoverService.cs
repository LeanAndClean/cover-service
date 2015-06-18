namespace CoverService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using Newtonsoft.Json.Linq;
    using ReCache;

    public class CoverService : ICoverService
    {
        private static readonly string defaultImageUrl = Environment.GetEnvironmentVariable("DEFAULT_IMAGE_URL")
                                                         ?? "http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png";

        private readonly Cache<string, string[]> cache;

        public CoverService()
        {
            var cacheOptions = new CacheOptions
            {
                CacheItemExpiry = TimeSpan.FromHours(12),
                FlushInterval = TimeSpan.FromMinutes(10),
                MaximumCacheSizeIndicator = 100000
            };

            cache = new Cache<string, string[]>(cacheOptions, FetchUrlsFromCoverArtArchive);
        }

        public string[] GetCoverUrlsAsync(string mbid)
        {
            return cache.GetOrLoad(mbid);
        }

        public string[] FetchUrlsFromCoverArtArchive(string mbid)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync("http://coverartarchive.org/release/" + mbid).Result;
                    response.EnsureSuccessStatusCode();

                    dynamic json = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    return ((IEnumerable<dynamic>) json.images).Select(x => (string) x.image).ToArray();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return new[] { defaultImageUrl  };
            }
        }
    }
}
