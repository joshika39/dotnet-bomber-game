using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace Bomber.UI.WPF.GameCanvas
{
    public partial class GameCanvasView : UserControl, IGameCanvasView
    {
        public GameCanvasView()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetRequiredService<IGameCanvasViewModel>();
        }
        
        public IGameCanvasViewModel ViewModel => (IGameCanvasViewModel)DataContext;
    }
}

