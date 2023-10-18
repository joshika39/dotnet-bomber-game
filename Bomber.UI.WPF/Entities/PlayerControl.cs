using System;
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
        private readonly Canvas _canvas;
        private bool _disposed;
        
        public void ViewAddedToMap()
        {
            EntityLoaded?.Invoke(this, EventArgs.Empty);
        }
        
        public event EventHandler? EntityLoaded;
        public PlayerControl(IConfigurationService2D configurationService, Canvas canvas) : base(configurationService)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
            Fill = new SolidColorBrush(Colors.Fuchsia);
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            Canvas.SetLeft(this, position.X * ConfigurationService.Dimension);
            Canvas.SetTop(this, position.Y * ConfigurationService.Dimension);
        }

        private void Dispose(bool disposing)
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
