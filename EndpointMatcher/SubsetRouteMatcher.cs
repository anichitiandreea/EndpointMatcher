using System.Collections.Generic;
using System.Linq;

namespace RouteTemplate
{
    public class SubsetRouteMatcher
    {
        public int GetNumberOfLevels(string value)
        {
            return value.Count(ch => ch == '/');
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

        public string GetSubsetMatchedRoute(string route, List<string> values)
        {
            string[] strings = route.Split('/');
            int index = 1;
            var indexOfSpecificity = -1;
            var numberOflevels = strings.Length - 1;
            while (index < strings.Length)
            {
                var typeOfSubset = GetTypeOfSubset(strings[index]);
                for (int i = 0; i < values.Count; i++)
                {
                    if (GetNumberOfLevels(values[i]) != numberOflevels)
                    {
                        values[i] = "-";
                    }

                    if (GetNumberOfLevels(values[i]) == numberOflevels)
                    {
                        var splited = values[i].Split('/')[index];

                        if (splited.IndexOf('{') < 0 && splited.IndexOf('}') < 0 && splited != strings[index])
                        {
                            values[i] = "-";
                        }

                        if (splited.IndexOf(':') > 0 && !splited.Contains(typeOfSubset))
                        {
                            values[i] = "-";
                        }

                        if (splited.IndexOf(':') > 0 && splited.Contains(typeOfSubset))
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
                return values[indexOfSpecificity];
            }

            return values.FirstOrDefault(v => v != "-");
        }
    }
}
