using Newtonsoft.Json;
using System.IO;

namespace WpfUI
{
    public class EventWriter : EventHandlerBase, IEventWriter
    {
        public void Save(string jsonContent, string filePath)
        {
            if (File.Exists(filePath)) { File.Delete(filePath); }
            File.WriteAllText(filePath, jsonContent);
        }

        public bool Validate(string jsonContent)
        {
            try
            {
                JsonConvert.DeserializeObject(jsonContent);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
