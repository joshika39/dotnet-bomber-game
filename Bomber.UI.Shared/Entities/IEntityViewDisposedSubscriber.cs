using Bomber.UI.Shared.Views;

namespace Bomber.UI.Shared.Entities
{
    public interface IEntityViewDisposedSubscriber
    {
        void OnViewDisposed(IBomberMapEntityView view);
    }
}
