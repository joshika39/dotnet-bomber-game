using Bomber.BL.Impl.Models;
using Bomber.UI.Forms.MapGenerator._Interfaces;
using Bomber.UI.Forms.Views.Main._Interfaces;
using GameFramework.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Views.Main
{
    internal class MainWindowPresenter : AMainWindowModel, IMainWindowPresenter
    {
        private readonly IServiceProvider _provider;
        public MainWindowPresenter(IServiceProvider provider, IGameManager gameManager) : base(provider)
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
