using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Visuals;

namespace Bomber.UI.WPF.Entities
{
    internal sealed class PlayerControl : ACustomShape, IPlayerView
    {
        private bool _disposed;
        private readonly ICollection<IViewLoadedSubscriber> _subscribers = new List<IViewLoadedSubscriber>();
        private readonly ICollection<IViewDisposedSubscriber> _disposedSubscribers = new List<IViewDisposedSubscriber>();

        public PlayerControl(IConfigurationService2D configurationService) : base(configurationService)
        {
            Fill = new SolidColorBrush(Colors.Fuchsia);
        }

        ~PlayerControl()
        {
            Dispose(false);
        }

        public void UpdatePosition(IPosition2D position)
        {
            Canvas.SetLeft(this, position.X * ConfigurationService.Dimension);
            Canvas.SetTop(this, position.Y * ConfigurationService.Dimension);
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

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var subscriber in _disposedSubscribers)
                {
                    subscriber.OnViewDisposed(this);
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
