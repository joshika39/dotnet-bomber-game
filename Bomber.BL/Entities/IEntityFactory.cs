using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;
using GameFramework.Core.Position;

namespace Bomber.BL.Entities
{
    public interface IEntityFactory
    {
        IBomb CreateBomb(IBombView view, 
            IPosition2D position2D, 
            IEnumerable<IBombWatcher> bombWatchers, int radius);
        
        IBomber CreatePlayer(
            IPlayerView view, 
            IPosition2D position, 
            string name, 
            string email);
        
        IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D);
    }
}
