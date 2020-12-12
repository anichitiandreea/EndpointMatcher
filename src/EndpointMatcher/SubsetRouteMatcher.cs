using System;
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

        public string GetTypeOfSubset(string subsetFromRoute)
        {
            if (subsetFromRoute.ToLower() == "true" || subsetFromRoute.ToLower() == "false")
            {
                return "bool";
            }

            var isNumber = decimal.TryParse(subsetFromRoute, out _);

            if (isNumber)
            {
                return "number";
            }

            return "string";
        }

        public string GetSubsetMatchedRoute(string route)
        {
            string[] routeSubsets = route.Split('/');
            int index = 1;
            var indexOfSpecificity = -1;
            var numberOfRouteLevels = routeSubsets.Length - 1;

            while (index < routeSubsets.Length)
            {
                var primitiveTypeOfSubset = GetTypeOfSubset(routeSubsets[index]);

                for (int i = 0; i < routeValues.Count; i++)
                {
                    if (GetNumberOfRouteLevels(routeValues[i]) != numberOfRouteLevels)
                    {
                        routeValues[i] = "-";
                    }
                    else
                    {
                        var routeValueSubset = routeValues[i].Split('/')[index];

                        if (routeValueSubset.IndexOf("{", 0, StringComparison.Ordinal) < 0
                            && routeValueSubset.IndexOf("}", 0, StringComparison.Ordinal) < 0
                            && routeValueSubset != routeSubsets[index])
                        {
                            routeValues[i] = "-";
                        }

                        if (routeValueSubset.IndexOf(":", 0, StringComparison.Ordinal) > 0
                            && !routeValueSubset.Contains(primitiveTypeOfSubset))
                        {
                            routeValues[i] = "-";
                        }

                        if (routeValueSubset.IndexOf(':') > 0 && routeValueSubset.Contains(primitiveTypeOfSubset))
                        {
                            if (indexOfSpecificity == -1)
                            {
                                indexOfSpecificity = i;
                            }
                            else
                            {
                                return "many specific routes";
                            }
                        }
                    }
                }

                index++;
            }

            if (indexOfSpecificity != -1)
            {
                return routeValues[indexOfSpecificity];
            }

            return routeValues.FirstOrDefault(v => v != "-");
        }
    }
}
