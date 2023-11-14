using Bomber.BL.Impl.Models;
using Bomber.UI.Forms.MapGenerator._Interfaces;
using Bomber.UI.Forms.Views.Main._Interfaces;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Manager.State;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Views.Main
{
    internal class MainWindowPresenter : AMainWindowModel, IMainWindowPresenter, IGameStateChangedListener
    {
        private readonly IServiceProvider _provider;
        public MainWindowPresenter(IServiceProvider provider, IConfigurationService2D configurationService, IGameManager gameManager) : base(provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            gameManager.AttachListener(this);
        }
        
        public void OpenMapGenerator()
        {
            var generatorWindow = _provider.GetRequiredService<IMapGeneratorWindow>();
            generatorWindow.ShowOnTop();
        }
    }
}
