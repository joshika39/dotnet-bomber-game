using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Tiles
{
    public sealed partial class WallTile : UserControl, IMapObject2D
    {
        public void SteppedOn(IUnit2D unit2D)
        {
            throw new NotImplementedException();
        }
        public IPosition2D Position { get; }
        public IScreenSpacePosition ScreenSpacePosition
        {
            get;
        }
        public bool IsObstacle => true;

        public WallTile(IPosition2D position, IConfigurationService2D configurationService)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.Y * configurationService.Dimension;
            Left = position.X * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Gray;
            SendToBack();
        }
        
        public void OnHovered()
        {
            throw new NotImplementedException();
        }
        
        public void OnHoverLost()
        {
            throw new NotImplementedException();
        }
        
        public bool IsHovered { get; }
    }
}

