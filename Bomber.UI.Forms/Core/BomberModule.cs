using Bomber.BL.Tiles.Factories;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.MapGenerator;
using Bomber.UI.Forms.MapGenerator._Interfaces;
using Bomber.UI.Forms.Tiles.Factories;
using Bomber.UI.Forms.Views.Main;
using Bomber.UI.Forms.Views.Main._Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Core
{
    public class BomberModule
    {
        public void LoadModules(IServiceCollection collection)
        {
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<IMapGeneratorWindow, MapGeneratorWindow>();
            
            collection.AddSingleton<IMainWindowPresenter, MainWindowPresenter>();
            collection.AddSingleton<IMapGeneratorWindowPresenter, MapGeneratorWindowPresenter>();
            
            collection.AddSingleton<ITileFactory, FormsTileFactory>();
        }
    }
}
