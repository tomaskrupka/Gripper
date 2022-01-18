using Bogus;
using Gripper.Test.Models;

namespace Gripper.Test
{
    public static class Fakers
    {
        public static Cookie GetCookie() =>
            new Faker<Cookie>()
            .RuleFor(c => c.name, f => f.Random.AlphaNumeric(f.Random.Int(min: 1, 2048)))
            .RuleFor(c => c.value, f => f.Random.AlphaNumeric(f.Random.Int(min: 1, 2048)))
            .RuleFor(c => c.domain, f => f.Internet.Url());

        public static object GetCookieObject()
        {
            var fakeCookie = GetCookie();

            var cookieObject = new
            {
                name = fakeCookie.name,
                value = fakeCookie.value,
                domain = fakeCookie.domain
            };

            return cookieObject;
        }
    }
}
