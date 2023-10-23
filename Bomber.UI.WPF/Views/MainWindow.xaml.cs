using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Bomber.BL;
using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Map;
using Bomber.UI.WPF.Entities;
using Bomber.UI.WPF.ViewModels;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using GameFramework.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using UiFramework.Shared;
using Path = System.IO.Path;

namespace Bomber.UI.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow, IBombWatcher
    {
        private readonly IMainWindowViewModel _viewModel;
        private readonly IPositionFactory _positionFactory;
        private readonly IConfigurationService2D _configService;
        
        private IBomber? _player;
        private readonly IStopwatch _stopwatch;

        public MainWindow(IMainWindowViewModel viewModel, IServiceProvider provider)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
            provider = provider ?? throw new ArgumentNullException(nameof(provider));
            InitializeComponent();
            DataContext = _viewModel.DataContext;
            _positionFactory = provider.GetRequiredService<IPositionFactory>();
            _configService = provider.GetRequiredService<IConfigurationService2D>();
            _stopwatch = provider.GetRequiredService<IStopwatch>();
            MainCanvas.Background = new SolidColorBrush(Colors.Coral);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MainCanvas.Children.Clear();
            var openDialog = new OpenFileDialog
            {
                Filter = @"BoB files (*.bob)|*.bob",
                InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps")
            };
            if (!openDialog.ShowDialog() ?? false)
            {
                return;
            }

            var map = _viewModel.OpenMap(openDialog.FileName);

            var view = new PlayerControl(_configService, MainCanvas);
            _player = new PlayerModel(view, _positionFactory.CreatePosition(3, 1), _configService, "TestPlayer", "test@email.com", _stopwatch);
            view.ViewAddedToMap();
            map.Entities.Add(_player);
            foreach (var mapMapObject in map.MapObjects)
            {
                if (mapMapObject is Shape control)
                {
                    MainCanvas.Children.Add(control);
                }
            }
            MainCanvas.Children.Add(view);
        }

        public void BombExploded(IBomb bomb)
        {
           
        }

        public DialogResult ShowOnTop()
        {
            return ShowDialog() ?? false ? UiFramework.Shared.DialogResult.Resolved : UiFramework.Shared.DialogResult.Cancelled;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            
            if (!_configService.GameIsRunning || _player is null)
            {
                return;
            }

            var map = _configService.GetActiveMap<IBomberMap>();

            if (e.Key == Key.D)
            {
                map?.MoveUnit(_player, Move2D.Right);
            }

            if (e.Key == Key.A)
            {
                map?.MoveUnit(_player, Move2D.Left);
            }

            if (e.Key == Key.W)
            {
                map?.MoveUnit(_player, Move2D.Forward);
            }

            if (e.Key == Key.S)
            {
                map?.MoveUnit(_player, Move2D.Backward);
            }

            if (e.Key == Key.T && map is not null)
            {
                var testEntities = map.GetEntitiesAtPortion(map.MapPortion(_player.Position, 3));
                foreach (var testEntity in testEntities)
                {
                    if (testEntity is not INpc enemy)
                    {
                        continue;
                    }
                    map.Entities.Remove(enemy);
                }
            }

            if (e.Key == Key.B)
            {
                var view = new BombControl(_configService, MainCanvas);
                _player.PutBomb(view, this);
                view.ViewAddedToMap();
                MainCanvas.Children.Add(view);
            }
        }
    }
}
