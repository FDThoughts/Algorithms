using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AttributeIdOfMessage
{
    /// <summary>
    /// XML load, parse and search
    /// </summary>
    class Program
    {
        /// <summary>
        /// Get ids of entries that contains a specific message
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private static IEnumerable<int> GetIdsByMessage(string xml, string message)
        {
            // Create XDocument
            XDocument xDoc = XDocument.Parse(xml);

            // Find all XElements that contains the message
            var elements = xDoc.Root
                      .Elements("entry")
                      .Elements("message")
                      .Where(x => x.Value == message);

            // return all ids
            return Array.ConvertAll(elements.ToArray(),
                new Converter<XElement, int>(p =>
                    Convert.ToInt32(p.Parent.FirstAttribute.Value)));
        }

        /// <summary>
        /// Main, start point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // XML string
            String xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
                "<log>\n" +
                "    <entry id=\"1\">\n" +
                "        <message>Application started</message>\n" +
                "    </entry>\n" +
                "    <entry id=\"2\">\n" +
                "        <message>Application ended</message>\n" +
                "    </entry>\n" +
                "</log>";

            // display ids
            foreach (int id in GetIdsByMessage(xml, "Application ended"))
                Console.WriteLine($"Entry: {id} contains message 'Application ended'.");

            // end
            Console.WriteLine("end!");
            Console.ReadLine();
        }
    }
}
