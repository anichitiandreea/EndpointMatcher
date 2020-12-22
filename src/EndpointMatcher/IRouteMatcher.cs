namespace EndpointMatcher
{
    public interface IRouteMatcher
    {
        string GetMatchedRoute();
        int GetRouteSegmentsNumber(string route);
        void RemovePatternBasedOnCurrentSegment(int indexOfSegment, string[] routeSegments);
    }
}
