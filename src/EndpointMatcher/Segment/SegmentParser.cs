using System;

namespace EndpointMatcher.Segment
{
    public class SegmentParser
    {
        private bool isVariable;
        private bool hasStrictParameter;

        private string parameterName;
        private string parameterType;

        public SegmentParser(string segment)
        {
            Parse(segment);
        }

        public void Parse(string segment)
        {
            isVariable = segment.IndexOf("{", 0, StringComparison.Ordinal) >= 0
               && segment.IndexOf("}", 0, StringComparison.Ordinal) >= 0;

            var values = segment.Split(':');
            hasStrictParameter = values.Length > 1;

            parameterName = values[0];

            if (hasStrictParameter)
            {
                parameterType = values[1];
            }
        }

        public bool Match(string routeSegmentType)
        {
            if (hasStrictParameter)
            {
                return routeSegmentType == parameterType;
            }

            return true;
        }

        public bool IsVariable()
        {
            return isVariable;
        }
    }
}
