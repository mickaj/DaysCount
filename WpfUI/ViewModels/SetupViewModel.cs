using Caliburn.Micro;
using System;
using System.IO;
using System.Windows;
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class SetupViewModel : Screen, ISetupViewModel
    {
        private IShellViewModel _shellParent;
        private IEventWriter _eventWriter;
        private IEvent _event;

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

        public SetupViewModel(IEventWriter eventWriter, IEvent @event)
        {
            _eventWriter = eventWriter;
            _event = @event;
        }

        public void SetShellParent(IShellViewModel parent)
        {
            _shellParent = parent;
            EventNameEdits = _shellParent.Event.EventName;
            EventDateEdits = _shellParent.Event.EventDate;
        }

        public void CancelEdits()
        {
            this.TryClose();
        }

        public void SaveEdits()
        {
            try
            {
                _shellParent.Event.EventDate = EventDateEdits;
                _shellParent.Event.EventName = EventNameEdits;
                _eventWriter.Save(_shellParent.Event, _shellParent.FilePath);
            }
            catch
            {
                this.TryClose();
                MessageBox.Show(TextFile.saveErrorMessage, TextFile.saveErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _shellParent.LoadEvent();
            this.TryClose();
        }
    }
}
