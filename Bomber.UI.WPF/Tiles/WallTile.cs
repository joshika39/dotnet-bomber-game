using System.Windows.Media;
using System.Windows.Shapes;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;
using GameFramework.Map.MapObject;

namespace Bomber.UI.WPF.Tiles
{
    internal class WallTile : ATile
    {
        public WallTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService)
        {
            Fill = new SolidColorBrush(Colors.Gray);
        }

        public override bool IsObstacle => true;
        public override void SteppedOn(IUnit2D unit2D)
        {
            if (ConfigurationService.GameIsRunning)
            {
                
            }
        }
    }
}
