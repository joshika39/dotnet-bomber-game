using Bomber.UI.Shared.Views;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;

namespace Bomber.UI.Forms.Tiles
{
    public sealed partial class GroundTile : UserControl, IBomberMapTileView
    {
        public void SteppedOn(IUnit2D unit2D)
        {
            unit2D.Step(this);
        }
        
        public IPosition2D Position { get; }
        public IScreenSpacePosition ScreenSpacePosition
        {
            get;
        }
        public bool IsObstacle => false;
        
        public async void IndicateBomb(double waitTime)
        {
            await SetBack(waitTime);
        }
        
        private async Task SetBack(double waitTime)
        {
            BackColor = Color.Yellow;
            await Task.Delay(TimeSpan.FromSeconds(waitTime));
            BackColor = Color.Green;
        }

        public GroundTile(IPosition2D position, IConfigurationService2D configurationService)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.Y * configurationService.Dimension;
            Left = position.X * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Green;
            SendToBack();
        }
        
        public void OnHovered()
        {
            throw new NotImplementedException();
        }
        
        public void OnHoverLost()
        {
            throw new NotImplementedException();
        }
        
        public bool IsHovered
        {
            get;
        }
    }
}
