using System;
using Bomber.BL.Impl.Models;
using GameFramework.Configuration;

namespace Bomber.UI.WPF.ViewModels
{
    internal class MainWindowViewModel : AMainWindowModel, IMainWindowViewModel
    {
        public object DataContext => this;
        
        public MainWindowViewModel(IServiceProvider provider, IConfigurationService2D configurationService) : base(provider, configurationService)
        { }
    }
}
