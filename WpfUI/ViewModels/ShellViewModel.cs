using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using WpfUI.Helpers;
using WpfUI.Models;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class ShellViewModel : Screen
    {
        private readonly IWindowManager _windowManager;
        private readonly SetupViewModel _setupViewModel;
        private readonly IEventReader _eventReader;

        public string FilePath { get; private set; }

        private List<Event> _events = new List<Event>();

        public List<Event> Events 
        {
            get => _events;
            set
            {
                _events = value;
                NotifyOfPropertyChange(() => Events);
                NotifyOfPropertyChange(() => TopEvent);
                NotifyOfPropertyChange(() => MinorEvents);
            }
        }

        public List<CalculatedEvent> MinorEvents => _events.Skip(1).Select(e => new CalculatedEvent(e, DateTimeHelper.GetRemainingDays(e.EventDate))).ToList();

        public CalculatedEvent TopEvent { get; private set; }

        public ShellViewModel(IWindowManager windowManager, SetupViewModel setupViewModel, IEventReader eventReader)
        {
            _windowManager = windowManager;
            _setupViewModel = setupViewModel;
            _eventReader = eventReader;
            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\days.json";
            LoadEvents();
            _setupViewModel.SetShellParent(this);
        }

        public void LoadEvents()
        {
            try
            {
                _events = _eventReader.Read(FilePath).OrderBy(e => e.EventDate).ToList();
            }
            catch
            {
                MessageBox.Show(TextFile.loadErrorMessage, TextFile.loadErrorTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                _events.Add(_eventReader.GetTodayEvent(TextFile.todayString));
            }

            var first = _events.First();
            TopEvent = new CalculatedEvent(first, DateTimeHelper.GetRemainingDays(first.EventDate));
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
