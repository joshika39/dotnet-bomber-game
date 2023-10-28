using System;
using Bomber.BL;
using Bomber.BL.Map;
using Microsoft.Extensions.DependencyInjection;
using UiFramework.WPF;

namespace Bomber.UI.WPF.GameCanvas
{
    public interface IGameCanvasViewModel : IViewModel, IBombWatcher
    {
        void StartGame(IBomberMap map);

    }
}
