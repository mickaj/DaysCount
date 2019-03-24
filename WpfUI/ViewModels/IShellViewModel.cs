using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Models;

namespace WpfUI.ViewModels
{
    public interface IShellViewModel
    {
        string FilePath { get; }
        IEvent Event { get; set; }

        void LoadEvent();
    }
}
