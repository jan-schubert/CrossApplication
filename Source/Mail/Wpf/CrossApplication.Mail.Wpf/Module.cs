﻿using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Mail.Core.Navigation;
using CrossApplication.Mail.Wpf.Shell;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Contracts.Navigation;

namespace CrossApplication.Mail.Wpf
{
    [Module]
    public class Module : IModule
    {
        public Module(IContainer container, INavigationService navigationService, IViewManager viewManager)
        {
            _container = container;
            _navigationService = navigationService;
            _viewManager = viewManager;
        }

        private void RegisterViews()
        {
            var shell = new ViewItem(ViewKeys.Shell, false, RegionNames.MainRegion);
            shell.SubViewItems.Add(new ViewItem(typeof(RibbonStartView).FullName, false, RegionNames.RibbonRegion));

            _viewManager.AddViewItem(shell);
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<object, ShellView>(ViewKeys.Shell);
            _container.RegisterType<object, RibbonStartView>(typeof(RibbonStartView).FullName);

            RegisterViews();
            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            _navigationService.NavigateTo(ViewKeys.Shell);
            return Task.FromResult(false);
        }

        private readonly INavigationService _navigationService;
        private readonly IContainer _container;
        private readonly IViewManager _viewManager;
    }
}