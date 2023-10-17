using Bomber.UI.Shared.Entities;
using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IBomber : IPlayer2D, IBomberEntity
    {
        ICollection<IBomb> PlantedBombs { get; }
        void PutBomb(IBombView bombView, IBombWatcher bombWatcher);
    }
}
