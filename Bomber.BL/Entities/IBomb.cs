using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IBomb : IUnit2D
    {
        int Radius { get; }
        Task Detonate();
        int RemainingTime { get; }
        void Attach(IBombWatcher bombWatcher);
    }
}
