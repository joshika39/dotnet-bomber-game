using System;
using System.Windows;
using Bomber.BL.Impl;
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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var modules = LoadModules();

            var mainWindowVM = modules.GetRequiredService<IMainWindowViewModel>();
            var mainWindow = modules.GetRequiredService<IMainWindow>();
            mainWindow.ShowOnTop();
        }
        
        private static IServiceProvider LoadModules()
        {
            var collection = new ServiceCollection();
            
            new CoreModule().LoadModules(collection, "Bomber");
            new GameModule().LoadModules(collection);
            new BusinessLogicModule().LoadModules(collection);
            new WpfModule().LoadModules(collection);
            
            return collection.BuildServiceProvider();
        }
    }
}
