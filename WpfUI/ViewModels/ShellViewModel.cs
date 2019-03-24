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
    public class ShellViewModel : Screen
    {
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

        public ShellViewModel()
        {
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\days.xml";
            LoadEvent();
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
            WindowManager wm = new WindowManager();
            wm.ShowDialog(new SetupViewModel(Event, this));
        }
    }
}
