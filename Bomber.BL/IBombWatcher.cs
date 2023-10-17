using Bomber.BL.Entities;

namespace Bomber.BL
{
    public interface IBombWatcher
    {
        void BombExploded(IBomb bomb);
    }
}
