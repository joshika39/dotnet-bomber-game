using Bomber.BL.Entities;
using Bomber.BL.Feedback;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Map;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.Views.Entities;
using Bomber.UI.Forms.Views.Main._Interfaces;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.GameFeedback;
using GameFramework.Time.Listeners;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.UI.Forms.Views.Main
{
    public sealed partial class MainWindow : Form, IMainWindow, ITickListener
    {
        public IMainWindowPresenter Presenter { get; }

        private IBomber? _player;

        private readonly IConfigurationService2D _service;
        private readonly IGameManager _gameManager;

        public TimeSpan ElapsedTime { get; set; }

        public MainWindow(IConfigurationService2D service, IMainWindowPresenter presenter, IGameManager gameManager)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _gameManager = gameManager ?? throw new ArgumentNullException(nameof(gameManager));
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            KeyPreview = true;
            InitializeComponent();
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
            bomberMap.Controls.Clear();
            var openDialog = new OpenFileDialog();
            openDialog.Filter = @"BoB files (*.bob)|*.bob";
            openDialog.InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps");
            if (openDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var map = Presenter.OpenMap(openDialog.FileName);

            var view = new PlayerView(_service);
            _player = new PlayerModel(view, map.MapLayout.PlayerPosition, _service, "TestPlayer", "test@email.com", _gameManager);
            bomberMap.Controls.Add(view);
            map.Entities.Add(_player);
            foreach (var mapMapObject in map.MapObjects)
            {
                if (mapMapObject is Control control)
                {
                    bomberMap.Controls.Add(control);
                }
            }

            foreach (var unit in map.Entities)
            {
                if (unit is not IEnemy { View: EnemyView control } enemy)
                {
                    continue;
                }
                Task.Run(async () => await enemy.ExecuteAsync());

                bomberMap.Controls.Add(control);
            }

            _gameManager.GameStarted(new GameplayFeedback(FeedbackLevel.Info, "The game is started"));
            _gameManager.Timer.PeriodicOperation(1000, this, _service.CancellationTokenSource.Token);
            mapName.Text = map.MapLayout.Name;
        }

        public void BombExploded(IBomb bomb)
        {
            if (_player is null)
            {
                return;
            }

            Presenter.BombExploded(bomb, _player);
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

            if (!_service.GameIsRunning || _player is null)
            {
                return;
            }

            Presenter.HandleKeyPress(e.KeyChar, _player);

            if (e.KeyChar == 'b')
            {
                var view = new BombView(_service);
                _player.PutBomb(view, this);
                bomberMap.Controls.Add(view);
            }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var map = _service.GetActiveMap<IBomberMap>();
            if (_player is null || map is null)
            {
                return;
            }
            
            map.SaveProgress(_player);
        }
    }
}
