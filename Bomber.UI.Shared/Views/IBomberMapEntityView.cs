using GameFramework.Core;

namespace Bomber.UI.Shared.Views
{
    public interface IBomberMapEntityView : IDisposable
    {
        void UpdatePosition(IPosition2D position);
        void ViewAddedToMap();
        event EventHandler EntityLoaded;
    }
}
