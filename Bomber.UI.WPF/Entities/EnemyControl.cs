using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core.Position;
using GameFramework.Impl.Core.Position;
using GameFramework.UI.WPF;
using GameFramework.Visuals;

namespace Bomber.UI.WPF.Entities
{
    internal class EnemyControl : ACustomShape, IEnemyView
    {
        private readonly ICollection<IViewLoadedSubscriber> _subscribers = new List<IViewLoadedSubscriber>();
        private readonly ICollection<IViewDisposedSubscriber> _disposedSubscribers = new List<IViewDisposedSubscriber>();

        public IScreenSpacePosition PositionOnScreen { get; private set; }

        public EnemyControl(IConfigurationService2D configurationService) : base(configurationService)
        {
            Fill = new SolidColorBrush(Colors.Red);
            PositionOnScreen = new ScreenSpacePosition(0, 0);
        }

        public void Dispose()
        {
            foreach (var subscriber in _disposedSubscribers)
            {
                Dispatcher.Invoke(() =>
                {
                    subscriber.OnViewDisposed(this);
                });
            }
        }

        public void UpdatePosition(IPosition2D position)
        {
            Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(this, position.X * ConfigurationService.Dimension);
                Canvas.SetTop(this, position.Y * ConfigurationService.Dimension);
                PositionOnScreen =
                    new ScreenSpacePosition(
                        position.X * ConfigurationService.Dimension,
                        position.Y * ConfigurationService.Dimension
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

        public void Attach(IViewLoadedSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Attach(IViewDisposedSubscriber subscriber)
        {
            _disposedSubscribers.Add(subscriber);
        }
    }
}
