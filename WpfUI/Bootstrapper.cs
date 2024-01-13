using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;
using WpfUI.Models;
using WpfUI.ViewModels;

namespace WpfUI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void Configure()
        {
            _container.Singleton<IWindowManager, WindowManager>();
            _container.Singleton<IEventReader, EventReader>();
            _container.Singleton<IEventWriter, EventWriter>();

            _container.PerRequest<Event>();
            _container.PerRequest<ShellViewModel, ShellViewModel>();
            _container.PerRequest<SetupViewModel, SetupViewModel>();
        }
    }
}
