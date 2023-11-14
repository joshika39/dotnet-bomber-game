using Bomber.BL.Tiles.Factories;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Feedback;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.Main;
using Bomber.UI.WPF.Tiles.Factories;
using Bomber.UI.WPF.ViewModels;
using Bomber.UI.WPF.Views;
using GameFramework.Map.MapObject;
using Implementation.Module;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.WPF
{
    public class WpfModule : AModule, IBaseModule
    {
        public WpfModule(IServiceCollection collection) : base(collection)
        { }
        public override IModule RegisterServices(IServiceCollection collection)
        {
            collection.AddSingleton<IMainWindowViewModel, MainWindowViewModel>();
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<ITileFactory, WpfTileFactory>();
            collection.AddSingleton<IEntityViewFactory, WpfEntityFactory>();
            collection.AddSingleton<IMapObject2DConverter, WpfObject2DConverter>();
            collection.AddSingleton<IFeedbackPopup, WpfFeedbackPopup>();
            
            return this;
        }
    }
}
