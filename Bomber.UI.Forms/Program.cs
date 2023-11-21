using Bomber.BL.Impl;
using Bomber.UI.Forms.Core;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.Views.Main;
using Bomber.UI.Shared.Feedback;
using GameFramework.Core;
using GameFramework.Impl.Core;
using Implementation.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.Forms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var modules = LoadModules();
            var mainWindow = modules.GetRequiredService<IMainWindow>();
            var gameManager = modules.GetRequiredService<IGameManager>();
            gameManager.AttachListener(modules.GetRequiredService<IFeedbackPopup>());
            if (mainWindow is MainWindow window)
            {
                Application.Run(window);
            }
        }

        private static IServiceProvider LoadModules()
        {
            var collection = new ServiceCollection();
            var source = new CancellationTokenSource();
            var core = new CoreModule(collection, source);
            core.RegisterServices("Bomber");
            core
                .RegisterOtherServices(new GameFrameworkCore(collection, source))
                .RegisterOtherServices(new BusinessLogicModule(collection))
                .RegisterOtherServices(new BomberModule(collection));

            return collection.BuildServiceProvider();
        }
    }
}
