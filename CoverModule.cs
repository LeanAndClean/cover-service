namespace CoverService
{
    using Nancy;

    public class CoverModule : NancyModule
    {
        public CoverModule(ICoverService coverService)
        {
            Get["/images/{mbid}"] = x => coverService.GetCoverUrlsAsync(x.mbid);
        }
    }
}
