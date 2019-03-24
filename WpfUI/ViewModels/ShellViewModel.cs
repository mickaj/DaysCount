using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class ShellViewModel : Screen, IShellViewModel
    {
        private IWindowManager _windowManager;
        private ISetupViewModel _setupViewModel;

        public string FilePath { get; private set; }

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

        public int RemainingDays
        {
            get => (Event.EventDate - DateTime.Now).Days + 1;
        }

        public ShellViewModel(IWindowManager windowManager, ISetupViewModel setupViewModel)
        {
            _windowManager = windowManager;
            _setupViewModel = setupViewModel;
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\days.xml";
            LoadEvent();
            _setupViewModel.SetShellParent(this);
        }

        public void LoadEvent()
        {
            try
            {
                Event = EventReader.Read(FilePath);
            }
            catch
            {
                MessageBox.Show(TextFile.loadErrorMessage, TextFile.loadErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                Event = EventReader.GetTodayEvent(TextFile.todayString);
            }
            NotifyOfPropertyChange(() => RemainingDays);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void GoToWebsite()
        {
            System.Diagnostics.Process.Start("https://mkajzer.pl/");
        }

        public void OpenSetup()
        {
            _windowManager.ShowDialog(_setupViewModel);
        }
    }
}
