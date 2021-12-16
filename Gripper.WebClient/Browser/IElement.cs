using System;
using System.Threading.Tasks;

namespace Gripper.WebClient.Browser
{
    public interface IElement
    {
        public Task ClickAsync();
        public Task FocusAsync();
        public Task<string> GetInnerTextAsync();
        public Task SendKeysAsync(string keys, TimeSpan delayBetweenStrokes);
        public Task SendSpecialKeyAsync(SpecialKey key);
        public Task WaitUntilInteractable();
    }
}
