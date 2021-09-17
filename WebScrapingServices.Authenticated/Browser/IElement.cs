using System.Threading.Tasks;

namespace WebScrapingServices.Authenticated.Browser
{
    public interface IElement
    {
        public Task ClickAsync();
        public Task SendKeysAsync(string keys);
        public Task SendKeysAsync(string keys, int delayEachMs);
        public Task SendSpecialKeyAsync(SpecialKey key);
    }
}
