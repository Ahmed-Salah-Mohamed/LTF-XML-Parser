using System.IO;
using System.Linq;
using System.Xml.Linq;
using LTF_XML_Parser.LTF_Classes;

namespace LTF_XML_Parser
{
    public static class LTFParser
    {
        private static string LTFDocumentTagName = "DOC";
        private static string LTFSegmentTagName = "SEG";
        private static string LTFTokenTagName = "TOKEN";
        private static string LTFOriginalTextTagName = "ORIGINAL_TEXT";

        private static string LTFIdPropertyName = "id";
        private static string LTFTokenizationPropertyName = "tokenization";
        private static string LTFGrammarnPropertyName = "grammar";
        private static string LTFRawTextLengthPropertyName = "raw_text_char_length";
        private static string LTFRawTextMD5PropertyName = "raw_text_md5";

        private static string LTFStartCharPropertyName = "start_char";
        private static string LTFEndCharPropertyName = "end_char";

        private static string LTFTokenPositionPropertyName = "pos";
        private static string LTFTokenMorphPropertyName = "morph";

        private static int? ParseStringIntoInt(string stringValue)
        {
            bool canParse = int.TryParse(stringValue, out int result);
            if (canParse)
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Parse LTF document using path
        /// </summary>
        /// <param name="documentPath">The document path</param>
        /// <returns>Returns boolean indicating parsing success and the parsed LTF document.</returns>
        public static (bool, LTFDocument) TryParseLTFDocument(string documentPath)
        {
            using (FileStream documentStream = new FileStream(documentPath, FileMode.Open))
            {
                return TryParseLTFDocument(documentStream);
            }
        }

        /// <summary>
        /// Parse LTF document using stream
        /// </summary>
        /// <param name="documentStream">The document stream</param>
        /// <returns>Returns boolean indicating parsing success and the parsed LTF document.</returns>
        public static (bool, LTFDocument) TryParseLTFDocument(Stream documentStream)
        {
            XDocument xmlDocument = XDocument.Load(documentStream);
            var ltfDocumentElement = xmlDocument.Descendants(LTFDocumentTagName).FirstOrDefault();
            if (ltfDocumentElement == null)
            {
                return (false, null);
            }

            bool canParse = true;
            LTFDocument ltfDocument = new LTFDocument();
            ltfDocument.id = ltfDocumentElement.Attribute(LTFIdPropertyName)?.Value;
            ltfDocument.tokenization = ltfDocumentElement.Attribute(LTFTokenizationPropertyName)?.Value;
            ltfDocument.grammar = ltfDocumentElement.Attribute(LTFGrammarnPropertyName)?.Value;
            ltfDocument.raw_text_char_length = ParseStringIntoInt(ltfDocumentElement.Attribute(LTFRawTextLengthPropertyName)?.Value);
            ltfDocument.raw_text_md5 = ltfDocumentElement.Attribute(LTFRawTextMD5PropertyName)?.Value;
            canParse &= ltfDocument.CheckValidity();

            foreach (var segmentElement in ltfDocumentElement.Descendants(LTFSegmentTagName))
            {
                LTFSegment ltfSegment = new LTFSegment();
                ltfSegment.id = segmentElement.Attribute(LTFIdPropertyName)?.Value;
                ltfSegment.start_char = ParseStringIntoInt(segmentElement.Attribute(LTFStartCharPropertyName)?.Value);
                ltfSegment.end_char = ParseStringIntoInt(segmentElement.Attribute(LTFEndCharPropertyName)?.Value);
                ltfSegment.originalText = segmentElement.Descendants(LTFOriginalTextTagName).FirstOrDefault()?.Value;
                
                foreach (var tokenElement in segmentElement.Descendants(LTFTokenTagName))
                {
                    LTFToken ltfToken = new LTFToken();
                    ltfToken.id = tokenElement.Attribute(LTFIdPropertyName)?.Value;
                    ltfToken.pos = tokenElement.Attribute(LTFTokenPositionPropertyName)?.Value;
                    ltfToken.morph = tokenElement.Attribute(LTFTokenMorphPropertyName)?.Value;
                    ltfToken.start_char = ParseStringIntoInt(tokenElement.Attribute(LTFStartCharPropertyName)?.Value);
                    ltfToken.end_char = ParseStringIntoInt(tokenElement.Attribute(LTFEndCharPropertyName)?.Value);
                    ltfToken.value = tokenElement.Value;

                    canParse &= ltfToken.CheckValidity();
                    ltfSegment.Tokens.Add(ltfToken);
                }

                canParse &= ltfSegment.CheckValidity();
                ltfDocument.Segments.Add(ltfSegment);
            }

            return (canParse, canParse ? ltfDocument : null);
        }
    }
}
