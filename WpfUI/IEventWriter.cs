using System.Collections.Generic;
using WpfUI.Models;

namespace WpfUI
{
    public interface IEventWriter
    {
        void Save(string jsonContent, string filePath);

        bool Validate(string jsonContent);
    }
}
