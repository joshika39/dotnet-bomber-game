using Bomber.UI.Shared.Views;
using GameFramework.Entities;

namespace Bomber.UI.Shared.Entities
{
    public interface IBomberEntity : IUnit2D, IDisposable
    {
        void Kill();
        IBomberMapEntityView View { get; }
    }
}
