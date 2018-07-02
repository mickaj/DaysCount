using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using WpfUI.Models;

namespace WpfUI
{
    internal static class EventReader
    {
        internal static IEvent Read(string filePath)
        {
            if (!File.Exists(filePath)) { throw new FileNotFoundException("XML file with Event data doesn't exists"); }
            else
            {
                FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
                return Convert(content);
            }
        }

        private static IEvent Convert(string fileContent)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(fileContent);
            XmlNodeList nodeList = xmlDoc.SelectNodes("Days");
            XmlNode node = nodeList[0];
            try
            {
                IEvent result = new Event();
                int day = DateTime.Now.Day, month = DateTime.Now.Month, year = DateTime.Now.Year;
                foreach (XmlNode setting in node.ChildNodes)
                {
                    if (setting.NodeType != XmlNodeType.Comment)
                    {
                        switch (setting.Name)
                        {
                            case "Event":
                                result.EventName = setting.InnerText;
                                break;
                            case "Date":
                                result.EventDate = DateTime.ParseExact(setting.InnerText, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                break;
                        }
                    }
                }
                return result;
            }
            catch
            {
                throw new FileFormatException("File format incorrect!");
            }
        }
    }
}
