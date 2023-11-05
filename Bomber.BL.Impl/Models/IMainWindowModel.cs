using Bomber.BL.Entities;
using Bomber.BL.Map;
using GameFramework.Time;
using GameFramework.Visuals;

namespace Bomber.BL.Impl.Models
{
    public interface IMainWindowModel
    {
        IBomberMap OpenMap(string mapFileName, IMapView2D mapView2D);
        void BombExploded(IBomb bomb, IBomber bomber);
        void HandleKeyPress(char keyChar, IBomber bomber);
        void PutBomb();
        void PauseGame();
    }
}
