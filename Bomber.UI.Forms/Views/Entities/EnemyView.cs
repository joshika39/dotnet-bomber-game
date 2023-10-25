using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Views.Entities
{
    public sealed partial class EnemyView : UserControl, IEnemyView
    {
        private readonly IConfigurationService2D _configurationService2D;
        private readonly ICollection<IEntityViewSubscriber> _subscribers = new List<IEntityViewSubscriber>();
        private readonly ICollection<IEntityViewDisposedSubscriber> _disposedSubscribers = new List<IEntityViewDisposedSubscriber>();
        
        public EnemyView(IConfigurationService2D configurationService2D)
        {
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            InitializeComponent();
            Width = _configurationService2D.Dimension - 4;
            Height = _configurationService2D.Dimension - 4;
            BackColor = Color.Red;
            Disposed += OnDisposed;
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
            
            Invoke(() =>
            {
              
                BringToFront();
                Top = position.Y * _configurationService2D.Dimension + 2;
                Left = position.X * _configurationService2D.Dimension + 2; 
                
            });
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
    }
}

