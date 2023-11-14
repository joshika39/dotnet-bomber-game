using System;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Impl.Models;
using Bomber.BL.Map;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.GameCanvas;
using Bomber.UI.WPF.ViewModels;
using CommunityToolkit.Mvvm.Input;
using GameFramework.GameFeedback;
using Infrastructure.Time.Listeners;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Bomber.UI.WPF.Main
{
    internal partial class MainWindowViewModel : AMainWindowModel, IMainWindowViewModel, ITickListener
    {
        private string _currentTime;
        public GameCanvasControl MapCanvas { get; set; }

        public object DataContext => this;
        public double CanvasWidth => ConfigurationService.Dimension * BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>()?.SizeX ?? 0d;
        public double CanvasHeight => ConfigurationService.Dimension * BoardService.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>()?.SizeY ?? 0d;
        
        public TimeSpan ElapsedTime { get; set; }
        public string CurrentTime
        {
            get => _currentTime;
            private set => SetProperty(ref _currentTime, value);
        }

        public MainWindowViewModel(IServiceProvider provider) : base(provider)
        {
            GameManager.Timer.PeriodicOperation(1000, this, LifeCycleManager.Token);
            _currentTime = "";
            CurrentTime = ElapsedTime.ToString("g");
            MapCanvas = new GameCanvasControl();
        }

        public void RaiseTick(int round)
        {
            CurrentTime = GameManager.Timer.Elapsed.ToString("g");
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
            
            GameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"));
            BoardService.SetActiveMap(map);
        }

        [RelayCommand]
        private void OnSave()
        {
            var map = BoardService.GetActiveMap<IBomberMap>();
            map?.SaveProgress();
        }
    }
}
