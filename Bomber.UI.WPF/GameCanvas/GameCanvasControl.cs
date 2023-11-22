using System.Windows.Shapes;
using Bomber.BL.Map;
using Bomber.UI.Shared.Entities;
using GameFramework.UI.WPF.Map;

namespace Bomber.UI.WPF.GameCanvas
{
    public class GameCanvasControl : WpfMapControl, IBomberMapView
    {
        public void PlantBomb(IBombView bombView)
        {
            if (bombView is Shape shape)
            {
                Dispatcher.Invoke(() => Children.Add(shape));
            }
        }
        
        public void DeleteBomb(IBombView bombView)
        {
            if (bombView is Shape shape)
            {
                Dispatcher.Invoke(() => Children.Remove(shape));
            }
        }
        
        public void Clear()
        {
            Children.Clear();
        }
    }
}
