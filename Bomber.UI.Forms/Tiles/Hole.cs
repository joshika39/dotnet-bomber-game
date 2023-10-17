using Bomber.BL.Tiles;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Entities;

namespace Bomber.UI.Forms.Tiles
{
    public sealed partial class Hole : UserControl, IDeadlyTile
    {
        private readonly IConfigurationService2D _configurationService;
        public void SteppedOn(IUnit2D unit2D)
        {
            if (unit2D is IPlayer2D)
            {
                _configurationService.GameIsRunning = true;
            }
            unit2D.Step(this);
        }
        public IPosition2D Position { get; }
        public bool IsObstacle => false;
        
        public Hole(IPosition2D position, IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            Position = position ?? throw new ArgumentNullException(nameof(position));
            InitializeComponent();
            Top = position.Y * configurationService.Dimension;
            Left = position.X * configurationService.Dimension;
            Width = configurationService.Dimension;
            Height = configurationService.Dimension;
            BackColor = Color.Black;
            SendToBack();
        }
    }
}

