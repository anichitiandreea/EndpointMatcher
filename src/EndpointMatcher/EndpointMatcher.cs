using EndpointMatcher.Route;
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
            var firstSegment = route.Split('/', 2)[0];
            routes.TryGetValue(firstSegment, out List<string> routePatterns);

            if (routePatterns.Contains(route))
            {
                return route;
            }

            var routeMatcher = new RouteMatcher(routePatterns, route);
            string matchedRoute = routeMatcher.GetMatchedRoute();

            return matchedRoute;
        }
    }
}
