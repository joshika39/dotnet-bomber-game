using Bomber.BL.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;
using GameFramework.Tiles;

namespace Bomber.UI.Forms.Tiles
{
    public sealed partial class Hole : UserControl, IMapObject2D, IDeadlyTile
    {
        public void SteppedOn(IUnit2D unit2D)
        {
            unit2D.Step(this);
        }
        public IPosition2D Position { get; }
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
        }
        public bool InstantDeath { get; }
        public int Damage { get; }
    }
}

