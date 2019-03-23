using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Models;

namespace WpfUI.ViewModels
{
    public class SetupViewModel : Screen
    {
        private IEvent _event;
        public IEvent Event
        {
            get => _event;
            set
            {
                _event = value;
                NotifyOfPropertyChange(() => Event);
            }
        }

        private string _eventNameEdits;
        public string EventNameEdits
        {
            get => _eventNameEdits;
            set
            {
                _eventNameEdits = value;
                NotifyOfPropertyChange(() => EventNameEdits);
            }
        }

        private DateTime _eventDateEdits;
        public DateTime EventDateEdits
        {
            get => _eventDateEdits;
            set
            {
                _eventDateEdits = value;
                NotifyOfPropertyChange(() => EventDateEdits);
            }
        }

        public SetupViewModel(IEvent @event)
        {
            Event = @event;
            EventNameEdits = Event.EventName;
            EventDateEdits = Event.EventDate;
        }

        public void CancelEdits()
        {
            this.TryClose();
        }
    }
}
