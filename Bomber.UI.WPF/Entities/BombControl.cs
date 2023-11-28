using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core.Position;
using GameFramework.Impl.Core.Position;
using GameFramework.UI.WPF;
using GameFramework.Visuals;

namespace Bomber.UI.WPF.Entities
{
    internal sealed class BombControl : ACustomShape, IBombView
    {
        private bool _disposed;
        private readonly ICollection<IViewLoadedSubscriber> _subscribers = new List<IViewLoadedSubscriber>();
        private readonly ICollection<IViewDisposedSubscriber> _disposedSubscribers = new List<IViewDisposedSubscriber>();

        public IScreenSpacePosition PositionOnScreen { get; private set; }

        public BombControl(IConfigurationService2D configurationService) : base(configurationService)
        {
            Width = (double)ConfigurationService.Dimension / 2;
            Height = (double)ConfigurationService.Dimension / 2;
            PositionOnScreen = new ScreenSpacePosition(0, 0);
        }

        public void UpdatePosition(IPosition2D position)
        {
            Dispatcher.Invoke(() =>
            {
                Canvas.SetLeft(this, position.X * ConfigurationService.Dimension + (double)ConfigurationService.Dimension / 4);
                Canvas.SetTop(this, position.Y * ConfigurationService.Dimension + (double)ConfigurationService.Dimension / 4);
                PositionOnScreen =
                    new ScreenSpacePosition(
                        position.X * ConfigurationService.Dimension + (double)ConfigurationService.Dimension / 4,
                        position.Y * ConfigurationService.Dimension + (double)ConfigurationService.Dimension / 4
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
            if (!_disposedSubscribers.Contains(subscriber))
            {
                _disposedSubscribers.Add(subscriber);
            }
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            Dispatcher.Invoke(() =>
            {
                foreach (var subscriber in _disposedSubscribers)
                {
                    subscriber.OnViewDisposed(this);
                }
            });

            _disposed = true;
        }
    }
}
