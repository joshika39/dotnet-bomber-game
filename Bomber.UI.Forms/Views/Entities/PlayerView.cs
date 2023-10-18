using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Views.Entities
{
    public partial class PlayerView : UserControl, IPlayerView
    {
        private readonly IConfigurationService2D _configurationService;

        public PlayerView(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            InitializeComponent();
            Width = _configurationService.Dimension - 4;
            Height = _configurationService.Dimension - 4;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EntityLoaded?.Invoke(this, EventArgs.Empty);
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            if (IsDisposed)
            {
                return;
            }
            
            Top = position.Y * _configurationService.Dimension + 2;
            Left = position.X * _configurationService.Dimension + 2;
            BringToFront();
        }
        
        public void ViewAddedToMap()
        {
            throw new NotImplementedException();
        }

        public event EventHandler? EntityLoaded;
    }
}
