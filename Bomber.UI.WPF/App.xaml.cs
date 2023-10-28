using System;
using System.Threading;
using System.Windows;
using Bomber.BL.Impl;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.ViewModels;
using Bomber.UI.WPF.Views;
using GameFramework.Impl.Core;
using Implementation.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var collection = new ServiceCollection();
            Services = LoadModules(collection);
        }

        public static new App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = Services.GetRequiredService<IMainWindow>();
            mainWindow.ShowOnTop();
        }

        private static IServiceProvider LoadModules(IServiceCollection collection)
        {
            new CoreModule().LoadModules(collection, "Bomber");
            new GameModule().LoadModules(collection, new CancellationTokenSource());
            new BusinessLogicModule().LoadModules(collection);
            new WpfModule().LoadModules(collection);

            return collection.BuildServiceProvider();
        }
    }
}
