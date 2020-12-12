using System.Collections.Generic;

namespace RouteTemplate
{
    public class EndpointMatcher
    {
        public Dictionary<string, List<string>> routes = new Dictionary<string, List<string>>();

        public EndpointMatcher(Dictionary<string, List<string>> routes)
        {
            this.routes = routes;
        }

        public string Match(string route)
        {
            routes.TryGetValue(route.Split('/')[0], out List<string> value);

            if (value.Contains(route))
            {
                return route;
            }

            var subsetRouteMatcher = new SubsetRouteMatcher();
            string result = subsetRouteMatcher.GetSubsetMatchedRoute(route, value);

            return result;
        }
    }
}
