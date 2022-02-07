using Bogus;
using Gripper.Test.Models;

namespace Gripper.Test
{
    internal static class Fakers
    {
        internal static Cookie GetCookie() =>
            new Faker<Cookie>()
            .RuleFor(c => c.name, f => f.Random.AlphaNumeric(f.Random.Int(min: 1, max: 2048)))
            .RuleFor(c => c.value, f => f.Random.AlphaNumeric(f.Random.Int(min: 1, max: 2048)))
            .RuleFor(c => c.domain, f => f.Internet.Url());

        internal static Url GetUrl() => new Faker<Url>()
            .RuleFor(u => u.url, f => f.Internet.Url());

    }
}
