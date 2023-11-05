using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IEnemy : IUnit2D, IDisposable
    {
        Task ExecuteAsync();
    }
}
