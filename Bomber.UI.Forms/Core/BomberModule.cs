using Bomber.BL.Impl.Entities.Factories;
using Bomber.BL.Tiles.Factories;
using Bomber.UI.Forms._Interface;
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
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms.Core
{
    public class BomberModule
    {
        public void LoadModules(IServiceCollection collection)
        {
            collection.AddSingleton<IMainWindowPresenter, MainWindowPresenter>();
            collection.AddSingleton<IMapGeneratorWindowPresenter, MapGeneratorWindowPresenter>();
            
            collection.AddSingleton<IMainWindow, MainWindow>();
            collection.AddSingleton<IAboutWindow, AboutWindow>();
            collection.AddSingleton<IMapGeneratorWindow, MapGeneratorWindow>();
            
            collection.AddSingleton<ITileFactory, FormsTileFactory>();
            collection.AddSingleton<IEntityViewFactory, EntityViewFactory>();
            collection.AddSingleton<IFeedbackPopup, FormsFeedbackPopup>();
        }
    }
}
