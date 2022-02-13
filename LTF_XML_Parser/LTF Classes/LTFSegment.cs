using System.Collections.Generic;

namespace LTF_XML_Parser.LTF_Classes
{
    public class LTFSegment
    {
        public string id { get; set; } = null;
        public int? start_char { get; set; } = null;
        public int? end_char { get; set; } = null;
        public string originalText { get; set; } = null;
        public List<LTFToken> Tokens { get; set; } = new List<LTFToken>();

        public bool CheckValidity()
        {
            return id != null
                && start_char != null
                && end_char != null
                && originalText != null;
        }
    }
}
