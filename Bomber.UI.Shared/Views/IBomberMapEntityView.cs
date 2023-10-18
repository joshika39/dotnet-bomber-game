using GameFramework.Core;

namespace Bomber.UI.Shared.Views
{
    public interface IBomberMapEntityView : IDisposable
    {
        void UpdatePosition(IPosition2D position);
        event EventHandler EntityLoaded;
    }
}
