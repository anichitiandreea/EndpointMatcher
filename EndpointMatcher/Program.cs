using System;
using System.Collections.Generic;

namespace RouteTemplate
{
    public class Program
    {
        static void Main()
        {
            Dictionary<string, List<string>> routes = new Dictionary<string, List<string>>
            {
                { "users", new List<string>() { "users/Ion/{name}", "users/Ion/ionut", "users/{id:number}" } },
                { "categories", new List<string>() { "categories/{id}/apps/{appId}" } },
                { "pages", new List<string>() { "pages/{id:number}/myName" } }
            };

            var endpointMatcher = new EndpointMatcher(routes);

            Console.WriteLine(endpointMatcher.Match("users/Ion/33"));
            Console.ReadKey();
        }
    }
}
