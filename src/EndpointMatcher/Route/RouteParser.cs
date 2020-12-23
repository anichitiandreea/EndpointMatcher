using EndpointMatcher.Route.Interfaces;
using System.Linq;

namespace EndpointMatcher.Route
{
    public class RouteParser : IRouteParser
    {
        public int GetRouteSegmentsNumber(string route)
        {
            return route.Count(character => character == '/');
        }

        public int GetRouteSpecificSegmentsNumber(string route)
        {
            return route.Count(character => character == ':');
        }
    }
}
