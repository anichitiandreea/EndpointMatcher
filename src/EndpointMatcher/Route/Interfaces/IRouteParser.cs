namespace EndpointMatcher.Route.Interfaces
{
    public interface IRouteParser
    {
        int GetRouteSegmentsNumber(string route);
        int GetRouteSpecificSegmentsNumber(string route);
    }
}
