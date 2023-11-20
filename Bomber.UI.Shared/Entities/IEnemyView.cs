using Bomber.UI.Shared.Views;
using GameFramework.Core.Position;
using GameFramework.Visuals;

namespace Bomber.UI.Shared.Entities
{
    public interface IEnemyView : IDynamicMapObjectView
    {
        IScreenSpacePosition PositionOnScreen { get; }
    }
}
