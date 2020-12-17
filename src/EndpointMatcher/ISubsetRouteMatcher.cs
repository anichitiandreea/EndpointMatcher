namespace EndpointMatcher
{
    public interface ISubsetRouteMatcher
    {
        string GetSubsetMatchedRoute(string route);
        int GetNumberOfRouteLevels(string route);
    }
}
