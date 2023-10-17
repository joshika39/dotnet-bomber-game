using Bomber.BL.Entities;
using Bomber.BL.Impl.Entities;
using Bomber.BL.Map;
using Bomber.UI.Forms.Main;
using Bomber.UI.Forms.Views.Entities;
using Bomber.UI.Forms.Views.Main._Interfaces;
using GameFramework.Configuration;
using GameFramework.Core.Factories;
using GameFramework.Core.Motion;
using DialogResult = UiFramework.Shared.DialogResult;

namespace Bomber.UI.Forms.Views.Main
{
    public partial class MainWindow : Form, IMainWindow
    {
        public IMainWindowPresenter Presenter { get; }

        private IBomber? _player;

        private readonly IConfigurationService2D _service;
        private readonly IPositionFactory _factory;
        private readonly IServiceProvider _provider;
        private readonly IEntityFactory _entityFactory;

        public MainWindow(IConfigurationService2D service, IPositionFactory factory, IServiceProvider provider, IMainWindowPresenter presenter, IEntityFactory entityFactory)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _entityFactory = entityFactory ?? throw new ArgumentNullException(nameof(entityFactory));
            Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            KeyPreview = true;
            InitializeComponent();
            _token = new CancellationTokenSource();
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
            openDialog.Filter = "BoB files (*.bob)|*.bob";
            openDialog.InitialDirectory = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "joshik39", "Bomber", "maps");
            if (openDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var map = Presenter.OpenMap(openDialog.FileName);

            var view = new PlayerView(_service);
            _player = new PlayerModel(view, _factory.CreatePosition(3, 1), _service, "TestPlayer", "test@email.com", _token.Token);
            bomberMap.Controls.Add(view);
            map.Entities.Add(_player);
            foreach (var mapMapObject in map.MapObjects)
            {
                if (mapMapObject is Control control)
                {
#if DEBUG
                    var label = new Label();
                    label.Text = $"{mapMapObject.Position.X}, {mapMapObject.Position.Y}";
                    control.Controls.Add(label);
#endif
                    bomberMap.Controls.Add(control);
                }
            }
        }

        private readonly CancellationTokenSource _token;

        private async void OnTestClick(object sender, EventArgs e)
        {
            var map = _service.GetActiveMap<IBomberMap>();
            if (map is null)
            {
                return;
            }

            var enemyView = new EnemyView(_service, map.Entities.Count + 1);
            var enemy = _entityFactory.CreateEnemy(enemyView, _factory.CreatePosition(1, 1), _token.Token);
            bomberMap.Controls.Add(enemyView);
            map.Entities.Add(enemy);
            await enemy.ExecuteAsync();
        }

        private void OnStopTestClick(object sender, EventArgs e)
        {
            _token.Cancel();
        }

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (!_service.GameIsRunning || _player is null)
            {
                return;
            }

            var map = _service.GetActiveMap<IBomberMap>();

            if (e.KeyCode == Keys.D)
            {
                map?.MoveUnit(_player, Move2D.Right);
            }

            if (e.KeyCode == Keys.A)
            {
                map?.MoveUnit(_player, Move2D.Left);
            }

            if (e.KeyCode == Keys.W)
            {
                map?.MoveUnit(_player, Move2D.Forward);
            }

            if (e.KeyCode == Keys.S)
            {
                map?.MoveUnit(_player, Move2D.Backward);
            }

            if (e.KeyCode == Keys.T && map is not null)
            {
                var testEntities = map.GetEntitiesAtPortion(map.MapPortion(_player.Position, 3));
                foreach (var testEntity in testEntities)
                {
                    if (testEntity is not INpc enemy)
                    {
                        continue;
                    }
                    map?.Entities.Remove(enemy);
                }
            }

            if (e.KeyCode == Keys.B)
            {
                var view = new BombView(_service);
                _player.PutBomb(view, this);
                bomberMap.Controls.Add(view);
            }
        }

        public void BombExploded(IBomb bomb)
        {
            var map = _service.GetActiveMap<IBomberMap>();
            if (map is null)
            {
                return;
            }
            
            var affectedObjects = map.MapPortion(bomb.Position, bomb.Radius);
            
            var entities = map.GetEntitiesAtPortion(affectedObjects);
            foreach (var entity in entities)
            {
                map.Entities.Remove(entity);
                entity.Dispose();
            }
        }
    }
}
