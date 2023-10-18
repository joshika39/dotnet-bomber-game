using System;
using System.Windows.Controls;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.WPF.Entities
{
    internal sealed class BombControl : ACustomShape, IBombView
    {
        private readonly Canvas _canvas;
        private bool _disposed;

        public BombControl(IConfigurationService2D configurationService, Canvas canvas) : base(configurationService)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
        }
        public void UpdatePosition(IPosition2D position)
        {
            throw new NotImplementedException();
        }
        
        public void ViewAddedToMap()
        {
            throw new NotImplementedException();
        }
        
        public event EventHandler? EntityLoaded;
        
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _canvas.Children.Remove(this);
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
