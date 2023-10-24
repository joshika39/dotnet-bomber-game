using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.WPF.Entities
{
    internal sealed class BombControl : ACustomShape, IBombView
    {
        private bool _disposed;
        private readonly ICollection<IEntityViewSubscriber> _subscribers = new List<IEntityViewSubscriber>();
        private readonly ICollection<IEntityViewDisposedSubscriber> _disposedSubscribers = new List<IEntityViewDisposedSubscriber>();

        public BombControl(IConfigurationService2D configurationService) : base(configurationService)
        {
            Width = (double)ConfigurationService.Dimension / 2;
            Height = (double)ConfigurationService.Dimension / 2;
        }

        ~BombControl()
        {
            Dispose(true);
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            Canvas.SetLeft(this, position.X * ConfigurationService.Dimension + (double)ConfigurationService.Dimension / 4);
            Canvas.SetTop(this, position.Y * ConfigurationService.Dimension + (double)ConfigurationService.Dimension / 4);
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
            throw new NotImplementedException();
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
