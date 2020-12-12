using System.Collections.Generic;

namespace EndpointMatcher
{
    public class EndpointMatcher : IEndpointMatcher
    {
        private readonly Dictionary<string, List<string>> routes = new Dictionary<string, List<string>>();

        public EndpointMatcher(Dictionary<string, List<string>> routes)
        {
            this.routes = routes;
        }

        public string Match(string route)
        {
            routes.TryGetValue(route.Split('/')[0], out List<string> routeSubsets);

            if (routeSubsets.Contains(route))
            {
                return route;
            }

            var subsetRouteMatcher = new SubsetRouteMatcher(routeSubsets);
            string matchedRoute = subsetRouteMatcher.GetSubsetMatchedRoute(route);

            return matchedRoute;
        }
    }
}
