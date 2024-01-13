using Caliburn.Micro;
using System;
using System.Windows;
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class SetupViewModel : Screen
    {
        private ShellViewModel _shellParent;
        private readonly IEventWriter _eventWriter;
        private readonly Event _event;

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

        public SetupViewModel(IEventWriter eventWriter, Event @event)
        {
            _eventWriter = eventWriter;
            _event = @event;
        }

        public void SetShellParent(ShellViewModel parent)
        {
            _shellParent = parent;
            EventNameEdits = _shellParent.TopEvent.EventName;
            EventDateEdits = _shellParent.TopEvent.EventDate;
        }

        public void CancelEdits()
        {
            this.TryClose();
        }

        public void SaveEdits()
        {
            try
            {
                _shellParent.TopEvent.EventDate = EventDateEdits;
                _shellParent.TopEvent.EventName = EventNameEdits;
                _eventWriter.Save(_shellParent.TopEvent, _shellParent.FilePath);
            }
            catch
            {
                this.TryClose();
                MessageBox.Show(TextFile.saveErrorMessage, TextFile.saveErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _shellParent.LoadEvents();
            this.TryClose();
        }
    }
}
