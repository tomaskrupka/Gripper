using System;
using System.Threading.Tasks;

namespace Gripper.Authenticated.Browser
{
    public interface IElement
    {
        public Task ClickAsync();
        public Task SendKeysAsync(string keys);
        public Task SendKeysAsync(string keys, TimeSpan delayAfterStroke);
        public Task SendSpecialKeyAsync(SpecialKey key);
    }
}
