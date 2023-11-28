using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core.Position;
using GameFramework.Impl.Core.Position;
using GameFramework.Visuals;

namespace Bomber.UI.Forms.Views.Entities
{
    public sealed partial class EnemyView : UserControl, IEnemyView
    {
        private readonly IConfigurationService2D _configurationService2D;
        private readonly ICollection<IViewLoadedSubscriber> _subscribers = new List<IViewLoadedSubscriber>();
        private readonly ICollection<IViewDisposedSubscriber> _disposedSubscribers = new List<IViewDisposedSubscriber>();

        public IScreenSpacePosition PositionOnScreen { get; private set; }

        public EnemyView(IConfigurationService2D configurationService2D)
        {
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            InitializeComponent();
            Width = _configurationService2D.Dimension - 4;
            Height = _configurationService2D.Dimension - 4;
            BackColor = Color.Red;
            Disposed += OnDisposed;
            PositionOnScreen = new ScreenSpacePosition(0, 0);
        }

        private void OnDisposed(object? sender, EventArgs e)
        {
            foreach (var subscriber in _disposedSubscribers)
            {
                subscriber.OnViewDisposed(this);
            }
        }

        public void UpdatePosition(IPosition2D position)
        {
            if (IsDisposed)
            {
                return;
            }

            BeginInvoke(() =>
            {
                BringToFront();
                Top = position.Y * _configurationService2D.Dimension + 2;
                Left = position.X * _configurationService2D.Dimension + 2;
                PositionOnScreen =
                    new ScreenSpacePosition(
                        position.X * _configurationService2D.Dimension + 2,
                        position.Y * _configurationService2D.Dimension + 2
                    );
            });
        }

        public void ViewLoaded()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnLoaded();
            }
        }

        public new void Dispose()
        {
            BeginInvoke(() =>
                {
                    base.Dispose();
                }
            );
        }

        public void Attach(IViewLoadedSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Attach(IViewDisposedSubscriber subscriber)
        {
            _disposedSubscribers.Add(subscriber);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ViewLoaded();
        }
    }
}
