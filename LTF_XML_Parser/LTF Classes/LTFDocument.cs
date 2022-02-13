using System.Collections.Generic;

namespace LTF_XML_Parser.LTF_Classes
{
    public class LTFDocument
    {
        public string id { get; set; } = null;
        public string tokenization { get; set; } = null;
        public string grammar { get; set; } = null;
        public int? raw_text_char_length { get; set; } = null;
        public string raw_text_md5 { get; set; } = null;
        public List<LTFSegment> Segments { get; set; } = new List<LTFSegment>();

        public bool CheckValidity()
        {
            return id != null
                && tokenization != null
                && grammar != null
                && raw_text_char_length != null
                && raw_text_md5 != null;
        }
    }
}
