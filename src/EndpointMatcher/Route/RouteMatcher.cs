using EndpointMatcher.Route.Interfaces;
using EndpointMatcher.Segment;
using System.Collections.Generic;
using System.Linq;

namespace EndpointMatcher.Route
{
    public class RouteMatcher : IRouteMatcher
    {
        private List<string> routePatterns;
        private readonly string route;
        private readonly int routeSegmentsNumber;
        private readonly RouteParser routeParser = new RouteParser();

        public RouteMatcher(List<string> routePatterns, string route)
        {
            this.routePatterns = routePatterns;
            this.route = route;
            routeSegmentsNumber = routeParser.GetRouteSegmentsNumber(route);
        }

        public string GetMatchedRoute()
        {
            string[] routeSegments = route.Split('/');
            int indexOfSegment = 1;

            while (indexOfSegment < routeSegments.Length)
            {
                string routeSegment = routeSegments[indexOfSegment];

                RemovePatternBasedOnCurrentSegment(indexOfSegment, routeSegment);

                indexOfSegment ++;
            }

            return GetSpecificRoute();
        }

        public string GetSpecificRoute()
        {
            var numberOfSpecificSegments = 0;
            var mostSpecificRoute = string.Empty;

            if (routePatterns.Count == 1)
            {
                return routePatterns.FirstOrDefault();
            }

            foreach (var route in routePatterns)
            {
                var routeSpecificSegmentsNumber = routeParser.GetRouteSpecificSegmentsNumber(route);
                if (routeSpecificSegmentsNumber > numberOfSpecificSegments)
                {
                    numberOfSpecificSegments = routeSpecificSegmentsNumber;
                    mostSpecificRoute = route;
                }
            }

            return mostSpecificRoute;
        }

        public void RemovePatternBasedOnCurrentSegment(int indexOfSegment, string routeSegment)
        {
            var segmentTypeParser = new SegmentTypeParser(routeSegment);
            var routeSegmentType = segmentTypeParser.GetSegmentType();

            var eligibleRoutePatterns = routePatterns
                .Where(pattern =>
                {
                    var patternSegmentsNumber = routeParser.GetRouteSegmentsNumber(pattern);
                    if (patternSegmentsNumber != routeSegmentsNumber)
                    {
                        return false;
                    }
                    else
                    {
                        var patternSegment = pattern.Split('/')[indexOfSegment];
                        var segmentParser = new SegmentParser(patternSegment);

                        if (!segmentParser.IsVariable()
                            && patternSegment != routeSegment)
                        {
                            return false;
                        }

                        if (!segmentParser.Match(routeSegmentType))
                        {
                            return false;
                        }
                    }

                    return true;
                })
                .ToList();

            routePatterns = eligibleRoutePatterns;
        }
    }
}
