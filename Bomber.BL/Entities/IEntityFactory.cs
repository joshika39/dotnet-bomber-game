using Bomber.UI.Shared.Entities;
using GameFramework.Core.Position;

namespace Bomber.BL.Entities
{
    public interface IEntityFactory
    {
        IBomb CreateBomb(IBombView view, 
            IPosition2D position2D, 
            int radius);
        
        IBomb CreateBomb(IBombView view, 
            IPosition2D position2D, 
            int radius,
            int timeToExplosion);
        
        IBomber CreatePlayer(
            IPlayerView view, 
            IPosition2D position, 
            string name, 
            string email);
        
        IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D);
    }
}
