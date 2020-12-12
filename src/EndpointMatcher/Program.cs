using System;
using System.Collections.Generic;

namespace EndpointMatcher
{
    public class Program
    {
        static void Main()
        {
            Dictionary<string, List<string>> routes = new Dictionary<string, List<string>>
            {
                { "users", new List<string>() { "users/{name}", "users/{id}/applications", "users/{id:number}" } },
                { "categories", new List<string>() { "categories/{id}/apps/{appId}" } },
                { "pages", new List<string>() { "pages/{id:number}/sections" } }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            Console.WriteLine(endpointMatcher.Match("users/33"));
            Console.ReadKey();
        }
    }
}
