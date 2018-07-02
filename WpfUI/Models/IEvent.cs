using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Models
{
    public interface IEvent
    {
        DateTime EventDate { get; set; }
        string EventName { get; set; }
    }
}
