using Bomber.BL.Map;
using GameFramework.Visuals;

namespace Bomber.BL.Impl.Models
{
    public interface IMainWindowModel
    {
        IBomberMap OpenMap(string mapFileName, IBomberMapView mapView2D);
        void HandleKeyPress(char keyChar);
        void PauseGame();
    }
}
