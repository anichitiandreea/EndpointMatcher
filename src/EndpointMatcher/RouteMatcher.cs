using EndpointMatcher.Segment;
using System.Collections.Generic;
using System.Linq;

namespace EndpointMatcher
{
    public class RouteMatcher : IRouteMatcher
    {
        private readonly List<string> routePatterns;
        private readonly string route;
        private readonly int routeSegmentsNumber;

        public RouteMatcher(List<string> routePatterns, string route)
        {
            this.routePatterns = routePatterns;
            this.route = route;
            routeSegmentsNumber = GetRouteSegmentsNumber(route);
        }

        public int GetRouteSegmentsNumber(string route)
        {
            return route.Count(character => character == '/');
        }

        public string GetMatchedRoute()
        {
            string[] routeSegments = route.Split('/');
            int indexOfSegment = 1;

            while (indexOfSegment < routeSegments.Length)
            {
                RemovePatternBasedOnCurrentSegment(indexOfSegment, routeSegments);

                indexOfSegment ++;
            }

            return routePatterns.FirstOrDefault();
        }

        public void RemovePatternBasedOnCurrentSegment(int indexOfSegment, string[] routeSegments)
        {
            var segmentTypeParser = new SegmentTypeParser(routeSegments[indexOfSegment]);
            var routeSegmentType = segmentTypeParser.GetSegmentType();

            for (int currentPatternIndex = 0; currentPatternIndex < routePatterns.Count; currentPatternIndex++)
            {
                if (GetRouteSegmentsNumber(routePatterns[currentPatternIndex]) != routeSegmentsNumber)
                {
                    routePatterns.RemoveAt(currentPatternIndex);
                }
                else
                {
                    var patternSegment = routePatterns[currentPatternIndex].Split('/')[indexOfSegment];
                    var segmentParser = new SegmentParser(patternSegment);

                    if (!segmentParser.IsVariable() && patternSegment != routeSegments[indexOfSegment])
                    {
                        routePatterns.RemoveAt(currentPatternIndex);
                    }

                    if (!segmentParser.Match(routeSegmentType))
                    {
                        routePatterns.RemoveAt(currentPatternIndex);
                    }
                }
            }
        }
    }
}
