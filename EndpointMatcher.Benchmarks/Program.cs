using BenchmarkDotNet.Running;

namespace EndpointMatcher.Benchmarks
{
    class Program
    {
        static void Main()
        {
            _ = RouteMatcher.Match("/users/{id:int}", "/users/1");
            _ = BenchmarkRunner.Run<EndpointMatcherVsAspNetCoreMatcherBenchmark>();
        }
    }
}
