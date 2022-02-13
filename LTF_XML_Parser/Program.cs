using LTF_XML_Parser.LTF_Classes;
using System;

namespace LTF_XML_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"LTF Test Files\\test.ltf.xml";
            (bool canParse, _) = LTFParser.TryParseLTFDocument(filePath);
            Console.WriteLine($"Document parsed successfully: {canParse}");
        }
    }
}
