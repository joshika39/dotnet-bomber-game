using System;
using Bomber.BL;
using Bomber.BL.Map;

namespace Bomber.UI.WPF.GameCanvas
{
    public interface IGameCanvasViewModel : IBombWatcher
    {
        void StartGame(IBomberMap map);
    }
}
