using System;
using System.Globalization;
using System.IO;
using System.Xml;
using WpfUI.Models;

namespace WpfUI
{
    internal static class EventReader
    {
        internal static string DateFormat = "dd-MM-yyyy";
        internal static CultureInfo CultureInfo = CultureInfo.InvariantCulture;

        internal static string FILE_DOES_NOT_EXIST_EXCEPTION_MESSAGE = "XML file with Event data doesn't exists!";
        internal static string FILE_DOES_NOT_CONTAIN_VALID_DATA_EXCEPTION_MESSAGE = "XML file does not contain valid Event data!";

        internal static IEvent Read(string filePath)
        {
            if (!File.Exists(filePath)) { throw new FileNotFoundException(FILE_DOES_NOT_EXIST_EXCEPTION_MESSAGE); }
            else
            {
                return Convert(GetXmlContentString(filePath));
            }
        }

        private static IEvent Convert(string xmlContent)
        {
            XmlNode node = GetDaysNode(xmlContent);
            return GetEventFromNode(node);

        }

        private static string GetXmlContentString(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            reader.Dispose();
            stream.Dispose();
            return content;
        }

        private static XmlNode GetDaysNode(string xmlContent)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlContent);
            XmlNodeList nodeList = xmlDoc.SelectNodes("Days");
            if (nodeList.Count > 0)
            {
                return nodeList[0];
            }
            else { throw new FileFormatException(FILE_DOES_NOT_CONTAIN_VALID_DATA_EXCEPTION_MESSAGE); }

        }

        private static bool IsNodeValid(XmlNode node)
        {
            bool hasEvent = false;
            bool hasDate = false;

            foreach (XmlNode setting in node.ChildNodes)
            {
                if (setting.NodeType != XmlNodeType.Comment)
                {
                    if (setting.Name == "Event") { hasEvent = true; }
                    if (setting.Name == "Date")
                    {
                        hasDate = DateTime.TryParseExact(setting.InnerText, DateFormat, CultureInfo, DateTimeStyles.None, out _);
                    }
                }
            }
            return hasEvent && hasDate;
        }

        private static IEvent GetEventFromNode(XmlNode node)
        {
            IEvent daysEvent = new Event();
            if (IsNodeValid(node))
            {
                foreach (XmlNode setting in node.ChildNodes)
                {
                    if (setting.NodeType != XmlNodeType.Comment)
                    {
                        switch (setting.Name)
                        {
                            case "Event":
                                daysEvent.EventName = setting.InnerText;
                                break;
                            case "Date":
                                daysEvent.EventDate = DateTime.ParseExact(setting.InnerText, DateFormat, CultureInfo);
                                break;
                        }
                    }
                }
                return daysEvent;
            }
            else
            {
                throw new FileFormatException(FILE_DOES_NOT_CONTAIN_VALID_DATA_EXCEPTION_MESSAGE);
            }
        }
    }
}
