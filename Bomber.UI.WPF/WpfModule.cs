using Bomber.BL.Tiles.Factories;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.Main;
using Bomber.UI.WPF.Tiles.Factories;
using Bomber.UI.WPF.ViewModels;
using Bomber.UI.WPF.Views;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.WPF
{
    public class WpfModule : IModule
    {
        public void LoadModules(IServiceCollection collection)
        {
            collection.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
            collection.AddSingleton<IGameCanvasViewModel, GameCanvasViewModel>();
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<ITileFactory, WpfTileFactory>();
            collection.AddSingleton<IEntityViewFactory, WpfEntityFactory>();

            collection.AddSingleton<IGameCanvasView, GameCanvasView>();
        }
    }
}
