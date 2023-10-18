using System.Windows.Media;
using Bomber.BL.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;

namespace Bomber.UI.WPF.Tiles
{
    internal class HoleTile : ATile, IDeadlyTile
    {
        public HoleTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService)
        {
            Fill = new SolidColorBrush(Colors.Black);
        }

        public override void SteppedOn(IUnit2D unit2D)
        {
            if (ConfigurationService.GameIsRunning)
            {
                
            }
        }
    }
}
