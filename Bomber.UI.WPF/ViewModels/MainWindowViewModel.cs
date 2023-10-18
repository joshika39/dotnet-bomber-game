using System;
using Bomber.BL.Impl.Models;
using Bomber.BL.Map;
using GameFramework.Configuration;

namespace Bomber.UI.WPF.ViewModels
{
    internal class MainWindowViewModel : AMainWindowModel, IMainWindowViewModel
    {
        private readonly IConfigurationService2D _configurationService;
        public object DataContext => this;
        public double CanvasWidth => _configurationService.Dimension * _configurationService.GetActiveMap<IBomberMap>()?.SizeX ?? 0d;
        public double CanvasHeight => _configurationService.Dimension * _configurationService.GetActiveMap<IBomberMap>()?.SizeY ?? 0d;

        public MainWindowViewModel(IServiceProvider provider, IConfigurationService2D configurationService) : base(provider, configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }
    }
}
