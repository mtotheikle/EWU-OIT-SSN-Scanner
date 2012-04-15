using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLParser
{
    class Parser
    {
        public static String ParseXMLtoString(String path)
        {
            String output = "";

            try
            {
                XmlTextReader reader = new XmlTextReader(path);
                reader.WhitespaceHandling = WhitespaceHandling.None;

                while (reader.Read())
                {
                    if (reader.NodeType.Equals(XmlNodeType.Text))
                        output += " " + reader.Value;
                }
            }
            catch (Exception e) { }

            return output;
        }
    }
}
