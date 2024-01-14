using Caliburn.Micro;
using System;
using System.Windows;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class SetupViewModel : Screen
    {
        private ShellViewModel _shellParent;
        private readonly IEventWriter _eventWriter;
        private readonly IEventReader _eventReader;

        private string _jsonEdits;

        public string JsonEdits
        {
            get => _jsonEdits;
            set
            {
                _jsonEdits = value;
                NotifyOfPropertyChange(() => JsonEdits);
            }
        }

        public SetupViewModel(IEventWriter eventWriter, IEventReader eventReader)
        {
            _eventWriter = eventWriter;
            _eventReader = eventReader;
        }

        public void SetShellParent(ShellViewModel parent)
        {
            _shellParent = parent;
        }

        public void CancelEdits()
        {
            this.TryClose();
        }

        public void SaveEdits()
        {
            try
            {
                _eventWriter.Save(_jsonEdits, _shellParent.FilePath);
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
