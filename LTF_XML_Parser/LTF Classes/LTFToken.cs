namespace LTF_XML_Parser.LTF_Classes
{
    public class LTFToken
    {
        public string id { get; set; } = null;
        public string pos { get; set; } = null;
        public string morph { get; set; } = null;
        public int? start_char { get; set; } = null;
        public int? end_char { get; set; } = null;
        public string value { get; set; } = null;

        public bool CheckValidity()
        {
            return id != null
                && pos != null
                && morph != null
                && start_char != null
                && end_char != null
                && value != null;
        }
    }
}
