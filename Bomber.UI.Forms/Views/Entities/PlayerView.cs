using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Views.Entities
{
    public partial class PlayerView : UserControl, IPlayerView
    {
        private readonly IConfigurationService2D _configurationService;
        private readonly ICollection<IEntityViewSubscriber> _subscribers = new List<IEntityViewSubscriber>();
        private readonly ICollection<IEntityViewDisposedSubscriber> _disposedSubscribers = new List<IEntityViewDisposedSubscriber>();

        public PlayerView(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Width = _configurationService.Dimension - 4;
            Height = _configurationService.Dimension - 4;
            Disposed += OnDisposed;
        }
        
        private void OnDisposed(object? sender, EventArgs e)
        {
            foreach (var subscriber in _disposedSubscribers)
            {
                subscriber.OnViewDisposed(this);
            }
        }

        public void EntityViewLoaded()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.OnViewLoaded();
            }
        }
        
        public void Attach(IEntityViewSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }
        public void Attach(IEntityViewDisposedSubscriber subscriber)
        {
            _disposedSubscribers.Add(subscriber);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EntityViewLoaded();
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            if (IsDisposed)
            {
                return;
            }
            
            Top = position.Y * _configurationService.Dimension + 2;
            Left = position.X * _configurationService.Dimension + 2;
            BringToFront();
        }
    }
}
