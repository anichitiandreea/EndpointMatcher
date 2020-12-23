namespace EndpointMatcher.Route.Interfaces
{
    public interface IRouteMatcher
    {
        string GetMatchedRoute();
        string GetSpecificRoute();
        void RemovePatternBasedOnCurrentSegment(int indexOfSegment, string routeSegment);
    }
}
