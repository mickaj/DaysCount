using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Models
{
    public class Event : IEvent
    {
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
    }
}
