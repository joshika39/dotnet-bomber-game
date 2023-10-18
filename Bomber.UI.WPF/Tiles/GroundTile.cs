using System.Windows.Media;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;

namespace Bomber.UI.WPF.Tiles
{
    internal class GroundTile : ATile
    {
        public GroundTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService)
        {
            Fill = new SolidColorBrush(Colors.Green);
        }

        public override void SteppedOn(IUnit2D unit2D)
        {
            if (ConfigurationService.GameIsRunning)
            {
                
            }
        }
    }
}
