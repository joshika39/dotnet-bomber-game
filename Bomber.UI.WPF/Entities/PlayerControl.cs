using System;
using System.Windows.Media;
using Bomber.UI.Shared.Entities;
using Bomber.UI.WPF.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.WPF.Entities
{
    internal class PlayerControl : ACustomShape, IPlayerView
    {
        public event EventHandler? EntityLoaded;
        public PlayerControl(IConfigurationService2D configurationService) : base(configurationService)
        {
            Fill = new SolidColorBrush(Colors.Fuchsia);
            EntityLoaded?.Invoke(this, EventArgs.Empty);
            GeometryTransform.Cl
        }
        
        public void Dispose()
        {
            // TODO release managed resources here
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            Rect.X = position.X * ConfigurationService.Dimension;
            Rect.Y = position.Y * ConfigurationService.Dimension;
        }
    }
}
