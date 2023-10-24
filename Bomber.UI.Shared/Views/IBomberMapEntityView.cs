using Bomber.UI.Shared.Entities;
using GameFramework.Core;

namespace Bomber.UI.Shared.Views
{
    public interface IBomberMapEntityView : IDisposable
    {
        void UpdatePosition(IPosition2D position);
        void EntityViewLoaded();
        void Attach(IEntityViewSubscriber subscriber);
        void Attach(IEntityViewDisposedSubscriber subscriber);
    }
}
