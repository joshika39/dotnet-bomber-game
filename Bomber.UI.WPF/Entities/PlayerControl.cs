﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.WPF.Entities
{
    internal sealed class PlayerControl : ACustomShape, IPlayerView
    {
        private bool _disposed;
        private readonly ICollection<IEntityViewSubscriber> _subscribers = new List<IEntityViewSubscriber>();
        private readonly ICollection<IEntityViewDisposedSubscriber> _disposedSubscribers = new List<IEntityViewDisposedSubscriber>();

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
