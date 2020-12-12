namespace EndpointMatcher
{
    public interface ISubsetRouteMatcher
    {
        string GetSubsetMatchedRoute(string route);
        string GetTypeOfSubset(string subsetFromRoute);
        int GetNumberOfRouteLevels(string route);
    }
}
