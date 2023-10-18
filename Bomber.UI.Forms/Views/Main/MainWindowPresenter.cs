using Bomber.BL.Impl.Models;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.MapGenerator._Interfaces;
using Bomber.UI.Forms.Views.Main._Interfaces;
using GameFramework.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Views.Main
{
    internal class MainWindowPresenter : AMainWindowModel, IMainWindowPresenter
    {
        private readonly IServiceProvider _provider;
        public MainWindowPresenter(IServiceProvider provider, IConfigurationService2D configurationService) : base(provider, configurationService)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }
        
        public void OpenMapGenerator()
        {
            var generatorWindow = _provider.GetRequiredService<IMapGeneratorWindow>();
            generatorWindow.ShowOnTop();
        }
        public IMainWindow? View { get; }

    }
}
