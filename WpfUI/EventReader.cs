using System;
using System.Globalization;
using System.IO;
using System.Xml;
using WpfUI.Models;

namespace WpfUI
{
    public class EventReader : EventHandlerBase, IEventReader
    {
        private IEvent _event;

        public EventReader(IEvent @event)
        {
            _event = @event;
        }

        public IEvent Read(string filePath)
        {
            if (!File.Exists(filePath)) { throw new FileNotFoundException(FILE_DOES_NOT_EXIST_EXCEPTION_MESSAGE); }
            else
            {
                return Convert(GetXmlContentString(filePath));
            }
        }

        public IEvent GetTodayEvent(string todayString)
        {
            _event.EventName = todayString;
            _event.EventDate = DateTime.Now;
            return _event;
        }

        private IEvent Convert(string xmlContent)
        {
            XmlNode node = GetDaysNode(xmlContent);
            return GetEventFromNode(node);
        }

        private string GetXmlContentString(string filePath)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            reader.Dispose();
            stream.Dispose();
            return content;
        }

        private XmlNode GetDaysNode(string xmlContent)
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

        private bool IsNodeValid(XmlNode node)
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

        private IEvent GetEventFromNode(XmlNode node)
        {
            if (IsNodeValid(node))
            {
                foreach (XmlNode setting in node.ChildNodes)
                {
                    if (setting.NodeType != XmlNodeType.Comment)
                    {
                        switch (setting.Name)
                        {
                            case "Event":
                                _event.EventName = setting.InnerText;
                                break;
                            case "Date":
                                _event.EventDate = DateTime.ParseExact(setting.InnerText, DateFormat, CultureInfo);
                                break;
                        }
                    }
                }
                return _event;
            }
            else
            {
                throw new FileFormatException(FILE_DOES_NOT_CONTAIN_VALID_DATA_EXCEPTION_MESSAGE);
            }
        }
    }
}
