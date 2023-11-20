using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Impl.Core.Position;
using GameFramework.Visuals;

namespace Bomber.UI.Forms.Views.Entities
{
    public partial class PlayerView : UserControl, IPlayerView
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly ICollection<IViewLoadedSubscriber> _subscribers = new List<IViewLoadedSubscriber>();
        private readonly ICollection<IViewDisposedSubscriber> _disposedSubscribers = new List<IViewDisposedSubscriber>();

        public IScreenSpacePosition PositionOnScreen { get; private set; }
        
        public PlayerView(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Width = _configurationService.Dimension - 4;
            Height = _configurationService.Dimension - 4;
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

        public void ViewLoaded()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnLoaded();
            }
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
        
        public void UpdatePosition(IPosition2D position)
        {
            if (IsDisposed)
            {
                return;
            }
            
            Top = position.Y * _configurationService.Dimension + 2;
            Left = position.X * _configurationService.Dimension + 2;
            PositionOnScreen =
                new ScreenSpacePosition(
                    position.X * _configurationService.Dimension + 2,
                    position.Y * _configurationService.Dimension + 2
                    );
            BringToFront();
        }
    }
}
