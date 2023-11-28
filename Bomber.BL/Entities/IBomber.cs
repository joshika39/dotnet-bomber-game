using Bomber.UI.Shared.Entities;
using GameFramework.Entities;

namespace Bomber.BL.Entities
{
    public interface IBomber : IPlayer2D, IBombWatcher
    {
        ICollection<IBomb> PlantedBombs { get; }
        IBomb PutBomb(IBombView bombView);

        void DetonateBombAt(int bombIndex);
        int Score { get; set; }
    }
}
