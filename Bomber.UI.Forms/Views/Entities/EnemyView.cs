using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.UI.Forms.Views.Entities
{
    public sealed partial class EnemyView : UserControl, IEnemyView
    {
        private readonly IConfigurationService2D _configurationService2D;
        
        public EnemyView(IConfigurationService2D configurationService2D, int guardCount)
        {
            _configurationService2D = configurationService2D ?? throw new ArgumentNullException(nameof(configurationService2D));
            InitializeComponent();
            Width = _configurationService2D.Dimension - 4;
            Height = _configurationService2D.Dimension - 4;
            BackColor = Color.Red;
            var label = new Label();
            label.Text = $@"{guardCount}";
            Controls.Add(label);
        }
        
        public void UpdatePosition(IPosition2D position)
        {
            if (IsDisposed)
            {
                return;
            }
            
            Invoke(() =>
            {
                BringToFront();
                Top = position.Y * _configurationService2D.Dimension + 2;
                Left = position.X * _configurationService2D.Dimension + 2; 
            });
        }
        
        public void ViewAddedToMap()
        {
            throw new NotImplementedException();
        }
        
        public event EventHandler? EntityLoaded;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EntityLoaded?.Invoke(this, EventArgs.Empty);
        }
    }
}

