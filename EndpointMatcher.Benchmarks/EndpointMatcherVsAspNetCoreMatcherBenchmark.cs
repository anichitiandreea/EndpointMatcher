using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace EndpointMatcher.Benchmarks
{
    public class EndpointMatcherVsAspNetCoreMatcherBenchmark
    {
        private readonly Dictionary<string, List<string>> data;
        private readonly EndpointMatcher endpointMatcher;

        public EndpointMatcherVsAspNetCoreMatcherBenchmark()
        {
            data = new Dictionary<string, List<string>>
            {
                { "users", new List<string>() { "users/{name}", "users/{id}/applications", "users/{id:number}" } },
                { "categories", new List<string>() { "categories/{id}/apps/{appId}" } },
                { "pages", new List<string>() { "pages/{id:number}/sections/myPages", "pages/{id}/sections" } }
            };

            endpointMatcher = new EndpointMatcher(data);
        }

        [Benchmark]
        public string EndpointMatcher() => endpointMatcher.Match("users/33454");

        [Benchmark]
        public RouteValueDictionary RouteMatch() => RouteMatcher.Match("/users/{id:int}", "/users/1");
    }
}
