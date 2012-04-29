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
            XmlTextReader reader = null;
            String output = "";

            try
            {
                reader = new XmlTextReader(path);
                reader.WhitespaceHandling = WhitespaceHandling.None;

                while (reader.Read())
                {
                    if (reader.NodeType.Equals(XmlNodeType.Text))
                        output += " " + reader.Value;
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }
            catch (Exception e) { }

            reader.Close();
            reader = null;

            return output;
        }
    }
}
