using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.BL.Entities
{
    public interface IEntityFactory
    {
        IBomb CreateBomb(IBombView view, 
            IPosition2D position2D, 
            IConfigurationService2D configurationService2D, 
            IEnumerable<IBombWatcher> bombWatchers, int radius, 
            CancellationToken stoppingToken);
        
        IBomber CreatePlayer(
            IPlayerView view, 
            IPosition2D position, 
            IConfigurationService2D configurationService2D, 
            string name, 
            string email, CancellationToken cancellationToken);
        
        IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D, CancellationToken token);
    }
}
