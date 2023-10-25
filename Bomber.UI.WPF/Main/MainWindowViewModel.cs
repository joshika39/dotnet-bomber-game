using System;
using Bomber.BL.Entities;
using Bomber.BL.Impl.Models;
using Bomber.BL.Map;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Time.Listeners;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Bomber.UI.WPF.Main
{
    internal partial class MainWindowViewModel : AMainWindowModel, IMainWindowViewModel, ITickListener
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly IGameManager _gameManager;
        private string _currentTime;
        private readonly IGameCanvasViewModel _canvasViewModel;

        public object DataContext => this;
        public double CanvasWidth => _configurationService.Dimension * _configurationService.GetActiveMap<IBomberMap>()?.SizeX ?? 0d;
        public double CanvasHeight => _configurationService.Dimension * _configurationService.GetActiveMap<IBomberMap>()?.SizeY ?? 0d;
        
        public TimeSpan ElapsedTime { get; set; }
        public string CurrentTime
        {
            get => _currentTime;
            private set => SetProperty(ref _currentTime, value);
        }

        public MainWindowViewModel(IServiceProvider provider, IConfigurationService2D configurationService, IGameManager gameManager) : base(provider, configurationService, gameManager)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _gameManager.Timer.PeriodicOperation(1000, this, _configurationService.CancellationTokenSource.Token);
            _currentTime = "";
            CurrentTime = ElapsedTime.ToString("g");
            _canvasViewModel = provider.GetRequiredService<IGameCanvasViewModel>();
        }

        public void RaiseTick(int round)
        {
            CurrentTime = _gameManager.Timer.Elapsed.ToString("g");
        }

        [RelayCommand]
        private void OnOpen()
        {
            var openDialog = new OpenFileDialog
            {
                Filter = @"BoB files (*.bob)|*.bob",
                InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps")
            };
            if (!openDialog.ShowDialog() ?? false)
            {
                return;
            }

            var map = OpenMap(openDialog.FileName);
            
            _canvasViewModel.StartGame(map);
        }
    }
}
