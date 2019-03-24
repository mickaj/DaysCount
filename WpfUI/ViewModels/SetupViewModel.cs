using Caliburn.Micro;
using System;
using System.IO;
using System.Windows;
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class SetupViewModel : Screen
    {
        private ShellViewModel Parent;

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

        public SetupViewModel(IEvent @event, ShellViewModel parent)
        {
            Parent = parent;
            Event = @event;
            EventNameEdits = Event.EventName;
            EventDateEdits = Event.EventDate;
        }

        public void CancelEdits()
        {
            this.TryClose();
        }

        public void SaveEdits()
        {
            try
            {
                Event.EventDate = EventDateEdits;
                Event.EventName = EventNameEdits;
                EventWriter.Save(Event, Parent.FilePath);
            }
            catch
            {
                this.TryClose();
                MessageBox.Show(TextFile.saveErrorMessage, TextFile.saveErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Parent.LoadEvent();
            this.TryClose();
        }
    }
}
