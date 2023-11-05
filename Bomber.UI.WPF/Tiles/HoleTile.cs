using System.Windows.Media;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Tiles;

namespace Bomber.UI.WPF.Tiles
{
    internal class HoleTile : ATile, IDeadlyTile
    {
        public bool InstantDeath { get; }
        public int Damage { get; }
        
        public override bool IsObstacle => false;

        public HoleTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService)
        {
            Fill = new SolidColorBrush(Colors.Black);
        }

        public override void SteppedOn(IUnit2D unit2D)
        {
            unit2D.Step(this);
        }
        
    }
}
