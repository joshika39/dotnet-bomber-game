using System;
using System.Threading.Tasks;
using System.Windows.Media;
using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;

namespace Bomber.UI.WPF.Tiles
{
    internal class GroundTile : ATile, IBomberMapTileView
    {
        public override bool IsObstacle => false;

        public GroundTile(IPosition2D position, IConfigurationService2D configurationService) : base(position, configurationService)
        {
            Fill = new SolidColorBrush(Colors.Green);
        }

        public override void SteppedOn(IUnit2D unit2D)
        {
            if (ConfigurationService.GameIsRunning)
            {
                unit2D.Step(this);
            }
        }

        public async void IndicateBomb(double waitTime)
        {
            await SetBack(waitTime);
        }
        
        private async Task SetBack(double waitTime)
        {
            Dispatcher.Invoke(() =>
            {
                Fill = new SolidColorBrush(Colors.Yellow);
            });
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
            Dispatcher.Invoke(() =>
            {
                Fill = new SolidColorBrush(Colors.Green);
            });
        }
        
    }
}
