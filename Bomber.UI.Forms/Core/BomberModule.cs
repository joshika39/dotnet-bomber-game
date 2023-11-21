using Bomber.BL.Tiles.Factories;
using Bomber.UI.Forms.Feedback;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.MapGenerator;
using Bomber.UI.Forms.MapGenerator._Interfaces;
using Bomber.UI.Forms.Tiles.Factories;
using Bomber.UI.Forms.Views.Entities;
using Bomber.UI.Forms.Views.Main;
using Bomber.UI.Forms.Views.Main._Interfaces;
using Bomber.UI.Shared.Entities;
using Bomber.UI.Shared.Feedback;
using GameFramework.Map.MapObject;
using Implementation.Module;
using Infrastructure.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Core
{
    public class BomberModule : AModule, IBaseModule
    {
        public BomberModule(IServiceCollection collection) : base(collection)
        { }
        
        public override IModule RegisterServices(IServiceCollection collection)
        {
            collection.AddSingleton<IMainWindowPresenter, MainWindowPresenter>();
            collection.AddSingleton<IMapGeneratorWindowPresenter, MapGeneratorWindowPresenter>();
            
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<IMapGeneratorWindow, MapGeneratorWindow>();
            
            collection.AddSingleton<ITileFactory, FormsTileFactory>();
            collection.AddSingleton<IEntityViewFactory, EntityViewFactory>();
            collection.AddSingleton<IFeedbackPopup, FormsFeedbackPopup>();
            collection.AddSingleton<IMapObject2DConverter, FormsObject2DConverter>();
            
            return this;
        }
    }
}
