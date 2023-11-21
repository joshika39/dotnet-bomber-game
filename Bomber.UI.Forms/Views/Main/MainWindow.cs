using System.Collections.ObjectModel;
using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Impl.Map;
using Bomber.BL.Map;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.Views.Entities;
using Bomber.UI.Forms.Views.Main._Interfaces;
using Bomber.UI.Shared.Entities;
using GameFramework.Board;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Factories;
using GameFramework.GameFeedback;
using GameFramework.Manager;
using GameFramework.Map.MapObject;
using GameFramework.Visuals;
using Infrastructure.Application;
using Infrastructure.Time.Listeners;
using Microsoft.Extensions.DependencyInjection;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.UI.Forms.Views.Main
{
    public sealed partial class MainWindow : Form, IMainWindow, ITickListener
    {
        public IMainWindowPresenter Presenter { get; }

        private IBomber? _player;

        private readonly IConfigurationService2D _service;
        private readonly IGameManager _gameManager;
        private readonly IPositionFactory _positionFactory;
        private readonly IServiceProvider _provider;
        private readonly IBoardService _boardService;
        private readonly ILifeCycleManager _lifeCycleManager;

        public TimeSpan ElapsedTime { get; set; }
        public ObservableCollection<IMapObject2D> MapObjects { get; set; }
        public ObservableCollection<IDynamicMapObjectView> EntityViews { get; set; }

        public MainWindow(IConfigurationService2D service, IMainWindowPresenter presenter, IGameManager gameManager, IPositionFactory positionFactory, IServiceProvider provider, IBoardService boardService)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            _positionFactory = positionFactory ?? throw new ArgumentNullException(nameof(positionFactory));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _boardService = boardService ?? throw new ArgumentNullException(nameof(boardService));
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _lifeCycleManager = provider.GetRequiredService<ILifeCycleManager>();
            KeyPreview = true;
            InitializeComponent();
            
            openToolStripMenuItem.Click += OnOpenMap!;
            openMapGeneratorToolStripMenuItem.Click += openMapGeneratorToolStripMenuItem_Click!;
            KeyPress += OnKeyPressed!;
            saveToolStripMenuItem.Click += OnSaveClicked!;
            
            MapObjects = new ObservableCollection<IMapObject2D>();
            EntityViews = new ObservableCollection<IDynamicMapObjectView>();
        }

        public DialogResult ShowOnTop()
        {
            var result = ShowDialog();

            switch (result)
            {
                case System.Windows.Forms.DialogResult.Cancel:
                case System.Windows.Forms.DialogResult.Abort:
                    return UiFramework.Shared.DialogResult.Cancelled;
                case System.Windows.Forms.DialogResult.Yes:
                case System.Windows.Forms.DialogResult.OK:
                    return UiFramework.Shared.DialogResult.Resolved;
                case System.Windows.Forms.DialogResult.None:
                case System.Windows.Forms.DialogResult.Retry:
                case System.Windows.Forms.DialogResult.Ignore:
                case System.Windows.Forms.DialogResult.No:
                case System.Windows.Forms.DialogResult.TryAgain:
                case System.Windows.Forms.DialogResult.Continue:
                default:
                    throw new InvalidOperationException("Unsupported dialog result!");
            }
        }

        private void openMapGeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Presenter.OpenMapGenerator();
        }

        private void OnOpenMap(object sender, EventArgs e)
        {
            var prevMap = _boardService.GetActiveMap<IBomberMap>();
            
            if(prevMap is { View: Control prevMapView })
            {
                Controls.Remove(prevMapView);
            }
            
            var openDialog = new OpenFileDialog();
            openDialog.Filter = @"BoB files (*.bob)|*.bob";
            openDialog.InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps");
            if (openDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var source = new BomberMapSource(_provider, openDialog.FileName);

            var mapView = new BomberMapView();
            
            var map = new BomberMap(
                source, 
                mapView,
                _positionFactory, 
                _service, 
                _provider.GetRequiredService<IEntityFactory>(), 
                _provider.GetRequiredService<IEntityViewFactory>());
            
            _gameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "Game started!"));
            _boardService.SetActiveMap<IBomberMap, IBomberMapSource, IMapView2D>(map);

            var view = new PlayerView(_service);
            _player = new PlayerModel(view, _positionFactory.CreatePosition(0, 0), "TestPlayer", "test@email.com", _gameManager, _lifeCycleManager, _boardService);
            map.Units.Add(_player);

            foreach (var unit in map.Units)
            {
                if (unit is not IEnemy enemy)
                {
                    continue;
                }
                
                Task.Run(async () => await enemy.ExecuteAsync());
            }
            
            _gameManager.StartGame(new GameplayFeedback(FeedbackLevel.Info, "The game is started"));
            _gameManager.Timer.PeriodicOperation(1000, this, _lifeCycleManager.Token);
            mapName.Text = map.MapSource.Name;
            description.Text = map.MapSource.Description;
            
            mapView.Left = 40;
            mapView.Top = 40;
            
            Controls.Add(mapView);
        }
        
        public void RaiseTick(int round)
        {
            Invoke(() =>
            {
                currentTime.Text = _gameManager.Timer.Elapsed.ToString("g");
            });
        }

        private void OnKeyPressed(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'p')
            {
                Presenter.PauseGame();
            }

            if (_gameManager.State != GameState.InProgress || _player is null)
            {
                return;
            }

            Presenter.HandleKeyPress(e.KeyChar);
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var map = _boardService.GetActiveMap<IBomberMap, IBomberMapSource, IBomberMapView>();
            if (_player is null || map is null)
            {
                return;
            }

            map.SaveProgress();
        }
    }
}
