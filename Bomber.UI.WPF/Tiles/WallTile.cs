using System.Windows.Media;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;
using GameFramework.Entities;
using GameFramework.UI.WPF;

namespace Bomber.UI.WPF.Tiles
{
    internal class WallTile : ATile
    {
        public WallTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService, Colors.Gray, false)
        { }

        public override bool IsObstacle => true;
        
        public override void SteppedOn(IUnit2D unit2D)
        {
            // do nothing
        }
    }
}
