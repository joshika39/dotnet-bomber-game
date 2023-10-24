using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.WPF.Entities
{
    internal class EnemyControl : ACustomShape, IEnemyView
    {
        private readonly ICollection<IEntityViewSubscriber> _subscribers = new List<IEntityViewSubscriber>();
        private readonly ICollection<IEntityViewDisposedSubscriber> _disposedSubscribers = new List<IEntityViewDisposedSubscriber>();

        public EnemyControl(IConfigurationService2D configurationService) : base(configurationService)
        {
            Fill = new SolidColorBrush(Colors.Red);
        }

        public void Dispose()
        {
            foreach (var subscriber in _disposedSubscribers)
            {
                subscriber.OnViewDisposed(this);
            }
        }

        public void UpdatePosition(IPosition2D position)
        {
            Canvas.SetLeft(this, position.X * ConfigurationService.Dimension);
            Canvas.SetTop(this, position.Y * ConfigurationService.Dimension);
        }

        public void ViewAddedToMap()
        {
            throw new NotImplementedException();
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
    }
}
