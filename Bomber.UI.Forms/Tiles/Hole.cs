using Bomber.BL.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Impl.Core.Position;
using GameFramework.Map.MapObject;
using GameFramework.Tiles;

namespace Bomber.UI.Forms.Tiles
{
    public sealed partial class Hole : UserControl, IDeadlyTile
    {
        public void SteppedOn(IUnit2D unit2D)
        {
            unit2D.Step(this);
        }
        public IPosition2D Position { get; }
        public IScreenSpacePosition ScreenSpacePosition { get; }
        public bool IsObstacle => false;
        
        public Hole(IPosition2D position, IConfigurationService2D configurationService)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.Y * configurationService.Dimension;
            Left = position.X * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Black;
            SendToBack();
            ScreenSpacePosition = new ScreenSpacePosition(position.X * configurationService.Dimension, position.Y * configurationService.Dimension);
        }
    }
}

