using Bomber.UI.Shared.Entities;
using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IBomber : IPlayer2D, IBomberEntity, IBombWatcher
    {
        ICollection<IBomb> PlantedBombs { get; }
        void PutBomb(IBombView bombView, IBombWatcher bombWatcher);

        void DetonateBombAt(int bombIndex);
        int Score { get; set; }
    }
}
