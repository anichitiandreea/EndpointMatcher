namespace EndpointMatcher.Segment
{
    public class SegmentTypeParser
    {
        private string segmentType;
        public SegmentTypeParser(string segment)
        {
            Parse(segment);
        }

        public void Parse(string segment)
        {
            if (segment.ToLower() == "true" || segment.ToLower() == "false")
            {
                segmentType = "bool";
            }

            var isNumber = decimal.TryParse(segment, out _);

            if (isNumber)
            {
                segmentType = "number";
            }

            segmentType = "string";
        }

        public string GetSegmentType()
        {
            return segmentType;
        }
    }
}
