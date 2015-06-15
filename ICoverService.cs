namespace CoverService
{
    using System.Threading.Tasks;

    public interface ICoverService
    {
        Task<string[]> GetCoverUrlsAsync(string mbid);
    }
}
