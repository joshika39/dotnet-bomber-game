using System;
using System.Threading;
using System.Windows;
using Bomber.BL.Impl;
using Bomber.UI.Shared.Feedback;
using Bomber.UI.WPF.Views;
using GameFramework.Core;
using GameFramework.Impl.Core;
using GameFramework.UI.WPF.Core;
using Implementation.Module;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : GameApp2D
    {
        protected override IServiceProvider LoadModules(ServiceCollection collection)
        {
            var source = new CancellationTokenSource();
            var core = new CoreModule(collection, source);
            core.RegisterServices("Bomber");
            core
                .RegisterOtherServices(new GameFrameworkCore(collection, source))
                .RegisterOtherServices(new BusinessLogicModule(collection))
                .RegisterOtherServices(new WpfModule(collection));

            return collection.BuildServiceProvider();
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var scope = Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                
                var mainWindow = provider.GetRequiredService<IMainWindow>();
                var gameManager = provider.GetRequiredService<IGameManager>();
                gameManager.AttachListener(provider.GetRequiredService<IFeedbackPopup>());
                mainWindow.ShowOnTop();
            }
        }
    }
}
