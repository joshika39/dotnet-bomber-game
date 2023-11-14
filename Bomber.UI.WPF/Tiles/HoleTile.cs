using System.Windows.Media;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.Tiles;
using GameFramework.UI.WPF;

namespace Bomber.UI.WPF.Tiles
{
    internal class HoleTile : ATile, IDeadlyTile
    {
        public override bool IsObstacle => false;

        public HoleTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService, Colors.Black, false)
        { }

        public override void SteppedOn(IUnit2D unit2D)
        {
            unit2D.Step(this);
        }

    }
}
