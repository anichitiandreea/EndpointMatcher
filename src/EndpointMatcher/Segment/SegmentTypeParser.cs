namespace EndpointMatcher.Segment
{
    public class SegmentTypeParser
    {
        private readonly string segment;
        public SegmentTypeParser(string segment)
        {
            this.segment = segment;
        }

        public string GetSegmentType()
        {
            if (segment.ToLower() == "true" || segment.ToLower() == "false")
            {
                return "bool";
            }

            var isNumber = decimal.TryParse(segment, out _);

            if (isNumber)
            {
                return "number";
            }

            return "string";
        }
    }
}
