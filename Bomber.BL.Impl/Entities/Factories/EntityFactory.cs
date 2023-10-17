using Bomber.BL.Entities;
using Bomber.UI.Shared.Entities;
using GameFramework.Configuration;
using GameFramework.Core;

namespace Bomber.BL.Impl.Entities.Factories
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IConfigurationService2D _configurationService;
        public EntityFactory(IConfigurationService2D configurationService)
        {
            _configurationService = configurationService ?? throw new ArgumentNullException(nameof(configurationService));
        }

        public IBomb CreateBomb(IBombView view, IPosition2D position2D, IConfigurationService2D configurationService2D, IEnumerable<IBombWatcher> bombWatchers, int radius, CancellationToken stoppingToken)
        {
            return new Bomb(view, position2D, configurationService2D, bombWatchers, radius, stoppingToken);
        }
        
        public IBomber CreatePlayer(IPlayerView view, IPosition2D position, IConfigurationService2D configurationService2D, string name, string email, CancellationToken cancellationToken)
        {
            return new PlayerModel(view, position, configurationService2D, name, email, cancellationToken);
        }
        
        public IEnemy CreateEnemy(IEnemyView view, IPosition2D position2D, CancellationToken token)
        {
            return new Enemy(view, _configurationService, position2D, token);
        }
    }
}
