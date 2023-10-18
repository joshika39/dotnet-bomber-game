using Bomber.BL.Tiles.Factories;
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
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<ITileFactory, WpfTileFactory>();
        }
    }
}
