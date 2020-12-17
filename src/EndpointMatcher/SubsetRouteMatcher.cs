using EndpointMatcher.Segment;
using System.Collections.Generic;
using System.Linq;

namespace EndpointMatcher
{
    public class SubsetRouteMatcher : ISubsetRouteMatcher
    {
        private readonly List<string> routeValues;

        public SubsetRouteMatcher(List<string> routeValues)
        {
            this.routeValues = routeValues;
        }

        public int GetNumberOfRouteLevels(string route)
        {
            return route.Count(character => character == '/');
        }

        public string GetSubsetMatchedRoute(string route)
        {
            string[] routeSubsets = route.Split('/');
            int index = 1;
            var numberOfRouteLevels = routeSubsets.Length - 1;

            while (index < routeSubsets.Length)
            {
                var segmentTypeParser = new SegmentTypeParser(routeSubsets[index]);
                var routeSegmentType = segmentTypeParser.GetSegmentType();

                for (int i = 0; i < routeValues.Count; i++)
                {
                    if (GetNumberOfRouteLevels(routeValues[i]) != numberOfRouteLevels)
                    {
                        routeValues.RemoveAt(i);
                    }
                    else
                    {
                        var patternSegment = routeValues[i].Split('/')[index];
                        var segmentParser = new SegmentParser(patternSegment);

                        if (!segmentParser.IsVariable() && patternSegment != routeSubsets[index])
                        {
                            routeValues.RemoveAt(i);
                        }

                        if (!segmentParser.Match(routeSegmentType))
                        {
                            routeValues.RemoveAt(i);
                        }
                    }
                }

                index++;
            }

            return routeValues.FirstOrDefault(v => v != "-");
        }
    }
}
