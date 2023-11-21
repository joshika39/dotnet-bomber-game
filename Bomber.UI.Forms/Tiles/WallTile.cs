using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Impl.Core.Position;
using GameFramework.Map.MapObject;

namespace Bomber.UI.Forms.Tiles
{
    public sealed partial class WallTile : UserControl, IMapObject2D
    {
        public void SteppedOn(IUnit2D unit2D)
        {
            throw new NotSupportedException();
        }
        public IPosition2D Position { get; }
        public IScreenSpacePosition ScreenSpacePosition { get; }
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
            ScreenSpacePosition = new ScreenSpacePosition(position.X * configurationService.Dimension, position.Y * configurationService.Dimension);
        }
    }
}

