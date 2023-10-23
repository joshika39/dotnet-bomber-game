using Bomber.BL.Entities;
using Bomber.BL.Map;
using GameFramework.Time;

namespace Bomber.BL.Impl.Models
{
    public interface IMainWindowModel
    {
        IBomberMap OpenMap(string mapFileName);
        void BombExploded(IBomb bomb, IBomber bomber);
        void HandleKeyPress(char keyChar, IBomber bomber);
        void PauseGame();
    }
}
