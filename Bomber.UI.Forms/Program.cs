using Bomber.BL.Impl;
using Bomber.UI.Forms.Core;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.Views.Main;
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

            if (mainWindow is MainWindow window)
            {
                Application.Run(window);
            }
        }

        private static IServiceProvider LoadModules()
        {
            var collection = new ServiceCollection();
            
            new CoreModule().LoadModules(collection, "Bomber");
            new GameModule().LoadModules(collection);
            new BusinessLogicModule().LoadModules(collection);
            new BomberModule().LoadModules(collection);

            return collection.BuildServiceProvider();
        }
    }
}
