using System;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Impl.Models;
using Bomber.BL.Map;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.ViewModels;
using CommunityToolkit.Mvvm.Input;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.GameFeedback;
using Infrastructure.Time.Listeners;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Bomber.UI.WPF.Main
{
    internal partial class MainWindowViewModel : AMainWindowModel, IMainWindowViewModel, ITickListener
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly IGameManager _gameManager;
        private readonly IPositionFactory _positionFactory;
        private string _currentTime;
        public GameCanvasControl MapCanvas { get; set; }

        public object DataContext => this;
        public double CanvasWidth => _configurationService.Dimension * _configurationService.GetActiveMap<IBomberMap>()?.SizeX ?? 0d;
        public double CanvasHeight => _configurationService.Dimension * _configurationService.GetActiveMap<IBomberMap>()?.SizeY ?? 0d;
        
        public TimeSpan ElapsedTime { get; set; }
        public string CurrentTime
        {
            get => _currentTime;
            private set => SetProperty(ref _currentTime, value);
        }

        public MainWindowViewModel(IServiceProvider provider, IConfigurationService2D configurationService, IGameManager gameManager, IPositionFactory positionFactory) : base(provider, configurationService, gameManager)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            _gameManager.Timer.PeriodicOperation(1000, this, _configurationService.CancellationTokenSource.Token);
            _currentTime = "";
            CurrentTime = ElapsedTime.ToString("g");
            MapCanvas = new GameCanvasControl();
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
                Filter = "BoB files (*.bob)|*.bob",
                InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps")
            };
            if (!openDialog.ShowDialog() ?? false)
            {
                return;
            }

            var map = OpenMap(openDialog.FileName, MapCanvas);
            
            var view = new PlayerControl(_configurationService);
            var player = new PlayerModel(view, _positionFactory.CreatePosition(3, 1), _configurationService, "TestPlayer", "test@email.com", _gameManager);
            view.ViewLoaded();
            map.Units.Add(player);
            
            _gameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"), map);
        }
    }
}
