using System.Globalization;

namespace WpfUI
{
    public abstract class EventHandlerBase
    {
        protected string DateFormat = "dd-MM-yyyy";
        protected CultureInfo CultureInfo = CultureInfo.InvariantCulture;

        protected string FILE_DOES_NOT_EXIST_EXCEPTION_MESSAGE = "XML file with Event data does not exists!";
        protected string FILE_DOES_NOT_CONTAIN_VALID_DATA_EXCEPTION_MESSAGE = "XML file does not contain valid Event data!";

        protected string XmlString = "<?xml version=\u00221.0\u0022 encoding=\u0022utf-8\u0022 ?>"
    + "<Days>\n"
    + "<Date>{0}</Date>\n"
    + "<Event>{1}</Event>\n"
    + "</Days>";
    }
}
