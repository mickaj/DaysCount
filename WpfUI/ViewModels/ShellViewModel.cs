using Caliburn.Micro;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private IWindowManager _windowManager;
        private SetupViewModel _setupViewModel;
        private IEventReader _eventReader;

        public string FilePath { get; private set; }

        private Event _event;

        public Event Event
        {
            get => _event;
            set
            {
                _event = value;
                NotifyOfPropertyChange(() => Event);
            }
        }

        public int RemainingDays
        {
            get => (Event.EventDate - DateTime.Now).Days + 1;
        }

        public ShellViewModel(IWindowManager windowManager, SetupViewModel setupViewModel, IEventReader eventReader)
        {
            _windowManager = windowManager;
            _setupViewModel = setupViewModel;
            _eventReader = eventReader;
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\days.xml";
            LoadEvent();
            _setupViewModel.SetShellParent(this);
        }

        public void LoadEvent()
        {
            try
            {
                Event = _eventReader.Read(FilePath);
            }
            catch
            {
                MessageBox.Show(TextFile.loadErrorMessage, TextFile.loadErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                Event = _eventReader.GetTodayEvent(TextFile.todayString);
            }
            NotifyOfPropertyChange(() => RemainingDays);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void GoToWebsite()
        {
            System.Diagnostics.Process.Start("https://github.com/mickaj/DaysCount");
        }

        public void OpenSetup()
        {
            _windowManager.ShowDialog(_setupViewModel);
        }
    }
}
