using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gripper.Test
{
    public static class Fakers
    {
        public static Models.Cookie GetCookie() =>
            new Faker<Models.Cookie>()
            .RuleFor(c => c.Name, f => f.Random.AlphaNumeric(f.Random.Int(min: 1, 2048)))
            .RuleFor(c => c.Value, f => f.Random.AlphaNumeric(f.Random.Int(min: 1, 2048)))
            .RuleFor(c => c.Domain, f => f.Internet.Url());

        public static object GetCookieObject()
        {
            var fakeCookie = GetCookie();

            var cookieObject = new
            {
                name = fakeCookie.Name,
                value = fakeCookie.Value,
                url = fakeCookie.Domain
            };

            return cookieObject;
        }
    }
}
