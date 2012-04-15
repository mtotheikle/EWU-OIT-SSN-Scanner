using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLParser
{
    public class Parser
    {
        public static String ParseXMLtoString(String path)
        {
            String output = "";
            XmlTextReader reader = new XmlTextReader(path);

            try
            {
                reader.WhitespaceHandling = WhitespaceHandling.None;

                while (reader.Read())
                {
                    if (reader.NodeType.Equals(XmlNodeType.Text))
                        output += " " + reader.Value;
                }

                reader.Close();
            }
            catch (Exception e)
            { 
                //Do nothing for now...
                reader.Close();
            }
            return output;
        }
    }
}
